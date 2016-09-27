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
			 float latitude = 0;
			 float longtitude = 0;
			 bool isLatitudeValid = float.TryParse(row["Latitude"].ToString(), out latitude);
			 bool isLongtitudeValid = float.TryParse(row["Longtitude"].ToString(), out longtitude);
			 Marker marker = new Marker();
			 marker.Name = row["Name"].ToString();
			 marker.Latitude = latitude;
			 marker.Longtitude = longtitude;
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

	   public void InitializeMap()
	   {
		  string jsonMarkers = string.Empty;
		  string initializeMethod = string.Format("initialize({0})", jsonMarkers);
		  if (!(dtGeoLocation.Rows.Count > 0))
		  {
			 Page.ClientScript.RegisterStartupScript(this.GetType(), "InitializeGeoLocation", initializeMethod, true);
			 return;
		  }
		  List<Marker> markers = new List<Marker>();
		  markers = ConvertToMarker();
		  jsonMarkers = ConvertToJson(markers);
		  initializeMethod = string.Format("initialize({0})", jsonMarkers);
		  Page.ClientScript.RegisterStartupScript(this.GetType(), "InitializeGeoLocation", initializeMethod, true);
	   }
	   private void AddNewMarkerToDataTable(Marker marker, string location)
	   {
		  DataRow row;
		  row = dtGeoLocation.NewRow();
		  row["GeoLocationId"] = 3;
		  row["Name"] = location;
		  row["Latitude"] = marker.Latitude;
		  row["Longtitude"] = marker.Longtitude;
		  dtGeoLocation.Rows.Add(row);
	   }
	   private void AssignMarkerToNewLocation(string location)
	   {
		  bool isOnlyInUk = false;
		  Marker marker = new Marker();
		  marker = marker.RetrieveCoordinates(location, isOnlyInUk);
		  AddNewMarkerToDataTable(marker, location);
	   }

	   protected void btnGeoLocation_Click(object sender, EventArgs e)
	   {
		  string location = txtGeoLocation.Text;
		  if (!string.IsNullOrEmpty(location))
		  {
			 AssignMarkerToNewLocation(location);
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