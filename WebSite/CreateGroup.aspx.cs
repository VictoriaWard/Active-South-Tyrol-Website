using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreateGroup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // checks user is logged in
        if (Session["userID"] == null)
            Server.Transfer("login.aspx");
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        // initialize variables
        string groupID = string.Empty;
        int rowsAffected = 0;

        try
        {

            // add group and info to Groups table
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("INSERT INTO [Groups] (UserID, GroupName, Details, DateTimeStamp) VALUES (@UserID, @GroupName, @Details, @DateTimeStamp); SELECT SCOPE_IDENTITY()", connection))
                {
                    command.Parameters.AddWithValue("UserID", Session["UserID"]);
                    command.Parameters.AddWithValue("GroupName", GroupNameText.Text);
                    command.Parameters.AddWithValue("Details", GroupDetailsText.Text);
                    command.Parameters.AddWithValue("DateTimeStamp", DateTime.Now.ToString());
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        rowsAffected = reader.RecordsAffected;
                        while (reader.Read())
                        {
                            groupID = reader[0].ToString();
                        }
                        reader.Close();
                    }
                }
            }

            // add group to user list of groups
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("INSERT INTO [UserGroupsList] (UserID, GroupID) VALUES (@UserID, @GroupID)", connection))
                {
                    command.Parameters.AddWithValue("UserID", Session["UserID"]);
                    command.Parameters.AddWithValue("GroupID", groupID);
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

        // check database updated successfully
        if (rowsAffected == 2)
        {
            Session["GroupID"] = groupID;
            Server.Transfer("Group.aspx");
            // Add creator to event joined list
            // Add event to creator's list of events
            // Add label created successfully
        }

        else
        {
            Server.Transfer("CreateGroup.aspx");
            LabelErr.Text = "Something went wrong =( Please try again";
            LabelErr.Visible = true;
        }
    }

}