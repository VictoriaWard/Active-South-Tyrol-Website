using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreateNewEvent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // check if user is logged in
        if (Session["userID"] == null)
            Server.Transfer("login.aspx");
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        // initialize variables
        string eventID = string.Empty;
        int rowsAffected = 0;
        
        try
        {
            // add event and event info to Events table
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("INSERT INTO [Events] (UserID, EventName, EventDate, EventPlace, Details, DateTimeStamp) VALUES (@UserID, @EventName, @EventDate, @EventPlace, @Details, @DateTimeStamp); SELECT SCOPE_IDENTITY()", connection))
                {
                    command.Parameters.AddWithValue("UserID", Session["UserID"]);
                    command.Parameters.AddWithValue("EventName", EventNameText.Text);
                    command.Parameters.AddWithValue("EventDate", EventDateText.Text);
                    command.Parameters.AddWithValue("EventPlace", EventPlaceText.Text);
                    command.Parameters.AddWithValue("Details", EventDetailsText.Text);
                    command.Parameters.AddWithValue("DateTimeStamp", DateTime.Now.ToString());
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        rowsAffected = reader.RecordsAffected;
                        while (reader.Read())
                        {
                            eventID = reader[0].ToString();
                        }

                        reader.Close();
                    }
                }
            }

            // add event to user list of events
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("INSERT INTO [UserEventsList] (UserID, EventID) VALUES (@UserID, @EventID)", connection))
                {
                    command.Parameters.AddWithValue("UserID", Session["UserID"]);
                    command.Parameters.AddWithValue("EventID", eventID);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        rowsAffected += reader.RecordsAffected;
                        reader.Close();
                    }
                }
            }
        }
        
        catch (Exception ex)
        {
            //To do: add to windows log
            Console.WriteLine("An error has occured: " + ex.Message);
            LabelErr.Text = "Something went wrong =( Please try again";
            LabelErr.Visible = true;
        }

        // check if database updated successfully
        if (rowsAffected == 2)
        {
            Session["EventID"] = eventID;
            Server.Transfer("Event.aspx");
            // Add creator to event joined list
            // Add event to creator's list of events
            // Add label - event created successfully          
        }

        else
        {          
            Server.Transfer("CreateNewEvent.aspx");
            LabelErr.Text = "Something went wrong =( Please try again";
            LabelErr.Visible = true;
        }
    }

}