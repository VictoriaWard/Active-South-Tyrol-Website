using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class UserHome : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // check if user is logged in and have user identity
            if ((Session["UserEmail"]) == null)
            {
                Server.Transfer("Login.aspx");
            }

            else
            {
                LoadUserDetails();
                LoadFriendRequests();
            }
        }
    }

    //load dynamic controls
    protected override void OnPreRender(EventArgs e)
    {
        UpdateNewsFeed();
    }

    private void ReportError(Label label)
    {
        label.Visible = true;
        label.Text = "Could not connect to database. Please try agagin. If problem persists, please connect our support team.";
    }

    private void LoadUserDetails() 
    {
        bool rowsAffected = false;
        try
        {
            // retrieve user info and update page
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT FirstName, LastName, AboutMeText, ProfileImage FROM [UserDetails] WHERE Email = @Email", connection))
                {
                    command.Parameters.AddWithValue("@Email", Session["UserEmail"].ToString());
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            litHeader.Text = reader[0].ToString() + " " + reader[1].ToString();
                            UserAboutMeText.Text = reader[2].ToString();
                            userProfileImage.ImageUrl = reader[3].ToString();
                        }   
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ReportError(LabelErr);
            //add to error log
        }
    }

    private void LoadFriendRequests()
    {
        bool rowsAffected = false;
        try
        {
            // check if user has any friend requests
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
            ReportError(LabelErr);
            //add to error log
        }
        // show if user has new friend requests
        if (rowsAffected == true)
            NewFriendRequestsButton.Visible = true;
    }


    private void UpdateNewsFeed()
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT Content, Date FROM [News] WHERE UserID = @UserID ORDER BY NewsID DESC", connection))
                {
                    command.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int i = 0;
                        while (reader.Read())
                        {
                            if (i < 10)
                            {
                                // if news item is image
                                if (reader[0].ToString().StartsWith("~/Data"))
                                {
                                    System.Web.UI.WebControls.Image newsImage = new System.Web.UI.WebControls.Image();
                                    newsImage.Width = 600;
                                    newsImage.AlternateText = "News feed image";
                                    newsImage.ImageUrl = reader[0].ToString();

                                    TextBox date = new TextBox();
                                    date.Text = reader[1].ToString() + ":";
                                    date.Width = 600;
                                    NewsPanel.Controls.Add(date);
                                    NewsPanel.Controls.Add(newsImage);
                                    NewsPanel.Controls.Add(new LiteralControl("&nbsp &nbsp"));


                                }

                                // if news item is not image
                                else
                                {
                                    TextBox newsItem = new TextBox();
                                    newsItem.Text = reader[1].ToString() + ":" + "\r\n" + reader[0].ToString() + "\r\n \r\n";
                                    newsItem.Width = 600;
                                    newsItem.Height = 80;
                                    newsItem.TextMode = TextBoxMode.MultiLine;
                                    NewsPanel.Controls.Add(newsItem);
                                    NewsPanel.Controls.Add(new LiteralControl("&nbsp &nbsp"));
                                }
                                i++;
                            }
                        }
                    }
                }
            }
        }

        //error updating news feed
        catch (Exception ex)
        {
            ReportError(LabelErr);
            //To do: add to windows log
        }
    }

    protected void SubmitStatusButton_Click(object sender, EventArgs e)
    {
        int rowsAffected = 0;
        // insert user news item into db
        try
        {
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("INSERT INTO [News] (UserID, Content, Date) VALUES (@UserID, @Content, @Date)", connection))
                {
                    command.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    command.Parameters.AddWithValue("@Content", StatusUpdateText.Text);
                    command.Parameters.AddWithValue("@Date", DateTime.Now.ToString());
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            ReportError(LabelErr);
            //To do: add to windows log
        }
        //if news item successfully added
        if (rowsAffected != 0)
        {
            StatusUpdateText.Text = string.Empty;
        }
        else
        {
            ReportError(LabelErr);
            //To do: add to windows log
        }
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

        int rowsAffected = 0;
        try
        {
            // update user about me info in user details table
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("UPDATE [UserDetails] SET AboutMeText = @AboutMeText WHERE Email = @userEmail", connection))
                {
                    command.Parameters.AddWithValue("@AboutMeText", UserAboutMeText.Text);
                    command.Parameters.AddWithValue("@userEmail", Session["UserEmail"].ToString());
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
            ReportError(LabelErr2);
            //To do: add to windows log
        }
        // check db updated
        if (rowsAffected == 0)
        {
            ReportError(LabelErr2);
            //To do: add to windows log
        }
    }

    protected void SubmitPhotoButton_Click(object sender, EventArgs e)
    {
        if (NewsUpload.HasFile)
        {
            SaveImage(NewsUpload);
        }

        string fileName = "~/Data/" + NewsUpload.FileName;
        int rowsAffected = 0;

        //add img file path to news db table
        try
        {
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("INSERT INTO [News] (UserID, Content, Date) VALUES (@UserID, @Content, @Date)", connection))
                {
                    command.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    command.Parameters.AddWithValue("@Content", fileName);
                    command.Parameters.AddWithValue("@Date", DateTime.Now.ToString());
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            ReportError(LabelErr);
            //To do: add to windows log
        }
        //if details could not be added to db
        if (rowsAffected == 0)
        {
            ReportError(LabelErr);
            //To do: add to windows log
        }
    }

    protected void SubmitProfileImageButton_Click(object sender, EventArgs e)
    {
        if (ProfileImageUpload.HasFile)
        {
            SaveImage(ProfileImageUpload);
        }

        string fileName = "~/Data/" + ProfileImageUpload.FileName;
        int rowsAffected = 0;

        //add img file path to db
        try
        {
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("UPDATE [UserDetails] SET ProfileImage = @profileImage WHERE Email = @userEmail", connection))
                {
                    command.Parameters.AddWithValue("@profileImage", fileName);
                    command.Parameters.AddWithValue("@userEmail", Session["UserEmail"]);
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
            ReportError(LabelErr);
            //To do: add to windows log
        }
        // check db updated
        if (rowsAffected != 0)
        {
            userProfileImage.ImageUrl = fileName;
        }
        else
        {
            ReportError(LabelErr);
            //To do: add to windows log
        }
    }

    protected void ViewFriendRequestsButton_Click(object sender, ImageClickEventArgs e)
    {
        Server.Transfer("UserFriends.aspx");
    }

    //auto rotate and save image
    private void SaveImage(FileUpload fileUpload)
    {
        byte[] imageData = new byte[fileUpload.PostedFile.ContentLength];
        fileUpload.PostedFile.InputStream.Read(imageData, 0, fileUpload.PostedFile.ContentLength);

        MemoryStream ms = new MemoryStream(imageData);
        System.Drawing.Image originalImage = System.Drawing.Image.FromStream(ms);

        if (originalImage.PropertyIdList.Contains(0x0112))
        {
            int rotationValue = originalImage.GetPropertyItem(0x0112).Value[0];
            switch (rotationValue)
            {
                case 1: // landscape, do nothing
                    break;

                case 8: // rotated 90 right
                    // de-rotate:
                    originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate270FlipNone);
                    break;

                case 3: // bottoms up
                    originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate180FlipNone);
                    break;

                case 6: // rotated 90 left
                    originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate90FlipNone);
                    break;
            }
        }

        originalImage.Save(Server.MapPath("~/Data/") + fileUpload.FileName);
    }

    protected void LogOutButton_Click(object sender, EventArgs e)
    {
        // destroy all session vars and transfer user to login page
        Session.Contents.RemoveAll();
        Server.Transfer("Login.aspx");
    }
}


