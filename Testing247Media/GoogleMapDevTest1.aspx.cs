using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Testing247Media.Models;

namespace Testing247Media
{
    public partial class GoogleMapDevTest1 : System.Web.UI.Page
    {
	   private string cs = ConfigurationManager.ConnectionStrings["247"].ConnectionString;

	   protected DataTable dtGeoLocation
	   {
		  get
		  {
			 if (this.ViewState["GeoLocation"] == null)
			 {
				this.ViewState["GeoLocation"] = new DataTable();
			 }
			 return (DataTable)this.ViewState["GeoLocation"];
		  }
		  set
		  {
			 this.ViewState["GeoLocation"] = value;
		  }
	   }

	   protected bool IsMarkerTooClose
	   {
		  get
		  {
			 if (this.ViewState["MarkerTooClose"] == null)
			 {
				this.ViewState["MarkerTooClose"] = false;
			 }
			 return (bool)this.ViewState["MarkerTooClose"];
		  }
		  set
		  {
			 this.ViewState["MarkerTooClose"] = value;
		  }
	   }

	   protected bool IsMarkerValid
	   {
		  get
		  {
			 if (this.ViewState["MarkerValid"] == null)
			 {
				this.ViewState["MarkerValid"] = false;
			 }
			 return (bool)this.ViewState["MarkerValid"];
		  }
		  set
		  {
			 this.ViewState["MarkerValid"] = value;
		  }
	   }
	   public string ErrorMessage
	   {
		  get
		  {
			 if (this.ViewState["ErrorMessage"] == null)
			 {
				this.ViewState["ErrorMessage"] = "NoError";
			 }
			 return (string)this.ViewState["ErrorMessage"];
		  }
		  set
		  {
			 this.ViewState["ErrorMessage"] = value;
		  }
	   }
	   protected void Page_Load(object sender, EventArgs e)
	   {
		  int advertId = 1;
		  if (!Page.IsPostBack)
		  {
			 RetrieveGeoLocations(advertId);
			 BindGvGeolocation();
			 InitializeMap();
		  }

	   }

	   private void BindGvGeolocation()
	   {
		  gvGeoLocation.DataSource = dtGeoLocation;
		  gvGeoLocation.DataBind();
	   }

	   private void RetrieveGeoLocations(int advertId)
	   {
		  string storedProcedure = "mdAdvertGeoLocationGet";
		  using (SqlConnection conn = new SqlConnection(cs))
		  {
			 using (SqlDataAdapter da = new SqlDataAdapter())
			 {
				da.SelectCommand = new SqlCommand(storedProcedure, conn);
				da.SelectCommand.CommandType = CommandType.StoredProcedure;
				SqlParameter parameter = new SqlParameter("@AdvertId", advertId);
				da.SelectCommand.Parameters.Add(parameter);
				DataSet ds = new DataSet();
				da.Fill(ds, "GeoLocations");
				DataTable dt = ds.Tables["GeoLocations"];
				dtGeoLocation = dt;
			 }
		  }
	   }

	   private List<Marker> ConvertToMarker()
	   {
		  List<Marker> markers = new List<Marker>();
		  foreach (DataRow row in this.dtGeoLocation.Rows)
		  {
			 int zoomLevel = 1;
			 float latitude = 0;
			 float longitude = 0;
			 bool isLatitudeValid = float.TryParse(row["Latitude"].ToString(), out latitude);
			 bool isLongtitudeValid = float.TryParse(row["Longitude"].ToString(), out longitude);
			 bool isZoomLevelValid = int.TryParse(row["ZoomLevel"].ToString(), out zoomLevel);
			 Marker marker = new Marker();
			 marker.Name = row["Name"].ToString();
			 marker.Latitude = latitude;
			 marker.Longitude = longitude;
			 marker.ZoomLevel = zoomLevel;
			 markers.Add(marker);
		  }

		  return markers;
	   }

	   private string ConvertToJson(List<Marker> markers)
	   {
		  JavaScriptSerializer serializer = new JavaScriptSerializer();
		  string result = serializer.Serialize(markers);

		  return result;
	   }

