using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace Testing247Media
{
    public partial class ReturnCoordinatesToCodeBehind : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLocations.Text))
            {
                string test = "This is not empty";
            }
        }

        [WebMethod]
        public string GetValue(string result)
        {
            return result;
        }
    }
}