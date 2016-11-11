using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Configuration;
using System.Web.Script.Serialization;
using System.IO;
using Newtonsoft.Json;

namespace GoogleMapsConsoleTest
{
    public class Marker
    {
        public int GeoLocationId { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int AdvertId { get; set; }
        public int ZoomLevel { get; set; }

        private static string googleMapsApiKey = string.Empty;

        public Marker()
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["GoogleMapsApiKey"]))
                googleMapsApiKey = ConfigurationManager.AppSettings["GoogleMapsApiKey"];
        }

        public void RetrieveRestrictedCoordinates(string location)
        {
            bool isInUK = false;
            string ukCode = "UK";
            Geocoder geocoder = new Geocoder();
            string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}", Uri.EscapeDataString(location), googleMapsApiKey);
            requestUri = string.Concat(requestUri, "&components=GB");
            HttpWebResponse response = (HttpWebResponse)RetrieveCoordinatesFromLocation(requestUri);
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            var result = reader.ReadToEnd();
            geocoder = JsonConvert.DeserializeObject<Geocoder>(result);
            if(geocoder != null)
            {
                Result geocodingResult = new Result();
                geocodingResult = geocoder.Results.FirstOrDefault();
                foreach (var component in geocodingResult.AddressComponents)
                {
                    if (component.ShortName.ToLowerInvariant().Trim() == ukCode.ToLowerInvariant().Trim())
                    {
                        isInUK = true;
                        break;
                    }             
                }
            }
        }

        public Marker RetrieveCoordinates(string location, bool isOnlyInUk)
        {
            Marker marker = new Marker();
            string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?address={0}&key={1}", Uri.EscapeDataString(location), googleMapsApiKey);

            if (isOnlyInUk)
            {
                requestUri = string.Concat(requestUri, "&components=GB");
                WebResponse response = RetrieveCoordinatesFromLocation(requestUri);
                XElement result = GetGeocodeResponse(response);

                if (result == null || !CheckIfInUk(result))
                    return marker;

                var locationElement = result.Element("geometry").Element("location");
                AssignCoordinate(marker, locationElement);
            }
            else
            {
                WebResponse response = RetrieveCoordinatesFromLocation(requestUri);
                XElement result = GetGeocodeResponse(response);
                if (result == null)
                    return marker;

                var locationElement = result.Element("geometry").Element("location");
                AssignCoordinate(marker, locationElement);
            }

            return marker;
        }

        private void AssignCoordinate(Marker coordinate, XElement locationElement)
        {
            double latitude = 0;
            double longitude = 0;
            bool isLatitudeValid = double.TryParse(locationElement.Element("lat").Value.ToString(), out latitude);
            bool isLongtitudeValid = double.TryParse(locationElement.Element("lng").Value.ToString(), out longitude);
            coordinate.Latitude = latitude;
            coordinate.Longitude = longitude;
        }
        private WebResponse RetrieveCoordinatesFromLocation(string requestUri)
        {
            var request = WebRequest.Create(requestUri);
            var response = request.GetResponse();

            return response;
        }
        private XElement GetGeocodeResponse(WebResponse response)
        {
            XmlTextReader reader = new XmlTextReader(response.GetResponseStream());
            var xdoc = XDocument.Load(reader);
            var result = xdoc.Element("GeocodeResponse").Element("result");

            return result;
        }

        private bool CheckIfInUk(XElement locations)
        {
            var addressComponent = locations.Elements("address_component");
            var longNames = addressComponent.Elements("long_name");
            string ukCode = "United Kingdom";
            bool isInUk = false;
            foreach (var longName in longNames)
            {
                var location = longName.Value;
                if (location.Trim().ToLowerInvariant() == ukCode.Trim().ToLowerInvariant())
                {
                    isInUk = true;
                    break;
                }
            }

            return isInUk;
        }

        public List<Marker> ConvertToMarker(DataTable dtGeoLocation)
        {
            List<Marker> markers = new List<Marker>();
            foreach (DataRow row in dtGeoLocation.Rows)
            {
                Marker marker = new Marker();
                marker.ZoomLevel = AssignZoomLevelToMarker(row);
                marker.Latitude = AssignLatitudeToMarker(row);
                marker.Longitude = AssignLongitudeToMarker(row);
                marker.Name = row["Name"].ToString();

                markers.Add(marker);
            }

            return markers;
        }

        public Marker ConvertPostCodeToMarker(string postCode)
        {
            Marker marker = new Marker();
            bool isInUkOnly = true;
            marker = RetrieveCoordinates(postCode, isInUkOnly);
            marker.Name = postCode;
            marker.ZoomLevel = 0;

            return marker;
        }

        public string ConvertMarkerToJson<T>(List<T> markers)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string result = serializer.Serialize(markers);

            return result;
        }
        protected int AssignZoomLevelToMarker(DataRow row)
        {
            int zoomLevel = 0;
            bool isZoomLevelValid = int.TryParse(row["ZoomLevel"].ToString(), out zoomLevel);

            return zoomLevel;
        }
        protected double AssignLatitudeToMarker(DataRow row)
        {
            double latitude = 0;
            bool isLatitudeValid = double.TryParse(row["Latitude"].ToString(), out latitude);

            return latitude;
        }

        protected double AssignLongitudeToMarker(DataRow row)
        {
            double longitude = 0;
            bool isLongitudeValid = double.TryParse(row["Longitude"].ToString(), out longitude);

            return longitude;
        }
    }
}
