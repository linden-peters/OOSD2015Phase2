﻿/*
 * Author : Geetha
 * Date: July 20, 2015
 * Usage: Database interaction with Customer database
 * */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Data access class for Customer table
/// </summary>

public class CustomerDB
{
   
    //Method to check the username availability before registering the customer
    public static bool CheckUserAvailablity(string custUserName)
    {
        SqlConnection connection = TravelExpertsDB.GetConnection();
        string selectStatement = "SELECT COUNT(CustUserName) FROM Customers " +
                                "WHERE CustUserName = @CustUserName";
        SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
        selectCommand.Parameters.AddWithValue("@CustUserName", custUserName.Trim());
        try
        {
            connection.Open();
            int count = (int)selectCommand.ExecuteScalar();
           // int count = int.Parse(selectCommand.ExecuteScalar().ToString());
           // List customerList = selectCommand.ExecuteNonQuery();
            return (count == 0);
        }
        catch (SqlException ex)
        {
            throw ex;
        }
        finally
        {
            connection.Close();
        }
    }

    public static int RegisterCustomer(Customer customer)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            string insertStatement =
                "INSERT Customers " +
                "(CustUserName, CustPassword, CustFirstName, CustLastName, CustAddress, CustCity, " +
                "CustProv, CustPostal, CustCountry, CustHomePhone, CustBusPhone, CustEmail, AgentId) " +
                "VALUES (@CustUserName, @CustPassword, @CustFirstName, @CustLastName, @CustAddress, @CustCity, " +
                "@CustProv, @CustPostal, @CustCountry, @CustHomePhone, @CustBusPhone, @CustEmail, @AgentId)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue("@CustUserName", customer.CustUserName.Trim());
            insertCommand.Parameters.AddWithValue("@CustPassword", customer.CustPassword.Trim());
            insertCommand.Parameters.AddWithValue("@CustFirstName", customer.CustFirstName.Trim());
            insertCommand.Parameters.AddWithValue("@CustLastName", customer.CustLastName.Trim());
            insertCommand.Parameters.AddWithValue("@CustAddress", customer.CustAddress.Trim());
            insertCommand.Parameters.AddWithValue("@CustCity", customer.CustCity.Trim());
            insertCommand.Parameters.AddWithValue("@CustProv", customer.CustProv.Trim());
            insertCommand.Parameters.AddWithValue("@CustPostal", customer.CustPostal.Trim());
            insertCommand.Parameters.AddWithValue("@CustCountry", customer.CustCountry.Trim());
            insertCommand.Parameters.AddWithValue("@CustHomePhone", customer.CustHomePhone.Trim());
            insertCommand.Parameters.AddWithValue("@CustBusPhone", customer.CustBusPhone.Trim());
            insertCommand.Parameters.AddWithValue("@CustEmail", customer.CustEmail.Trim());
            //insertCommand.Parameters.AddWithValue("@AgentId", DBNull.Value);
           // insertCommand.Parameters.AddWithValue("@AgentId", customer.AgentId < 0 ? DBNull.Value : customer.AgentId);
           
