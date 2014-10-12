using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EventSearch : System.Web.UI.Page
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
                string eventName = EventNameText.Text;

                try
                {
                    // retrieve events with matching name if exist
                    // add link buttons to event pages for events included in search results
                    using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand("SELECT EventID, EventDate, EventPlace FROM [Events] WHERE EventName = @EventName", connection))
                        {
                            command.Parameters.AddWithValue("@EventName", eventName);
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //System.Diagnostics.Debug.Write(reader[1].ToString());

                                    Button eventButton = new Button();
                                    String eventid = reader[0].ToString();
                                    eventButton.Click += delegate(object sender2, EventArgs e2) { eventButton_Click(sender, e, eventid); };

                                    eventButton.Text = eventName + " " + (reader[1].ToString()) + " " + (reader[2].ToString()) + "\n\n";

                                    Panel2.Controls.Add(eventButton);
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


    protected void EventNameSearchButton_Click(object sender, EventArgs e)
    {
        string eventName = EventNameText.Text;
        bool rowsFound = false;
        try
        {
            // retrieve events with matching name if exist
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT Id, EventDate, EventPlace FROM [UserDetails] WHERE EventName = @EventName", connection))
                {
                    command.Parameters.AddWithValue("@EventName", eventName);
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
            LabelNotFound.Text = "No events found. Try changing your search criteria.";
        }
    }

    protected void eventButton_Click(object sender, EventArgs e, string eventid)
    {
        Session["eventID"] = eventid;
        Server.Transfer("Event.aspx");
    }
    
}