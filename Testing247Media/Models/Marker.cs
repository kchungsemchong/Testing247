using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace Testing247Media.Models
{
    public class Marker
    {
        public int GeoLocationId { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int AdvertId { get; set; }
        public int ZoomLevel { get; set; }

        private static string googleMapsApiKey;

        public Marker()
        {
            googleMapsApiKey = ConfigurationManager.AppSettings["GoogleMapsApiKey"];
        }

        public Marker RetrieveCoordinates(string location, bool isOnlyInUk)
        {
            Marker coordinate = new Marker();
            string requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&key={1}", Uri.EscapeDataString(location), googleMapsApiKey);

            if (isOnlyInUk)
                requestUri = string.Concat(requestUri, "&components=GB");

            WebResponse response = RetrieveCoordinatesFromLocation(requestUri);
            XElement result = GetGeocodeResponse(response);

            if (result == null)
                return coordinate;

            var locationElement = result.Element("geometry").Element("location");
            AssignCoordinate(coordinate, locationElement);

            return coordinate;

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
    }
}