	   private string ConvertToJson(string errorMessage)
	   {
		  JavaScriptSerializer serializer = new JavaScriptSerializer();
		  string result = serializer.Serialize(errorMessage);

		  return result;
	   }

	   public void InitializeMap()
	   {
		  string jsonMarkers = string.Empty;
		  string errorMessage = this.ErrorMessage;
		  errorMessage = ConvertToJson(errorMessage);
		  string initializeMethod = string.Format("initialize({0},{1})", jsonMarkers, errorMessage);
		  if (!(dtGeoLocation.Rows.Count > 0))
		  {
			 Page.ClientScript.RegisterStartupScript(this.GetType(), "InitializeGeoLocation", initializeMethod, true);
			 return;
		  }
		  List<Marker> markers = new List<Marker>();
		  markers = ConvertToMarker();
		  jsonMarkers = ConvertToJson(markers);
		  initializeMethod = string.Format("initialize({0},{1})", jsonMarkers, errorMessage);
		  Page.ClientScript.RegisterStartupScript(this.GetType(), "InitializeGeoLocation", initializeMethod, true);
	   }
	   private void AddNewMarkerToDataTable(Marker marker, string location)
	   {
		  DataRow row;
		  row = dtGeoLocation.NewRow();
		  row["GeoLocationId"] = 3;
		  row["Name"] = location;
		  row["Latitude"] = marker.Latitude;
		  row["Longitude"] = marker.Longitude;
		  row["ZoomLevel"] = marker.ZoomLevel;
		  dtGeoLocation.Rows.Add(row);
	   }
	   private void AssignMarkerToNewLocation(string location)
	   {
		  this.ErrorMessage = "NoError";
		  bool isOnlyInUk = false;
		  this.IsMarkerTooClose = false;
		  this.IsMarkerValid = true;
		  double invalidLatitude = 0.0;
		  double invalidLongitude = 0.0;
		  Marker marker = new Marker();
		  marker = marker.RetrieveCoordinates(location, isOnlyInUk);
		  if (marker.Latitude.Equals(invalidLatitude))
		  {
			 if (marker.Longitude.Equals(invalidLongitude))
			 {
				this.IsMarkerValid = false;
				this.ErrorMessage = " You entered an invalid location.";
				return;
			 }
		  }
		  EvaluateMarkerDuplication(marker);
		  if (this.IsMarkerTooClose)
			 return;
		  AddNewMarkerToDataTable(marker, location);
	   }
	   private void EvaluateMarkerDuplication(Marker marker)
	   {
		  double markerLat = 0;
		  double markerLng = 0;
		  markerLat = Math.Round(marker.Latitude, 4);
		  markerLng = Math.Round(marker.Longitude, 4);
		  foreach (DataRow row in dtGeoLocation.Rows)
		  {
			 double lat = Math.Round(Convert.ToDouble(row["Latitude"]), 4);
			 double lng = Math.Round(Convert.ToDouble(row["Longitude"]), 4);
			 if (lat.Equals(markerLat) && lng.Equals(markerLng))
			 {
				this.IsMarkerTooClose = true;
				this.ErrorMessage = "Marker is too close to existing ones";
				break;
			 }
		  }
	   }
	   protected void btnGeoLocation_Click(object sender, EventArgs e)
	   {
		  string location = txtGeoLocation.Text;
		  if (!string.IsNullOrEmpty(location))
		  {
			 AssignMarkerToNewLocation(location);
			 if (!this.IsMarkerValid)
				return;
			 BindGvGeolocation();
			 InitializeMap();
		  }
	   }
	   protected void gvGeoLocation_RowDeleting(object sender, GridViewDeleteEventArgs e)
	   {
		  int index = e.RowIndex;
		  dtGeoLocation.Rows.RemoveAt(index);
		  BindGvGeolocation();
		  InitializeMap();
	   }

	   protected void btnSave_Click(object sender, EventArgs e)
	   {
		  InitializeMap();
		  string locations = txtLocations.Text;
	   }
    }
}