using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewUserProfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // check user logged in
        if (Session["userProfileID"] == null)
            Server.Transfer("UserHome.aspx");
         
        else
        {
                // initailize variables
                string firstName = string.Empty;
                string lastName = string.Empty;
                string userCurrentStatus = string.Empty;
                string userAboutMe = string.Empty;
                string userProfileID = (Session["UserProfileID"]).ToString();
                string userID = (Session["UserID"]).ToString();
                bool friendRequestFound = false;
                bool friendFound = false;

                try
                {
                    // check if logged in user has friend request from user being viewed
                    using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand("SELECT * FROM [Friends] WHERE FriendID = @UserID AND UserID = @UserProfileID AND Confirmed = 0", connection))
                        {
                            command.Parameters.AddWithValue("@UserID", userID);
                            command.Parameters.AddWithValue("@UserProfileID", userProfileID);
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    friendRequestFound = reader.HasRows;
                                }
                            }
                        }
                    }

                    // check if logged in user is already friends with user being viewed
                    using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand("SELECT * FROM [Friends] WHERE FriendID = @UserID AND UserID = @UserProfileID AND Confirmed = 1", connection))
                        {
                            command.Parameters.AddWithValue("@UserID", userID);
                            command.Parameters.AddWithValue("@UserProfileID", userProfileID);
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    friendFound = reader.HasRows;
                                }
                            }
                        }
                    }
                    
                    // retrieve user info for user being viewed
                    using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand("SELECT FirstName, LastName, CurrentStatus, AboutMeText FROM [UserDetails] WHERE Id = @UserProfileID", connection))
                        {
                            command.Parameters.AddWithValue("@UserProfileID", userProfileID);
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    firstName = reader[0].ToString();
                                    lastName = reader[1].ToString();
                                    userCurrentStatus = reader[2].ToString();
                                    userAboutMe = reader[3].ToString();
                                }
                            }
                        }
                    }
                }

                catch (Exception ex)
                {
                    //error handling...
                }

                // add accept friend request button if user has friend request from user viewing profile
                if (friendRequestFound == true)
                {
                    AddFriendButton.Visible = false;
                    AcceptFriendRequestButton.Visible = true;
                }

                // remove add friend button if already friends
                if (friendFound == true)
                    AddFriendButton.Visible = false;

                // add user info to page
                UserNameText.Text = firstName + " " + lastName;
                CurrentStatusText.Text = userCurrentStatus;
                UserAboutMeText.Text = userAboutMe;
            }
        }        

    protected void AddFriendButton_Click(object sender, EventArgs e)
    {
        string eventID = string.Empty;
        int rowsAffected = 0;

        try
        {
            // add friend request to Friends table, friendship will be one way and unconfirmed until accepted
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("INSERT INTO [Friends] (UserID, FriendID) VALUES (@UserID, @FriendID)", connection))
                {
                    command.Parameters.AddWithValue("UserID", Session["UserID"]);
                    command.Parameters.AddWithValue("FriendID", Session["userProfileID"]);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        rowsAffected = reader.RecordsAffected;
                        reader.Close();
                    }
                }
            }
        }

        catch (Exception ex)
        {
            //To do: add to windows log
            Console.WriteLine("An error has occured: " + ex.Message);
            //LabelErr.Text = "Something went wrong =( Please try again";
            //LabelErr.Visible = true;
        }

        if (rowsAffected == 1)
        {
            //Add label "friend added"
        }

        else
        {
            //LabelErr.Text = "Something went wrong =( Please try again";
            //LabelErr.Visible = true;
        }
    }

    protected void Photos_Click(object sender, EventArgs e)
    {
        // to do
    }

    protected void Friends_Click(object sender, EventArgs e)
    {
        // to do
    }

    protected void AcceptFriendRequestButton_Click(object sender, EventArgs e)
    {
        int rowsAffected = 0;

        try
        {
            // update Friends table by adding 2 way friendship and confirm friendship by changing bool value to 1
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "AcceptFriend";
                    command.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    command.Parameters.AddWithValue("@FriendID", Session["userProfileID"]);
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
            // add success msg
            Server.Transfer("UserFriends.aspx");

        //else add fail to add msg
    }
}