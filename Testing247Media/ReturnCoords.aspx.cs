using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;

namespace Testing247Media
{
    [ScriptService]
    public partial class ReturnCoords : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string test = txtLocations.Text;
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtLocations.Text))
            {
                string test = "This is not empty";
            }
        }

        [WebMethod]
        public static string HelloWorld(string msg)
        {
            string test = msg;
            return msg;
        }
    }
}