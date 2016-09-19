using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Testing247Media
{
	public partial class TestCalendaraspx : System.Web.UI.Page
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			calBirthdate.Visible = false;
			btnShow.Text = "Display calendar";
		}

		protected void calBirthdate_SelectionChanged(object sender, EventArgs e)
		{
			lblBirthdate.Text = "The selected date is " + calBirthdate.SelectedDate.ToString();
		}

		protected void btnShow_Click(object sender, EventArgs e)
		{
			if (btnShow.Text == "Display calendar")
			{
				calBirthdate.Visible = true;
				btnShow.Text = "Hide Calendar";
				return;
			}
			if (btnShow.Text == "Hide calendar")
			{
				calBirthdate.Visible = false;
			}
		}


	}
}