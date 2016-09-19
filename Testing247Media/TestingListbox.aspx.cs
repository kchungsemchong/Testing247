using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;


namespace Testing247Media
{
	public partial class TestingListbox : System.Web.UI.Page
	{
		private string cs = ConfigurationManager.ConnectionStrings["247"].ConnectionString;

		protected void Page_Load(object sender, EventArgs e)
		{
			using (SqlConnection con = new SqlConnection(cs))
			{
				SqlCommand cmd = new SqlCommand("Select * from Customer", con);
				con.Open();
				var dataReader = cmd.ExecuteReader();
				DataTable dtTest = new DataTable();
				dtTest.Load(dataReader);
				dtTest.Columns.Add("IsActivated", typeof(bool));
				DataColumn isActivated = dtTest.Columns["Activated"];
				foreach (DataRow row in dtTest.Rows)
				{
					int value = Convert.ToInt32(row["Activated"]);
					if (value == 0)
						row["IsActivated"] = false;

					if (value == 1)
						row["IsActivated"] = true;

				}
				dtTest.Columns.Remove("Activated");
				GridView1.DataSource = dtTest;
				GridView1.DataBind();
				con.Close();
			}
		}

		protected void btnTest_Click(object sender, EventArgs e)
		{
			//foreach (GridViewRow row in GridView1.Rows)
			//{
			//	CheckBox cb = (CheckBox)row.FindControl("chkActivated");
			//	if (cb != null && cb.Checked)
			//	{
			//		Response.Write("ID" + GridView1.DataKeys[row.RowIndex].Value
			//			+ "Activated" + row.Cells[3].Text + "</br>");
			//	}
			//}

			GridViewRow row = GridView1.SelectedRow;
			Response.Write("You selected" + row.Cells[1].Text);

		}



		protected void btnSave_Click(object sender, EventArgs e)
		{
			string strIsActivated = String.Empty;
			string strId = string.Empty;
			string updateQuery = String.Format("UPDATE Customer SET Activated = {0} WHERE Id = {1}", strIsActivated, strId);

			using (SqlConnection con = new SqlConnection(cs))
			{
				con.Open();
				SqlCommand cmd = new SqlCommand(updateQuery, con);
				cmd.ExecuteNonQuery();
			}
			btnSave.Visible = false;
		}

		protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
		{

		}

		protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{

		}

		protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			//if (e.CommandName == "IsActivated")
			//{
			//	int index = Convert.ToInt32(e.CommandArgument);
			//	var id = index;
			//}
		}

		protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
		{
			int index = GridView1.EditIndex = e.NewEditIndex;
			string value = GridView1.DataKeys[index].Value.ToString();

		}

		protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			GridViewRow row = GridView1.SelectedRow;
			Response.Write("You selected" + row.Cells[3].Text);
		}
	}
}