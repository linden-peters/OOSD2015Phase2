/*
 * TravelExpertsDB.cs - Default Connection Provider
 * Author: Linden
 * Written: 2015/07/20
 */
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public static class TravelExpertsDB
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = "Data Source=localhost\\SqlExpress;Initial Catalog=TravelExperts;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
        public static SqlConnection GetInitConn()
        {
            string connectionString = "Data Source=localhost\\SqlExpress;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }

