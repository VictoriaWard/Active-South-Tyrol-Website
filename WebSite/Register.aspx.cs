using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    
    protected void Submitbtn_Click(object sender, EventArgs e)
    {
        //check if email address already registered
        bool registered = false;

        try
        {
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM [UserDetails] WHERE Email = @Email", connection))
                {
                    command.Parameters.AddWithValue("@Email", EmailText.Text);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            registered = reader.HasRows;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //To do: add to windows log
            Console.WriteLine("An error has occured: " + ex.Message);
            LabelErr.Text = "Couldn't connect to database. Please try again.";
            LabelErr.Visible = true;
        }

        //if email address found
        if (registered == true)
        {
            Console.WriteLine("Email address already registered");
            LabelErr.Text = "Email address already registered. Click below to log in.";
            LabelErr.Visible = true;
        }

        else
        {
            // check password re-entered correctly
            if (PasswordText.Text == PasswordReenter.Text)
            {
                //salt and hash password
                string salt;
                salt = CreateSalt(10);
                string safePass;
                safePass = GenerateSHA256Hash(PasswordText.Text, salt);

                int rowsAffected = 0;
                Session["UserEmail"] = EmailText.Text;

                //insert user reg details into db
                try
                {
                    using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand("INSERT INTO [UserDetails] (FirstName, LastName, Email, Password, IPAddress, DateTimeStamp, Salt) VALUES (@FirstName, @LastName, @Email, @Password, @IPAddress, @DateTimeStamp, @Salt)", connection))
                        {
                            command.Parameters.AddWithValue("@FirstName", FirstNameText.Text);
                            command.Parameters.AddWithValue("@LastName", LastNameText.Text);
                            command.Parameters.AddWithValue("@Email", EmailText.Text);
                            command.Parameters.AddWithValue("@Password", safePass);
                            command.Parameters.AddWithValue("@IPAddress", Request.UserHostAddress.ToString());
                            command.Parameters.AddWithValue("@DateTimeStamp", DateTime.Now.ToString());
                            command.Parameters.AddWithValue("@Salt", salt);
                            connection.Open();
                            rowsAffected = command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    //To do: add to windows log
                    Console.WriteLine("An error has occured: " + ex.Message);
                    LabelErr.Text = "Couldn't connect to database. Please try again.";
                    LabelErr.Visible = true;
                }

                //if details successfully added
                if (rowsAffected != 0)
                {
                    Server.Transfer("UserHome.aspx");
                }

                else
                {
                    string err;
                    Console.WriteLine("An error has occured: user details not inserted into database");
                    LabelErr.Text = "Unable to register your details. Please try again.";
                    LabelErr.Visible = true;
                    err = "An error has occured: user details not inserted into database" + DateTime.Now;
                    //using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"errorlog.txt", true))
                    //{
                    //    file.WriteLine(err);
                    //}
                }
            }

            //if password reenter doesn't match
            else
            {
                PasswordErrLabel.Visible = true;
                PasswordErrLabel.Text = "Please ensure that passwords match";
            }
        }
    }

    //generate salt method
    public String CreateSalt(int size)
    {
        var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
        var buff = new byte[size];
        rng.GetBytes(buff);
        return Convert.ToBase64String(buff);
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