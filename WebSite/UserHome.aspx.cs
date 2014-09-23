using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class UserHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            // check if user is logged in and have user identity
            if ((Session["UserEmail"]) == null)
            {
                Server.Transfer("Login.aspx");
            }

            else
            {
                // initialize variables
                string firstName = string.Empty;
                string lastName = string.Empty;
                string userCurrentStatus = string.Empty;
                string userAboutMe = string.Empty;
                string userEmail = (Session["UserEmail"]).ToString();
                bool rowsAffected = false;

                try
                {
                    // retrieve user info 
                    using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand("SELECT FirstName, LastName, CurrentStatus, AboutMeText FROM [UserDetails] WHERE Email = @Email", connection))
                        {
                            command.Parameters.AddWithValue("@Email", userEmail);
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


                    // checkif user has any friend requests
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
                                    rowsAffected = reader.HasRows;
                                }
                            }
                        }
                    }
                }

                catch (Exception ex)
                {
                    //error handling...
                }

                // update user info
                UserNameText.Text = firstName + " " + lastName;
                CurrentStatusText.Text = userCurrentStatus;
                UserAboutMeText.Text = userAboutMe;

                // show if user has new friend requests
                if (rowsAffected == true)
                    NewFriendRequestsButton.Visible = true;
            }
        }   
    }

    protected void SubmitStatusButton_Click(object sender, EventArgs e)
    {
        string userEmail = (Session["UserEmail"]).ToString();
        string userUpdateStatus = StatusUpdateText.Text;
        int rowsAffected = 0;

        try
        {
            // update status in user details table
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("UPDATE [UserDetails] SET CurrentStatus = @CurrentStatus WHERE Email = @userEmail", connection))
                {
                    command.Parameters.AddWithValue("@CurrentStatus", userUpdateStatus);
                    command.Parameters.AddWithValue("@userEmail", userEmail);
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
            Console.WriteLine("Some sort of error occured: " + ex.Message);
            LabelErr.Text = "Something went wrong =( Please try again";
            LabelErr.Visible = true;
        }

        // check db updated successfully
        if (rowsAffected == 0)
        {
            LabelErr.Text = "Your update was unsuccessful. Please try again later.";
            LabelErr.Visible = true;
        }

        else
        {
            // update user status on page
            CurrentStatusText.Text = userUpdateStatus;
            StatusUpdateText.Text = string.Empty;
        }
    }
    
    protected void ChangeProfile_Click(object sender, EventArgs e)
    {
        Server.Transfer("AddNewProfileImage.aspx");
    }
    
    protected void EditAboutMe_Click(object sender, EventArgs e)
    {
        UserAboutMeText.ReadOnly = false;
        SetFocus(UserAboutMeText);
        EditAboutMe.Visible = false;
        AboutMeSubmit.Visible = true;
    }

    protected void AboutMeSubmit_Click(object sender, EventArgs e)
    {
        UserAboutMeText.ReadOnly = true;
        EditAboutMe.Visible = true;
        AboutMeSubmit.Visible = false;

        string userEmail = (Session["UserEmail"]).ToString();
        string AboutMeText = UserAboutMeText.Text;
        int rowsAffected = 0;

        try
        {
            // update user about me info in user details table
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("UPDATE [UserDetails] SET AboutMeText = @AboutMeText WHERE Email = @userEmail", connection))
                {
                    command.Parameters.AddWithValue("@AboutMeText", AboutMeText);
                    command.Parameters.AddWithValue("@userEmail", userEmail);
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
            Console.WriteLine("Some sort of error occured: " + ex.Message);
            LabelErr2.Text = "Something went wrong =( Please try again";
            LabelErr2.Visible = true;
        }

        // check db updated
        if (rowsAffected == 0)
        {
            LabelErr2.Text = "Your update was unsuccessful. Please try again later.";
            LabelErr2.Visible = true;
            UserAboutMeText.Text = string.Empty;
        }

        else
        {
            // update user about me on page
            UserAboutMeText.Text = AboutMeText;
        }
    }

    protected void MyEvents_Click(object sender, EventArgs e)
    {
        Server.Transfer("UserEvents.aspx");
    }

    protected void MyGroups_Click(object sender, EventArgs e)
    {
        Server.Transfer("UserGroups.aspx");
    }

    protected void MyPhotos_Click(object sender, EventArgs e)
    {
        // to do
    }

    protected void MyFriends_Click(object sender, EventArgs e)
    {
        Server.Transfer("UserFriends.aspx");
    }

    protected void SubmitPhotoButton_Click(object sender, EventArgs e)
    {
        //to do
    }

    protected void ViewFriendRequestsButton_Click(object sender, ImageClickEventArgs e)
    {
        Server.Transfer("UserFriends.aspx");
    }
    
}