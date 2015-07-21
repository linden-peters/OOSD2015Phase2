/*
 * Author : Sunny Xie
 * Date: July 20, 2015
 * Usage: user purchased packages and products file c#.
 * 
 * */
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BookingDB
/// </summary>
/// 

// the mixed structure for the TreeView's leaf node, 
public struct SupperProduct
{
    public string SuppName;
    public string ProdName;
    public decimal BasePrice;
};

public class BookingDB
{
    public BookingDB()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public decimal GetBookingPrice(int customerId, int packageId)
    {
        //SELECT PackageId, SUM(bd.BasePrice ) as totalamount 
        //                        FROM BookingDetails bd, Bookings b
        //                        WHERE bd.BookingId = b.BookingId                                     
        //                               AND b.CustomerId = 135 
        //                               and Packageid is not null group by PackageId

        return 0.0m;
    }


    // get all products by booking id
    public static List<SupperProduct> GetProductsByBookingId(int bookingId)
    {
        List<SupperProduct> suppProds = new List<SupperProduct>();
        SqlConnection dbConn = TravelExpertsDB.GetConnection();
        string qrySelect =
            " select SupName, ProdName, BasePrice from BookingDetails bd inner join Products_Suppliers ps "
            + "on bd.ProductSupplierId = ps.ProductSupplierId "
            + "inner join Products pr on ps.ProductId = pr.ProductId "
            + " inner join Suppliers su on ps.SupplierId=su.SupplierId where BookingId = @bookingId";
        SqlCommand cmdSelect = new SqlCommand(qrySelect, dbConn);

        cmdSelect.Parameters.AddWithValue("@bookingId", bookingId);
        // cmdSelect.Parameters.AddWithValue("@custId", custId);
        try
        {
            dbConn.Open();
            SqlDataReader dbReader = cmdSelect.ExecuteReader();
            while (dbReader.Read())
            {
                SupperProduct bsk = new SupperProduct();
                bsk.SuppName = Convert.ToString(dbReader["SupName"]);
                bsk.ProdName = Convert.ToString(dbReader["ProdName"]);
                bsk.BasePrice = Convert.ToDecimal(dbReader["BasePrice"]);
                suppProds.Add(bsk);
            }
        }
        catch (SqlException ex)
        {
            throw ex;
        }
        finally
        {
            dbConn.Close();
        }
        return suppProds;

    }

    // get all Bookings from database, sorted for display
    public static List<Bookings> GetBookingsByPkg(int pKgId, int custId)
    {
        List<Bookings> bookings = new List<Bookings>();
        SqlConnection dbConn = TravelExpertsDB.GetConnection();
        string qrySelect =
            "select * from Bookings b where PackageId=@pKgId and CustomerId = @custId ";
        SqlCommand cmdSelect = new SqlCommand(qrySelect, dbConn);

        cmdSelect.Parameters.AddWithValue("@pKgId", pKgId);
        cmdSelect.Parameters.AddWithValue("@custId", custId);
        try
        {
            dbConn.Open();
            SqlDataReader dbReader = cmdSelect.ExecuteReader();
            while (dbReader.Read())
            {
                Bookings bsk = new Bookings();
                bsk.CustomerId = Convert.ToInt32(dbReader["CustomerId"]);
                bsk.BookingId = Convert.ToInt32(dbReader["BookingId"]);
                bsk.PackageId = Convert.ToInt32(dbReader["PackageId"]);
                bsk.BookingNo = Convert.ToString(dbReader["BookingNo"]);
                bsk.BookingDate = Convert.ToDateTime(dbReader["BookingDate"]);
                bookings.Add(bsk);
            }
        }
        catch (SqlException ex)
        {
            throw ex;
        }
        finally
        {
            dbConn.Close();
        }
        return bookings;
    }
}