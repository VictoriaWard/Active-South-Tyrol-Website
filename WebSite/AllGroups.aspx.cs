using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AllGroups : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // retrieve group info for all groups from Groups table
            // add link buttons for each group to a panel on the page
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT GroupID, GroupName FROM [Groups]", connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //System.Diagnostics.Debug.Write(reader[1].ToString());
                            Button aGroup = new Button();
                            String groupid = reader[0].ToString();
                            aGroup.Click += delegate(object sender2, EventArgs e2) { anEvent_Click(sender, e, groupid); };  //adds eventid as third argument to button click event handler for each button created
                            aGroup.Text = (reader[1].ToString()) + "\n\n";  // selects info from table to displayas button text

                            Panel1.Controls.Add(aGroup);
                            Panel1.Controls.Add(new LiteralControl("&nbsp &nbsp"));
                            // to do: change buttons to include group imgs
                        }
                    }
                }
            }
        }

        catch (Exception ex)
        {
            //error handling...
        }
    }

    protected void anEvent_Click(object sender, EventArgs e, string groupid)
    {
        // create session variable to identify and access group data and upload to event page for specific group user clicks on
        Session["groupID"] = groupid;
        Server.Transfer("Group.aspx");
    }

}