using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Events : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{

        // check if user logged in and there is a valid event id variable
        if ((Session["eventID"]) == null)
        {
            Server.Transfer("Login.aspx");  //check where to transfer
        }

        else
        {
            // initialize variables
            string eventName = string.Empty;
            string eventDate = string.Empty;
            string eventPlace = string.Empty;
            string eventDetails = string.Empty;
            string eventUsersJoined = string.Empty;

            try
            {
                // try to connect to db and retrieve event info
                using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SELECT EventName, EventDate, EventPlace, Details, Joined FROM [Events] WHERE EventID = @EventID", connection))
                    {
                        command.Parameters.AddWithValue("@EventID", Session["eventID"]);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                eventName = reader[0].ToString();
                                eventDate = reader[1].ToString();
                                eventPlace = reader[2].ToString();
                                eventDetails = reader[3].ToString();
                                eventUsersJoined = reader[4].ToString();
                            }
                        }
                    }
                }

                // try to connect to db and retrieve list of joined users
                using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "GetEventUserList";
                        command.Parameters.AddWithValue("@EventID", Session["eventID"]);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //System.Diagnostics.Debug.Write(reader[1].ToString());
                                // add joined user buttons that link to user profiles
                                Button anEventUser = new Button();
                                String userID = reader[0].ToString();
                                anEventUser.Click += delegate(object sender2, EventArgs e2) { anEventUser_Click(sender, e, userID); }; // add userid as third argument for button click handlers for each event button
                                anEventUser.Text = (reader[1].ToString()) + "\n" + (reader[2].ToString()) + "\n\n"; // add button text

                                Panel5.Controls.Add(anEventUser);
                                Panel5.Controls.Add(new LiteralControl("&nbsp &nbsp"));
                                // to do: add event imgs to buttons
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
            EventNameText.Text = eventName;
            EventDateText.Text = eventDate;
            EventPlaceText.Text = eventPlace;
            DetailsText.Text = eventDetails;
            EventJoined.Text = eventUsersJoined;
        }
    }

    protected void anEventUser_Click(object sender, EventArgs e, string userID)
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

    protected void JoinEventButton_Click(object sender, EventArgs e)
    {
        // add event to user event list
        int rowsAffected= 0;
        try
        {
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "JoinEvent";
                    command.Parameters.AddWithValue("@UserID", Session["userID"]);
                    command.Parameters.AddWithValue("@EventID", Session["eventID"]);
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
            Server.Transfer("UserEvents.aspx");

        //else
            //error label
    }

    protected void InviteButton_Click(object sender, EventArgs e)
    {
        // to do
    }

}