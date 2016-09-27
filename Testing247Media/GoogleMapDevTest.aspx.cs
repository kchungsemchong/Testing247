﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Testing247Media.Models;

namespace Testing247Media
{
    public partial class GoogleMapDevTest : System.Web.UI.Page
    {
	   protected void Page_Load(object sender, EventArgs e)
	   {
		  List<Marker> markers = new List<Marker>();
		  Marker marker1 = new Marker
		  {
			 Name = "Stadtbibliothek Zanklhof",
			 Latitude = 47.06976f,
			 Longtitude = 15.43154f
		  };
		  markers.Add(marker1);
		  Marker marker2 = new Marker
		  {
			 Name = "Stadtbibliothek dieMediathek",
			 Latitude = 47.06975f,
			 Longtitude = 15.43116f
		  };
		  markers.Add(marker2);

		  JavaScriptSerializer serializer = new JavaScriptSerializer();
		  string result = serializer.Serialize(markers);

		  Page.ClientScript.RegisterStartupScript(this.GetType(), "Test", "initialize()", true);
	   }

	   protected void btnSearch_Click(object sender, EventArgs e)
	   {
		  var address = txtSearch.Text;
		  var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(address));

		  var request = WebRequest.Create(requestUri);
		  var response = request.GetResponse();
		  var xdoc = XDocument.Load(response.GetResponseStream());
		  var result = xdoc.Element("GeocodeResponse").Element("result");
		  if (result == null)
		  {
			 ClientScript.RegisterStartupScript(this.GetType(), "InvalidLocation", "invalidLocation()", true);
			 return;
		  }
		  var locationElement = result.Element("geometry").Element("location");
		  var lat = locationElement.Element("lat").Value.ToString();
		  var lng = locationElement.Element("lng").Value.ToString();
		  string initialize = String.Format("initialize({0},{1})", lat, lng);

		  ClientScript.RegisterStartupScript(this.GetType(), "IntializeMap", initialize, true);
	   }


    }
}