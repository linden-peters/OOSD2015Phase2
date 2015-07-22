/*
 * Author : Sunny Xie
 * Date: July 20, 2015
 * Usage: user purchased packages and products file c#.
 * 
 * */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    decimal allTotalSpent;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CustomerId"] == null || Session["CustName"] == null)
        {
            Response.Redirect("Login.aspx");
            return;
        }

        // add the root node to the TreeView.
        TreeNode rootNode = new TreeNode("[" + Session["CustName"].ToString() + "]'s Travel Packages:");
        tvProducts.Nodes.Add(rootNode);
        int customerId = Convert.ToInt32(Session["CustomerId"]);
        PopulatePackages(rootNode, customerId);
        // tvProducts.ChildNodes.Add(NewNode);
    }


    void PopulatePackages(TreeNode node, int customerId)
    {
        SqlCommand sqlQuery = new SqlCommand(
            "   SELECT b.PackageId,PkgName, SUM(bd.BasePrice ) as Totalamount "
        + "FROM BookingDetails bd inner join Bookings b on bd.BookingId = b.BookingId "
        + "inner join Packages pck on b.PackageId = pck.PackageId "
        + "where b.CustomerId = @customeId "
        + "and b.Packageid is not null group by b.PackageId,PkgName");

        sqlQuery.Parameters.AddWithValue("@customeId", customerId);
        DataSet resultSet;
        resultSet = RunQuery(sqlQuery);
        if (resultSet.Tables.Count > 0)
        {
            allTotalSpent = 0.0m;
            foreach (DataRow row in resultSet.Tables[0].Rows)
            {
                TreeNode NewNode = new TreeNode("<div style='color: purple'>" + row["PkgName"].ToString() + ",       Total Price: " +
                    String.Format("{0:C2}" + "</div>", row["Totalamount"]),
                    row["PackageId"].ToString());
                NewNode.PopulateOnDemand = false;
                NewNode.SelectAction = TreeNodeSelectAction.Expand;
                node.ChildNodes.Add(NewNode);

                int pkgId = Convert.ToInt32(NewNode.Value);

                List<Bookings> bookings = BookingDB.GetBookingsByPkg(pkgId, customerId);
                AddBookingsToTree(NewNode, bookings);
                allTotalSpent += Convert.ToDecimal(row["Totalamount"]);
            }

            lblInfo.Text = "So far, you have spent " + allTotalSpent.ToString("c") + " in total, Thank you!";
        }
    }

    // addd bookings tree nodes.
    void AddBookingsToTree(TreeNode node, List<Bookings> bookings)
    {
        int bId = 0;
        foreach (Bookings b in bookings)
        {
            ++bId;
            TreeNode NewNode = new TreeNode("<div style='color: #CC6600'>Booking " + bId.ToString() + ": &nbsp;&nbsp;" + b.BookingDate.ToShortDateString() + "</div>",
                       b.BookingId.ToString());
            NewNode.PopulateOnDemand = false;
            NewNode.SelectAction = TreeNodeSelectAction.Expand;
            node.ChildNodes.Add(NewNode);

            AddProductToTree(NewNode);
        }
    }

    // addd product to leaf tree.
    void AddProductToTree(TreeNode node)
    {
        int bookingId = 0;
        if (!Int32.TryParse(node.Value, out bookingId))
        {
            return;
        }

        List<SupperProduct> lstSupPord = BookingDB.GetProductsByBookingId(bookingId);

        foreach (SupperProduct sp in lstSupPord)
        {
            TreeNode NewNode = new
               TreeNode(sp.SuppName + ", " + sp.ProdName + ",       Price: " + sp.BasePrice.ToString("c"));
            NewNode.PopulateOnDemand = false;
            NewNode.SelectAction = TreeNodeSelectAction.None;
            node.ChildNodes.Add(NewNode);
        }
    }

    void GenerateLeafCategories(TreeNode node)
    {
        SqlCommand sqlQuery = new SqlCommand(
            "   SELECT bd.BookingId, b.PackageId,PkgName, SUM(bd.BasePrice ) as Totalamount "
        + "FROM BookingDetails bd inner join Bookings b on bd.BookingId = b.BookingId "
        + "inner join Packages pck on b.PackageId = pck.PackageId "
        + "where b.CustomerId = 135 "
        + "and b.Packageid is not null group by b.PackageId,PkgName");
        DataSet resultSet;
        resultSet = RunQuery(sqlQuery);
        if (resultSet.Tables.Count > 0)
        {
            foreach (DataRow row in resultSet.Tables[0].Rows)
            {
                TreeNode NewNode = new TreeNode(row["bd.BookingId"].ToString() + ", " + row["PkgName"].ToString() + ", " + row["Totalamount"],
                    row["PackageId"].ToString());
                NewNode.PopulateOnDemand = false;
                NewNode.SelectAction = TreeNodeSelectAction.Expand;
                node.ChildNodes.Add(NewNode);
            }
        }
    }

    private DataSet RunQuery(SqlCommand sqlQuery)
    {

        SqlConnection DBConnection = TravelExpertsDB.GetConnection();

        SqlDataAdapter dbAdapter = new SqlDataAdapter();
        dbAdapter.SelectCommand = sqlQuery;
        sqlQuery.Connection = DBConnection;
        DataSet resultsDataSet = new DataSet();
        try
        {
            dbAdapter.Fill(resultsDataSet);
        }
        catch
        {
            // labelStatus.Text = "Unable to connect to SQL Server.";
        }
        return resultsDataSet;
    }
    protected void tvProducts_PreRender(object sender, EventArgs e)
    {

    }

    // go home button click method
    protected void btnHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
}