/*
 * Setup.aspx - DB Initializer
 * Author: Linden
 * Written: 2015/07/27
 */
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            using (StreamReader fsReader = new StreamReader(Server.MapPath("SQL/TravelExperts_DemoSeed-MSSQL.sql")))
            {
                lblOutput.Text += "Successfully opened seedfile.<br />";
                using (SqlConnection dbConn = TravelExpertsDB.GetInitConn())
                {
                    string s = null;
                    dbConn.Open();
                    while (!fsReader.EndOfStream)
                    {
                        StringBuilder qryParsed = new StringBuilder();
                        while (!fsReader.EndOfStream)
                        {
                            if ((s = fsReader.ReadLine()) != null && s.ToUpper().Trim().Equals("GO"))
                                break;
                            qryParsed.AppendLine(s);
                        }
                        lblOutput.Text += "QRY: " + qryParsed.ToString() + "<br />";
                        using (SqlCommand cmdOutput = new SqlCommand(qryParsed.ToString(), dbConn))
                        {
                            try
                            {
                                cmdOutput.ExecuteNonQuery();
                                lblOutput.Text += "[PASS]<br />";
                            }
                            catch (Exception ex)
                            {
                                lblOutput.Text += "[FAIL] " + ex.Message + "<br />";
                            }
                        }
                    }
                    dbConn.Close();
                }
            }
        }
        catch (Exception ex)
        {
            lblOutput.Text += "DB Init Failed: " + ex.Message + "<br />";
        }
    }
}