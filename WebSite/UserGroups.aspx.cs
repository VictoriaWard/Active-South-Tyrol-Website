using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserGroups : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // check user logged in
        if (Session["userID"] == null)
            Server.Transfer("login.aspx");

        else
        {
            try
            {
                // retrieve info for all groups user is member of
                using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "GetUserGroups";
                        command.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //System.Diagnostics.Debug.Write(reader[1].ToString());

                                Button aGroup = new Button();
                                String groupid = reader[0].ToString();
                                aGroup.Click += delegate(object sender2, EventArgs e2) { anEvent_Click(sender, e, groupid); };

                                aGroup.Text = (reader[1].ToString()) + "\n\n";

                                Panel3.Controls.Add(aGroup);
                                Panel3.Controls.Add(new LiteralControl("&nbsp &nbsp"));
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
    }

    protected void anEvent_Click(object sender, EventArgs e, string groupid)
    {
        Session["groupID"] = groupid;
        Server.Transfer("Group.aspx");
    }

    protected void NewGroupButton_Click(object sender, EventArgs e)
    {
        Server.Transfer("CreateGroup.aspx");
    }

    protected void GroupBrowseButton_Click(object sender, EventArgs e)
    {
        Server.Transfer("AllGroups.aspx");
    }

    protected void GroupSearchButton_Click(object sender, EventArgs e)
    {
        // to do
    }

    protected void GroupInviteButton_Click(object sender, EventArgs e)
    {
        // to do
    }
}