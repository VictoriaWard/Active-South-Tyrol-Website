using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SearchGroup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // check if user islogged in
        if (Session["userID"] == null)
            Server.Transfer("login.aspx");

        else
        {
            // if is postback, load search result buttons if applicable
            if (IsPostBack)
            {
                string groupName = GroupNameText.Text;

                try
                {
                    // retrieve groups with matching name if exist
                    // add link buttons to group pages for groups included in search results
                    using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand("SELECT GroupID FROM [Groups] WHERE GroupName = @GroupName", connection))
                        {
                            command.Parameters.AddWithValue("@GroupName", groupName);
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //System.Diagnostics.Debug.Write(reader[1].ToString());

                                    Button groupButton = new Button();
                                    String groupid = reader[0].ToString();
                                    groupButton.Click += delegate(object sender2, EventArgs e2) { groupButton_Click(sender, e, groupid); };

                                    groupButton.Text = groupName;

                                    Panel2.Controls.Add(groupButton);
                                    Panel2.Controls.Add(new LiteralControl("&nbsp &nbsp"));
                                }
                            }
                        }
                    }
                }

                catch
                {
                    //do something
                }
            }
        }

    }


    protected void GroupNameSearchButton_Click(object sender, EventArgs e)
    {
        string groupName = GroupNameText.Text;
        bool rowsFound = false;
        try
        {
            // retrieve groups with matching name if exist
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT GroupIDId FROM [UserDetails] WHERE GroupName = @GroupName", connection))
                {
                    command.Parameters.AddWithValue("@GroupName", groupName);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        rowsFound = reader.HasRows;
                    }
                }
            }
        }

        catch
        {
            //do something
        }

        if (rowsFound == false)
        {
            LabelNotFound.Visible = true;
            LabelNotFound.Text = "No groups found. Try changing your search criteria.";
        }
    }

    protected void groupButton_Click(object sender, EventArgs e, string groupid)
    {
        Session["groupID"] = groupid;
        Server.Transfer("Group.aspx");
    }
}