using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;


public partial class HomeTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LoginButton_Click(object sender, EventArgs e)
    {
        //Define ADO.NET objects
        SqlConnection connection = new SqlConnection(); //add using blocks
        connection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ToString();

        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "CheckLogin";

        command.Parameters.Add("@Email", SqlDbType.VarChar);
        command.Parameters["@Email"].Value = EmailText.Text;
        command.Parameters.Add("@Password", SqlDbType.VarChar);
        command.Parameters["@Password"].Value = PasswordText.Text;

        SqlDataReader reader;

        bool UserFound = false;
        Session["UserEmail"] = EmailText.Text;
        Session["UserID"] = "";

        //Try to open database and check if any records in UserDetails match with details entered by user
        try
        {
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Session["UserID"] = reader[0].ToString();
            }
            UserFound = reader.HasRows;
            reader.Close();                        
        }

        catch (Exception ex)
        {
            //To do: add to windows log
            Console.WriteLine("Some sort of error occured: " + ex.Message);
            LabelErr.Text = "Something went wrong =( Please try again";
            LabelErr.Visible = true;
        }

        finally
        {
            connection = null;
        }

        if (UserFound == false)
        {
            LabelErr.Text = "Your login was unsuccessful. Please check your login details and try again. If you are a new user, click below to register.";
            LabelErr.Visible = true;
        }

        else
        {
            Server.Transfer("UserHome.aspx");           
        }
    }

    protected void RegisterButton_Click(object sender, EventArgs e)
    {
        Server.Transfer("Register.aspx");
    }
}