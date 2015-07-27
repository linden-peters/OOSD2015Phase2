/*
 * Confirmation.aspx - Registration Confirmation
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
    Customer customer = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(10000); // For testing exception handling
        if (Session["CustomerId"] != null)
        {
            int customerId = Convert.ToInt32(Session["CustomerId"]);
            //customer = (Customer)Session["Customer"];
            try
            {
                customer = CustomerDB.GetCustomerDetails(customerId);
                this.DisplayCustomerDetails();
            }
            catch (Exception ex)
            {
                lblError.Text = "Error: " + ex.Message;
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    private void DisplayCustomerDetails()
    {
        lblUsername.Text = customer.CustUsername;
        lblFirstName.Text = customer.CustFirstName;
        lblLastName.Text = customer.CustLastName;
        lblAddress.Text = customer.CustAddress;
        lblCity.Text = customer.CustCity;
        lblProv.Text = customer.CustProv;
        lblPostal.Text = customer.CustPostal;
        lblCountry.Text = customer.CustCountry;
        lblHomePhone.Text = customer.CustHomePhone;
        lblBusPhone.Text = customer.CustBusPhone;
        lblEmail.Text = customer.CustEmail;
        lblCustomerId.Text = customer.CustomerId.ToString();
    }
}