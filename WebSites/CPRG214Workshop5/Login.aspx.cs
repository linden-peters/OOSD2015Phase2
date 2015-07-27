/*
 * Login.aspx - Login Form and Handler
 * Author: Geetha, Linden
 * Written: 2015/07/20
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(10000); // For testing exception handling
        Customer login = new Customer();
        login.CustUsername = txtUsername.Text;
        login.CustPassword = txtPassword.Text;
        try
        {
            Customer customer = CustomerDB.GetLoginCredential(login);
            if (customer != null)
            {
                Session["CustomerId"] = customer.CustomerId;
                Session["Customer"] = customer;
                Session["CustName"] = customer.CustFirstName + " " + customer.CustLastName;
                Response.Redirect("Home.aspx");
            }
            else
            {
                lblError.Text = "Invalid Username or Password";
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "Error: " + ex.Message;
        }
    }
}