using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class AllEvents : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // retrieve event info for all events from Events table
            // add link buttons for each event to a panel on the page
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT EventID, EventName, EventDate, EventPlace FROM [Events]", connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //System.Diagnostics.Debug.Write(reader[1].ToString());
                            Button anEvent = new Button();
                            String eventid = reader[0].ToString();
                            anEvent.Click += delegate(object sender2, EventArgs e2) { anEvent_Click(sender, e, eventid); }; //adds eventid as third argument to button click event handler for each button created
                            anEvent.Text = (reader[1].ToString()) + "\n" + (reader[2].ToString()) + "\n" + (reader[3].ToString()) + "\n\n"; // selects info from table to displayas button text
                            
                            Panel1.Controls.Add(anEvent);
                            Panel1.Controls.Add(new LiteralControl("&nbsp &nbsp"));
                            // to do: change buttons to include event imgs
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

    protected void anEvent_Click(object sender, EventArgs e, string eventid)
    {   
        // create session variable to identify and access event data and upload to event page for specific event user clicks on
        Session["eventID"] = eventid;
        Server.Transfer("Event.aspx");
    }
    
}