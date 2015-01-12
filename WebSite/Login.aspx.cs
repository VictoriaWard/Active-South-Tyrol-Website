using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;


public partial class HomeTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LoginButton_Click(object sender, EventArgs e)
    {
        //get user salt
        string salt = "";
        try
        {
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT Salt FROM [UserDetails] WHERE Email = @Email", connection))
                {
                    command.Parameters.AddWithValue("@Email", EmailText.Text);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            salt = reader[0].ToString();
                        }
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

        //if salt found in db
        if (salt != "")
        {
            //get hashed password
            string hashedPass;
            hashedPass = GenerateSHA256Hash(PasswordText.Text, salt);

            //set session vars
            bool userFound = false;
            Session["UserEmail"] = EmailText.Text;
            Session["UserID"] = "";

            //check login details and get user ID
            try
            {
                using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "CheckLogin";
                        command.Parameters.Add("@Email", SqlDbType.VarChar);
                        command.Parameters["@Email"].Value = EmailText.Text;
                        command.Parameters.Add("@Password", SqlDbType.VarChar);
                        command.Parameters["@Password"].Value = hashedPass;

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Session["UserID"] = reader[0].ToString();
                            }

                            userFound = reader.HasRows;
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

            //if login details not found in db
            if (userFound == false)
            {
                LabelErr.Text = "Your login was unsuccessful. Please check your login details and try again. If you are a new user, click below to register.";
                LabelErr.Visible = true;
            }

            //if user login details found
            else
            {
                Server.Transfer("UserHome.aspx");
            }

        }

        //if salt not found in db
        else
        {
            LabelErr.Text = "Your login was unsuccessful. Please check your login details and try again. If you are a new user, click below to register.";
            LabelErr.Visible = true;
        }
    }

    //hash password method
    public String GenerateSHA256Hash(String input, String salt)
    {
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
        System.Security.Cryptography.SHA256Managed sha256hashstring = new System.Security.Cryptography.SHA256Managed();
        byte[] hash = sha256hashstring.ComputeHash(bytes);

        return ByteArrayToHexString(hash);
    }

    //byte array to hex string converter method
    public static string ByteArrayToHexString(byte[] ba)
    {
        StringBuilder hex = new StringBuilder(ba.Length * 2);
        foreach (byte b in ba)
            hex.AppendFormat("{0:x2}", b);
        return hex.ToString();
    }
}