/*
 * Logout.aspx - Logout Page, automatically destroys session
 * Author: Linden
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
        Session.Abandon();
    }
}