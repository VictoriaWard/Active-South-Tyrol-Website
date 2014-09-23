using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FindFriends : System.Web.UI.Page
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
                string email = EmailText.Text;
                string firstName = FirstNameText.Text;
                string lastName = LastNameText.Text;

                try
                {
                    // retrieve user with matching email address if exists
                    // add link buttons to user profiles for users included in search results
                    using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand("SELECT Id, FirstName, LastName FROM [UserDetails] WHERE Email = @Email", connection))
                        {
                            command.Parameters.AddWithValue("@Email", email);
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //System.Diagnostics.Debug.Write(reader[1].ToString());

                                    Button user = new Button();
                                    String friendid = reader[0].ToString();
                                    user.Click += delegate(object sender2, EventArgs e2) { user_Click(sender, e, friendid); };

                                    user.Text = (reader[1].ToString()) + " " + (reader[2].ToString()) + "\n\n";

                                    Panel2.Controls.Add(user);
                                    Panel2.Controls.Add(new LiteralControl("&nbsp &nbsp"));
                                }
                            }
                        }
                    }

                    // retrieve users with matching first and last names if exist
                    // add link buttons to user profiles for users included in search results
                    using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand("SELECT Id, FirstName, LastName FROM [UserDetails] WHERE FirstName = @FirstName AND LastName = @LastName", connection))
                        {
                            command.Parameters.AddWithValue("@FirstName", firstName);
                            command.Parameters.AddWithValue("@LastName", lastName);
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //System.Diagnostics.Debug.Write(reader[1].ToString());

                                    Button user = new Button();
                                    String friendid = reader[0].ToString();
                                    user.Click += delegate(object sender2, EventArgs e2) { user_Click(sender, e, friendid); };

                                    user.Text = (reader[1].ToString()) + " " + (reader[2].ToString()) + "\n\n";

                                    Panel2.Controls.Add(user);
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
        

    protected void EmailSearchButton_Click(object sender, EventArgs e)
    {
        string email = EmailText.Text;
        bool rowsFound = false;
        try
        {
            // retrieve user with matching email address if exists
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT Id, FirstName, LastName FROM [UserDetails] WHERE Email = @Email", connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //while (reader.Read())
                        //{
                        //    //System.Diagnostics.Debug.Write(reader[1].ToString());

                        //    Button user = new Button();
                        //    String friendid = reader[0].ToString();
                        //    user.Click += delegate(object sender2, EventArgs e2) { user_Click(sender, e, friendid); };

                        //    user.Text = (reader[1].ToString()) + " " + (reader[2].ToString()) + "\n\n";

                        //    Panel2.Controls.Add(user);
                        //    Panel2.Controls.Add(new LiteralControl("&nbsp &nbsp"));
                        //}
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
            LabelNotFound.Text = "No users found. Try changing your search criteria.";
        }
    }

    protected void user_Click(object sender, EventArgs e, string friendid)
    {
        Session["userProfileID"] = friendid;
        Server.Transfer("ViewUserProfile.aspx");
    }

    protected void NameSearchButton_Click(object sender, EventArgs e)
    {
        string firstName = FirstNameText.Text;
        string lastName = LastNameText.Text;
        bool rowsFound = false;
        try
        {
            // retrieve user with matching email address if exists
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT Id, FirstName, LastName FROM [UserDetails] WHERE FirstName = @FirstName AND LastName = @LastName", connection))
                {
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
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
            LabelNotFound.Text = "No users found. Try changing your search criteria.";
        }
    }
    protected void EmailText_TextChanged(object sender, EventArgs e)
    {
        // remove values from name search
        FirstNameText.Text = string.Empty;
        LastNameText.Text = string.Empty;
    }
    protected void FirstNameText_TextChanged(object sender, EventArgs e)
    {
        // remove values from email search
        EmailText.Text = string.Empty;
    }
    protected void LastNameText_TextChanged(object sender, EventArgs e)
    {
        // remove values from email search
        EmailText.Text = string.Empty;
    }
}