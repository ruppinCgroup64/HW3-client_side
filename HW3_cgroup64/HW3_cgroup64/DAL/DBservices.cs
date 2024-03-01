using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using HW3_cgroup64.BL;
using HW2_cgroup64.BL;
using System.Net;

/// <summary>
/// DBServices is a class created by me to provides some DataBase Services
/// </summary>
public class DBservices
{

    public DBservices() { }

    //--------------------------------------------------------------------------------------------------
    // This method creates a connection to the database according to the connectionString name in the web.config 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json").Build();
        string cStr = configuration.GetConnectionString("myProjDB");
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }


    //--------------------------------------------------------------------------------------------------
    // This methods Inserts to tables
    //--------------------------------------------------------------------------------------------------
    public int InsertUser(User user)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateUserInsertCommandWithStoredProcedure("sp_InsertUser_2024", con,user);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    public int InsertFlat(Flat flat)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateFlatInsertCommandWithStoredProcedure("sp_InsertFlat_2024", con, flat);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    public int InsertVacation(Vacation vacation)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateVacationInsertCommandWithStoredProcedure("sp_InsertVacations_2024", con, vacation);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    //--------------------------------------------------------------------------------------------------
    // This method reads from the database 
    //--------------------------------------------------------------------------------------------------
    public List<User> ReadUsers()
    {

        SqlConnection con;
        SqlCommand cmd;
        List<User> usersList = new List<User>();

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureWithoutParameters("sp_GetUsers_2024", con);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                User u = new User();
                u.Email = dataReader["email"].ToString();
                u.FamilyName = dataReader["familyName"].ToString();
                u.FirstName = dataReader["firstName"].ToString();
                u.Password = dataReader["password"].ToString();
                u.IsActive = Convert.ToBoolean(dataReader["isActive"]);
                u.IsAdmin = Convert.ToBoolean(dataReader["isAdmin"]);
                usersList.Add(u);
            }
            return usersList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
        
    }
    public List<Flat> ReadFlats()
    {

        SqlConnection con;
        SqlCommand cmd;
        List<Flat> flatsList = new List<Flat>();

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureWithoutParameters("sp_GetFlats_2024", con);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
         
            while (dataReader.Read())
            {
                Flat f = new Flat();
                f.Id = Convert.ToInt32(dataReader["Id"]);
                f.City = dataReader["city"].ToString();
                f.Address = dataReader["address"].ToString();
                f.Price = Convert.ToDouble(dataReader["price"]);
                flatsList.Add(f);
            }
            return flatsList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    public List<Vacation> ReadVacations()
    {

        SqlConnection con;
        SqlCommand cmd;
        List<Vacation> vacationsList = new List<Vacation>();

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureWithoutParameters("sp_GetVacations_2024", con);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Vacation v = new Vacation();
                v.Id = Convert.ToInt32(dataReader["Id"]);
                v.FlatId = Convert.ToInt32(dataReader["flatId"]);
                v.UserEmail = dataReader["userEmail"].ToString();
                v.StartDate = Convert.ToDateTime(dataReader["startDate"]);
                v.EndDate = Convert.ToDateTime(dataReader["endDate"]);
                vacationsList.Add(v);
            }
            return vacationsList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    public Object ReadReport(int month)
    {

        SqlConnection con;
        SqlCommand cmd;
        List<Object> reports = new List<Object>();

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        cmd = CreateReportCommandWithStoredProcedure("sp_Report_2024", con, month);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                reports.Add(new
                {
                    city = dataReader["City"].ToString(),
                    averagePrice = Convert.ToDouble(dataReader["AveragePricePerNight"])
                });
            }
            return reports;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }  
   

    //--------------------------------------------------------------------------------------------------
    // This method update user details
    //--------------------------------------------------------------------------------------------------
    public User UpdateUser(User user)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateUserUpdateCommandWithStoredProcedure("sp_UpdateUser_2024", con, user);             // create the command
        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);


            User u = new User();
            while (dataReader.Read())
            {
                u.Email = dataReader["email"].ToString();
                u.FamilyName = dataReader["familyName"].ToString();
                u.FirstName = dataReader["firstName"].ToString();
                u.Password = dataReader["password"].ToString();
            }
            return u;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    public User Login(User user)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateUserInsertCommandWithStoredProcedure("sp_Login_2024", con, user);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);


            User u = new User();
            while (dataReader.Read())
            {
                u.Email = dataReader["email"].ToString();
                u.FamilyName = dataReader["familyName"].ToString();
                u.FirstName = dataReader["firstName"].ToString();
                u.Password = dataReader["password"].ToString();
                u.IsActive = Convert.ToBoolean(dataReader["isActive"]);
                u.IsAdmin = Convert.ToBoolean(dataReader["isAdmin"]);
            }
            return u;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    public int UpdateUserIsActive(User user)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateUserUpdateIsActiveCommandWithStoredProcedure("sp_UpdateUserActivity_2024", con, user);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateUserInsertCommandWithStoredProcedure(String spName, SqlConnection con, User user)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@email", user.Email);
        cmd.Parameters.AddWithValue("@firstName", user.FirstName);
        cmd.Parameters.AddWithValue("@familyName", user.FamilyName);
        cmd.Parameters.AddWithValue("@password", user.Password);

        return cmd;
    }
    private SqlCommand CreateFlatInsertCommandWithStoredProcedure(String spName, SqlConnection con, Flat flat)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@city", flat.City);
        cmd.Parameters.AddWithValue("@address", flat.Address);
        cmd.Parameters.AddWithValue("@numOfRooms", flat.NumOfRooms);
        cmd.Parameters.AddWithValue("@price", flat.Price);
        return cmd;
    }
    private SqlCommand CreateVacationInsertCommandWithStoredProcedure(String spName, SqlConnection con, Vacation vacation)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@flatId", vacation.FlatId);
        cmd.Parameters.AddWithValue("@userEmail", vacation.UserEmail);
        cmd.Parameters.AddWithValue("@startDate", vacation.StartDate);
        cmd.Parameters.AddWithValue("@endDate", vacation.EndDate);
        return cmd;
    }
    private SqlCommand CreateUserUpdateCommandWithStoredProcedure(String spName, SqlConnection con, User user)
    {


        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@email", user.Email);
        cmd.Parameters.AddWithValue("@firstName", user.FirstName);
        cmd.Parameters.AddWithValue("@familyName", user.FamilyName);
        cmd.Parameters.AddWithValue("@password", user.Password);

        return cmd;
    }
    private SqlCommand CreateUserUpdateIsActiveCommandWithStoredProcedure(String spName, SqlConnection con, User user)
    {


        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@email", user.Email);
        cmd.Parameters.AddWithValue("@firstName", user.FirstName);
        cmd.Parameters.AddWithValue("@familyName", user.FamilyName);
        cmd.Parameters.AddWithValue("@password", user.Password);
        cmd.Parameters.AddWithValue("@isActive", user.IsActive);

        return cmd;
    }
    private SqlCommand CreateReportCommandWithStoredProcedure(String spName, SqlConnection con, int month)
    {


        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@month", month);

        return cmd;
    }
    private SqlCommand CreateCommandWithStoredProcedureWithoutParameters(String spName, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        return cmd;
    }

}