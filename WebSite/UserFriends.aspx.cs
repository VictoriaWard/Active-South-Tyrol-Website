using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserFriends : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            // retrieve user friends and add profile buttons for friends to friends panel
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "GetUserFriends";
                    command.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //System.Diagnostics.Debug.Write(reader[1].ToString());

                            Button aFriend = new Button();
                            String friendid = reader[0].ToString();
                            aFriend.Click += delegate(object sender2, EventArgs e2) { aFriend_Click(sender, e, friendid); };

                            aFriend.Text = (reader[1].ToString()) + "\n" + (reader[2].ToString()) + "\n\n";

                            Panel4.Controls.Add(aFriend);
                            Panel4.Controls.Add(new LiteralControl("&nbsp &nbsp"));
                        }
                    }
                }
            }


            // checkif user has any friend requests and add profile link buttons to page
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "GetUserFriendRequests";
                    command.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //System.Diagnostics.Debug.Write(reader[1].ToString());

                            Button aFriendRequest = new Button();
                            String friendid = reader[0].ToString();
                            aFriendRequest.Click += delegate(object sender2, EventArgs e2) { aFriend_Click(sender, e, friendid); };

                            aFriendRequest.Text = (reader[1].ToString()) + "\n" + (reader[2].ToString()) + "\n\n";

                            Panel3.Controls.Add(aFriendRequest);
                            Panel3.Controls.Add(new LiteralControl("&nbsp &nbsp"));
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
    

    protected void aFriend_Click(object sender, EventArgs e, string userid)
    {
        Session["userProfileID"] = userid;
        Server.Transfer("ViewUserProfile.aspx");
    }

    protected void aFriendRequest_Click(object sender, EventArgs e, string userid)
    {
        Session["userProfileID"] = userid;
        Server.Transfer("ViewUserProfile.aspx");
    }

    protected void FriendSearchButton_Click(object sender, EventArgs e)
    {
        Server.Transfer("FindFriends.aspx");
    }

    protected void FriendInviteButton_Click(object sender, EventArgs e)
    {
        // to do
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        // destroy all session vars and transfer user to login page
        Session.Contents.RemoveAll();
        Server.Transfer("Login.aspx");
    }
}