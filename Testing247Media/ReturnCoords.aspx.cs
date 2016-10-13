using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using Testing247Media.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Testing247Media
{
    [ScriptService]
    public partial class ReturnCoords : System.Web.UI.Page
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
        protected static DataTable dtGeoLoc { get; set; }
        protected static List<Test> Message { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //RetrieveGeoLocations(1);
            //rptTest.DataSource = dtGeoLocation;
            //rptTest.DataBind();
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void HelloWorld(List<Test> name)
        {
            var markers = name;
            Message = markers;
            //List<Test> test = new List<Test>();
            //test = markers;

            //JavaScriptSerializer deserializer = new JavaScriptSerializer();
            //var results = deserializer.Deserialize<List<Test>>(postData);
            //Message = results;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static string GetGeoLocations(string msg)
        {
            List<Marker> markers = new List<Marker>();
            string jsonMarkers = string.Empty;
            DataTable dtGeo = new DataTable();
            ReturnCoords returnCoords = new ReturnCoords();
            returnCoords.RetrieveGeoLocations(1);
            markers = returnCoords.ConvertToMarker(dtGeoLoc);
            jsonMarkers = returnCoords.ConvertToJson(markers);
            //Message = msg;
            return jsonMarkers;
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

        private string ConvertToJson(List<Marker> markers)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string result = serializer.Serialize(markers);

            return result;
        }

        private List<Marker> ConvertToMarker(DataTable dtGeoLocations)
        {
            List<Marker> markers = new List<Marker>();
            foreach (DataRow row in dtGeoLocation.Rows)
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

        protected void btnTest_Click(object sender, EventArgs e)
        {

        }

        protected void btnGetGeoLocations_Click(object sender, EventArgs e)
        {
            List<Test> test = new List<Test>();
            test = Message;
        }
    }
}