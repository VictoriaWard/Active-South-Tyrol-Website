using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Events : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // check if user logged in 
        if (Session["userID"] == null)
            Server.Transfer("login.aspx");

        else
        {
            try
            {
                // connect to db and get event info for user events
                using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "GetUserEvents";
                        command.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //System.Diagnostics.Debug.Write(reader[1].ToString());

                                Button anEvent = new Button();
                                String eventid = reader[0].ToString();
                                anEvent.Click += delegate(object sender2, EventArgs e2) { anEvent_Click(sender, e, eventid); };

                                anEvent.Text = (reader[1].ToString()) + "\n" + (reader[2].ToString()) + "\n" + (reader[3].ToString()) + "\n\n";

                                Panel3.Controls.Add(anEvent);
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

    protected void anEvent_Click(object sender, EventArgs e, string eventid)
    {   
        // create session variable to identify event info for event page for specific event user clicks on
        Session["eventID"] = eventid;     
        Server.Transfer("Event.aspx");    
    }
    
    protected void NewEventButton_Click(object sender, EventArgs e)
    {
        Server.Transfer("CreateNewEvent.aspx");
    }

    protected void EventBrowseButton_Click(object sender, EventArgs e)
    {
        Server.Transfer("AllEvents.aspx");
    }

    protected void EventSearchButton_Click(object sender, EventArgs e)
    {
        // to do
    }

    protected void EventInviteButton_Click(object sender, EventArgs e)
    {
        // to do
    }

}