            if (customer.AgentId < 0)
                insertCommand.Parameters.AddWithValue("@AgentId", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@AgentId", customer.AgentId);
        
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                string selectStatement =
                    "SELECT IDENT_CURRENT('Customers') FROM Customers"; // After adding customer, get customer ID
                SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
                int customerId = Convert.ToInt32(selectCommand.ExecuteScalar());
                return customerId;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

    //Method to update package by giving new and old customer details
    public static bool UpdateCustomer(Customer originalCustomer, Customer customer)
    {

        SqlConnection connection = TravelExpertsDB.GetConnection();

        string updateStatement =
                    "UPDATE Customers SET " +
                    "CustUserName = @CustUserName, " +
                    "CustPassword = @CustPassword, " +
                    "CustFirstName = @CustFirstName, " +
                    "CustLastName = @CustLastName, " +
                    "CustAddress = @CustAddress, " +
                    "CustCity = @CustCity, " +
                    "CustProv = @CustProv, " + 
                    "CustPostal = @CustPostal, " + 
                    "CustCountry = @CustCountry, " + 
                    "CustHomePhone = @CustHomePhone, " + 
                    "CustBusPhone = @CustBusPhone, " + 
                    "CustEmail = @CustEmail " + 
                    "WHERE CustomerId = @original_CustomerId " +
                    "AND CustUserName = @original_CustUserName " +
                    "AND CustPassword = @original_CustPassword " +
                    "AND CustFirstName = @original_CustFirstName " +
                    "AND CustLastName = @original_CustLastName " +
                    "AND CustCity = @original_CustCity " +
                    "AND CustAddress = @original_CustAddress " + 
                    "AND CustProv = @original_CustProv " +
                    "AND CustPostal = @original_CustPostal " +
                    "AND CustCountry = @original_CustCountry " +
                    "AND CustHomePhone = @original_CustHomePhone " +
                    "AND CustBusPhone = @original_CustBusPhone " +
                    "AND CustEmail = @original_CustEmail";

        SqlCommand updateCommand = new SqlCommand(updateStatement, connection);

        //Customers new details
        updateCommand.Parameters.AddWithValue("@CustUserName", customer.CustUserName);
        updateCommand.Parameters.AddWithValue("@CustPassword", customer.CustPassword);
        updateCommand.Parameters.AddWithValue("@CustFirstName", customer.CustFirstName);
        updateCommand.Parameters.AddWithValue("@CustLastName", customer.CustLastName);
        updateCommand.Parameters.AddWithValue("@CustAddress", customer.CustAddress);
        updateCommand.Parameters.AddWithValue("@CustCity", customer.CustCity);
        updateCommand.Parameters.AddWithValue("@CustProv", customer.CustProv);
        updateCommand.Parameters.AddWithValue("@CustPostal", customer.CustPostal);
        updateCommand.Parameters.AddWithValue("@CustCountry", customer.CustCountry);
        updateCommand.Parameters.AddWithValue("@CustHomePhone", customer.CustHomePhone);
        updateCommand.Parameters.AddWithValue("@CustBusPhone", customer.CustBusPhone);
        updateCommand.Parameters.AddWithValue("@CustEmail", customer.CustEmail);

        //Customers old details
        updateCommand.Parameters.AddWithValue("@original_CustomerId", originalCustomer.CustomerId);
        updateCommand.Parameters.AddWithValue("@original_CustUserName", originalCustomer.CustUserName);
        updateCommand.Parameters.AddWithValue("@original_CustPassword", originalCustomer.CustPassword);
        updateCommand.Parameters.AddWithValue("@original_CustFirstName", originalCustomer.CustFirstName);
        updateCommand.Parameters.AddWithValue("@original_CustLastName", originalCustomer.CustLastName);
        updateCommand.Parameters.AddWithValue("@original_CustAddress", originalCustomer.CustAddress);
        updateCommand.Parameters.AddWithValue("@original_CustCity", originalCustomer.CustCity);
        updateCommand.Parameters.AddWithValue("@original_CustProv", originalCustomer.CustProv);
        updateCommand.Parameters.AddWithValue("@original_CustPostal", originalCustomer.CustPostal);
        updateCommand.Parameters.AddWithValue("@original_CustCountry", originalCustomer.CustCountry);
        updateCommand.Parameters.AddWithValue("@original_CustHomePhone", originalCustomer.CustHomePhone);
        updateCommand.Parameters.AddWithValue("@original_CustBusPhone", originalCustomer.CustBusPhone);
        updateCommand.Parameters.AddWithValue("@original_CustEmail", originalCustomer.CustEmail);

        try
        {
            connection.Open();
            int count = updateCommand.ExecuteNonQuery();    //get count of updated field
            if (count > 0)
                return true;
            else
                return false;
        }
        catch (SqlException ex)
        {
            throw ex;
        }
        finally
        {
            connection.Close();
        }
    }

    //Method to retreive package details for the particular packageId
    public static Customer GetLoginCredential(Customer login)
    {
        SqlConnection connection = TravelExpertsDB.GetConnection();

        string selectStatement = 
            "SELECT CustomerId, CustUserName, CustPassword, CustFirstName, CustLastName, CustAddress, CustCity, " +
            "CustProv, CustPostal, CustCountry, CustHomePhone, CustBusPhone, CustEmail, AgentId " +
            "FROM Customers " +
            "WHERE CustUserName = @CustUserName " + 
            "AND CustPassword = @CustPassword";

        SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
        selectCommand.Parameters.AddWithValue("@CustUserName", login.CustUserName);
        selectCommand.Parameters.AddWithValue("@CustPassword", login.CustPassword);
        try
        {
            connection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                Customer customer = new Customer();
                customer.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                customer.CustUserName = reader["CustUserName"].ToString();
                customer.CustPassword = reader["CustPassword"].ToString();
                customer.CustFirstName = reader["CustFirstName"].ToString();
                customer.CustLastName = reader["CustLastName"].ToString();
                customer.CustAddress = reader["CustAddress"].ToString();
                customer.CustCity = reader["CustCity"].ToString();
                customer.CustProv = reader["CustProv"].ToString();
                customer.CustPostal = reader["CustPostal"].ToString();
                customer.CustCountry = reader["CustCountry"].ToString();
                customer.CustHomePhone = reader["CustHomePhone"].ToString();
                customer.CustBusPhone = reader["CustBusPhone"].ToString();
                customer.CustEmail = reader["CustEmail"].ToString();
                if (reader["AgentId"] == null || reader["AgentId"] == DBNull.Value)
                     customer.AgentId = -1;
                else
                    customer.AgentId = Convert.ToInt32(reader["AgentId"]);
                return customer;
            }
            else
            {
                return null;
            }
        }
        catch (SqlException ex)
        {
            throw ex;
        }
        finally
        {
            connection.Close();
        }
    }
}