using System;
using System.Collections.Generic;
using System.Linq;
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
        SqlDataSource testDataSource = new SqlDataSource(); // add using blocks
        testDataSource.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UsersConnectionString1"].ToString();

        testDataSource.InsertCommandType = SqlDataSourceCommandType.Text;
        testDataSource.InsertCommand = "INSERT INTO [UserDetails] (UserName, FirstName, LastName, Email, Password, IPAddress, DateTimeStamp) VALUES (@UserName, @FirstName, @LastName, @Email, @Password, @IPAddress, @DateTimeStamp)";

        testDataSource.InsertParameters.Add("UserName", UserNameText.Text);
        testDataSource.InsertParameters.Add("FirstName", FirstNameText.Text);
        testDataSource.InsertParameters.Add("LastName", LastNameText.Text);
        testDataSource.InsertParameters.Add("Email", EmailText.Text);
        testDataSource.InsertParameters.Add("Password", PasswordText.Text);
        testDataSource.InsertParameters.Add("IPAddress", Request.UserHostAddress.ToString());
        testDataSource.InsertParameters.Add("DateTimeStamp", DateTime.Now.ToString());

        int rowsAffected = 0;
        Session["UserEmail"] = EmailText.Text;

        try
        {
            rowsAffected = testDataSource.Insert();
        }

        catch (Exception ex)
        {
            //To do: add to windows log
            Console.WriteLine("An error has occured: " + ex.Message);
            LabelErr.Text = "Something went wrong =( Please try again";
            LabelErr.Visible = true;
        }

        finally
        {
            testDataSource = null;
        }

        // check if database updated successfully
        if (rowsAffected != 0)
        {
            Server.Transfer("UserHome.aspx");
        }

        else
        {
            Server.Transfer("Login.aspx");
        }

    }

    protected void GotoLoginButton_Click(object sender, EventArgs e)
    {
        Server.Transfer("Login.aspx");
    }
}