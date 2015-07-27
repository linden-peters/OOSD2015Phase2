/*
 * Registration.aspx - Customer Registration Form and Handler
 * Author: Geetha, Linden
 * Written: 2015/07/20
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{

    private Customer customer = new Customer();
    //bool userAvailablity = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(10000); // For testing exception handling
        if (Session["CustomerId"] != null)
        {
            lblFormHead.Text = "Edit Customer Registration";
            btnRegister.Text = "Update";
            btnRegister.Click += new EventHandler(this.btnUpdate_Click);
            int customerId = Convert.ToInt32(Session["CustomerId"]);
            txtUsername.Enabled = false;
            btnCheckUser.Visible = false;
            ReqCustUsername.Enabled = false;
            ReqCustPassword.Enabled = false;
            ReqCustConfirmPwd.Enabled = false;
            //customer = (Customer)Session["Customer"];
            try
            {
                customer = CustomerDB.GetCustomerDetails(customerId);
                if (!IsPostBack)
                    this.DisplayCustomerDetails();
            }
            catch (Exception ex)
            {
                lblError.Text = "Error: " + ex.Message;
            }
        }
        else
        {
            lblFormHead.Text = "New Customer Registration";
            btnRegister.Text = "Register";
            btnRegister.Click += new EventHandler(this.btnRegister_Click);
        }
    }

    private void DisplayCustomerDetails()
    {
        txtUsername.Text = customer.CustUsername;
        //txtEditUsername.Text = customer.CustUsername;
        txtPassword.Text = ""; // customer.CustPassword;
        //txtConfirmPassword.Text = customer.CustConfirmPassword;
        txtConfirmPassword.Text = ""; // customer.CustPassword;
        txtCustFirstName.Text = customer.CustFirstName;
        txtCustLastName.Text = customer.CustLastName;
        txtCustAddress.Text = customer.CustAddress;
        txtCustCity.Text = customer.CustCity;
        txtCustProv.Text = customer.CustProv;
        txtCustPostal.Text = customer.CustPostal;
        txtCustCountry.Text = customer.CustCountry;
        txtCustHomePhone.Text = customer.CustHomePhone;
        txtCustBusPhone.Text = customer.CustBusPhone;
        txtCustEmail.Text = customer.CustEmail;
        txtAgentId.Text = (customer.AgentId == -1 ? "" : customer.AgentId.ToString());
    }


    //Method to register the customer details
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        if(IsValid)
        {
            customer.CustUsername = txtUsername.Text;
            customer.CustPassword = txtPassword.Text;
            //customer.CustConfirmPassword = txtPassword.Text;             
            customer.CustFirstName = txtCustFirstName.Text;
            customer.CustLastName = txtCustLastName.Text;
            customer.CustAddress = txtCustAddress.Text;
            customer.CustCity = txtCustCity.Text;
            customer.CustProv = txtCustProv.Text;
            customer.CustPostal = txtCustPostal.Text;
            customer.CustCountry = txtCustCountry.Text;
            // Strip out all non-digits, then format as phone number
            customer.CustHomePhone = Regex.Replace(Regex.Replace(txtCustHomePhone.Text, @"\D", ""), @"^((\d{3})(\d{3})(\d{4}))$", "($2)$3-$4");
            customer.CustBusPhone = Regex.Replace(Regex.Replace(txtCustBusPhone.Text, @"\D", ""), @"^((\d{3})(\d{3})(\d{4}))$", "($2)$3-$4");
            customer.CustEmail = txtCustEmail.Text;
            customer.AgentId = (txtAgentId.Text == "" ? -1 : Convert.ToInt32(txtAgentId));
            try
            {
                if (CustomerDB.CheckUserAvailablity(txtUsername.Text))
                {
                    int customerId = CustomerDB.RegisterCustomer(customer);
                    if (customerId > 0)
                    {
                        Session["CustomerId"] = customerId; // New user is now logged in
                        Session["CustName"] = customer.CustFirstName + " " + customer.CustLastName;
                        Response.Redirect("Confirmation.aspx");
                    }
                    else
                        Response.Redirect("Registration.aspx");
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error: " + ex.Message;
            }
        }
    }

    //Method to clear the customer registration form
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtUsername.Text = "";
        txtPassword.Text = "";
        txtConfirmPassword.Text = "";
        txtCustFirstName.Text = "";
        txtCustLastName.Text = "";
        txtCustAddress.Text = "";
        txtCustCity.Text = "";
        txtCustProv.Text = "";
        txtCustPostal.Text = "";
        txtCustCountry.Text = "";
        txtCustHomePhone.Text = "";
        txtCustBusPhone.Text = "";
        txtCustEmail.Text = "";
        txtAgentId.Text = "";
    }

    protected void btnCheckUser_Click(object sender, EventArgs e)
    {
        if (txtUsername.Text == "")
        {
            lblUsernameInfo.Text = "No username entered. Please try again.";
        }
        else if (CustomerDB.CheckUserAvailablity(txtUsername.Text))
        {
            lblUsernameInfo.Text = "Username available. Proceed with registration.";
        }
        else
        {
            lblUsernameInfo.Text = "Username already exists. Please try other.";
        }        
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            customer.CustUsername = txtUsername.Text;
            if (txtPassword.Text != "" && txtConfirmPassword.Text != "" && txtPassword.Text == txtConfirmPassword.Text)
            {
                customer.CustPassword = txtPassword.Text;
                //customer.CustConfirmPassword = txtPassword.Text;
            }
            customer.CustFirstName = txtCustFirstName.Text;
            customer.CustLastName = txtCustLastName.Text;
            customer.CustAddress = txtCustAddress.Text;
            customer.CustCity = txtCustCity.Text;
            customer.CustProv = txtCustProv.Text;
            customer.CustPostal = txtCustPostal.Text;
            customer.CustCountry = txtCustCountry.Text;
            // Strip out all non-digits, then format as phone number
            customer.CustHomePhone = Regex.Replace(Regex.Replace(txtCustHomePhone.Text, @"\D", ""), @"^((\d{3})(\d{3})(\d{4}))$", "($2)$3-$4");
            customer.CustBusPhone = Regex.Replace(Regex.Replace(txtCustBusPhone.Text, @"\D", ""), @"^((\d{3})(\d{3})(\d{4}))$", "($2)$3-$4");
            customer.CustEmail = txtCustEmail.Text;
            customer.CustomerId = Convert.ToInt32(Session["CustomerId"]);
            customer.AgentId = (txtAgentId.Text == "" ? -1 : Convert.ToInt32(txtAgentId.Text));
            try
            {
                bool updateStatus = CustomerDB.UpdateCustomer(customer);
                if (updateStatus)
                {
                    Session["CustName"] = customer.CustFirstName + " " + customer.CustLastName; // In case they changed their name
                    Response.Redirect("Confirmation.aspx");
                }
                else
                    Response.Redirect("Registration.aspx");
            }
            catch (Exception ex)
            {
                lblError.Text = "Error: " + ex.Message;
            }
        }
    }
}