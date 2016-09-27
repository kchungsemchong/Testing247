using System;
using System.Collections.Generic;
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
	   public float Latitude { get; set; }
	   public float Longtitude { get; set; }
	   public int AdvertId { get; set; }


	   public Marker RetrieveCoordinates(string location, bool isOnlyInUk)
	   {
		  Marker coordinate = new Marker();
		  string requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(location));
		  if (isOnlyInUk)
		  {
			 requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&components=GB&sensor=false", Uri.EscapeDataString(location));
		  }

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
		  float latitude = 0;
		  float longtitude = 0;
		  bool isLatitudeValid = float.TryParse(locationElement.Element("lat").Value.ToString(), out latitude);
		  bool isLongtitudeValid = float.TryParse(locationElement.Element("lng").Value.ToString(), out longtitude);
		  coordinate.Latitude = latitude;
		  coordinate.Longtitude = longtitude;
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