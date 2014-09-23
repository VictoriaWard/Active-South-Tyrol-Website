using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Group : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{

        // check user is logged in and there is avalid group id variable
        if ((Session["groupID"]) == null)
        {
            Server.Transfer("UserHome.aspx");  //check where to transfer and add err label
        }

        else
        {
            // initialize variables
            string groupName = string.Empty;
            string groupDetails = string.Empty;
            string groupMembers = string.Empty;

            try
            {
                // try to connect to db and retrieve event info
                using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SELECT GroupName, Details, Members FROM [Groups] WHERE GroupID = @GroupID", connection))
                    {
                        command.Parameters.AddWithValue("@GroupID", Session["groupID"]);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                groupName = reader[0].ToString();
                                groupDetails = reader[1].ToString();
                                groupMembers = reader[2].ToString();
                            }
                        }
                    }
                }

                // try to connect to db and retrieve list of members
                using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "GetGroupUserList";
                        command.Parameters.AddWithValue("@GroupID", Session["groupID"]);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //System.Diagnostics.Debug.Write(reader[1].ToString());
                                // add joined user buttons that link to user profiles
                                Button aGroup = new Button();
                                String userID = reader[0].ToString();
                                aGroup.Click += delegate(object sender2, EventArgs e2) { aGroup_Click(sender, e, userID); }; // add userid as third arg for button click handler for each group member
                                aGroup.Text = (reader[1].ToString()) + "\n" + (reader[2].ToString()) + "\n\n"; // set button text

                                Panel5.Controls.Add(aGroup);
                                Panel5.Controls.Add(new LiteralControl("&nbsp &nbsp"));
                                // to do: add user profile imgs to buttons
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                //error handling...
            }

            // set page info
            GroupNameText.Text = groupName;
            DetailsText.Text = groupDetails;
            Members.Text = groupMembers;
        }
    }

    protected void aGroup_Click(object sender, EventArgs e, string userID)
    {
        // check if user has clicked on own profile, if yes transfer to user home
        if (userID == (Session["UserID"]).ToString())
            Server.Transfer("UserHome.aspx");
        else
        {
            Session["userProfileID"] = userID;
            Server.Transfer("ViewUserProfile.aspx");
        }
    }
    
    protected void JoinGroupButton_Click(object sender, EventArgs e)
    {
        // add group to user group list
        int rowsAffected= 0;
        try
        {
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "JoinGroup";
                    command.Parameters.AddWithValue("@UserID", Session["userID"]);
                    command.Parameters.AddWithValue("@GroupID", Session["groupID"]);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        rowsAffected = reader.RecordsAffected;
                        reader.Close();
                    }
                }
            }
        }

        catch
        {
            // to do
        }

        if (rowsAffected == 2)
            Server.Transfer("UserGroups.aspx");

        //else
            //error label
    }

    protected void InviteButton_Click(object sender, EventArgs e)
    {
        // to do
    }

}
