using RealState.AllClass;
using RealState.DatabaseClass;
using RealState.GetSet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.DatabaseClass
{
    internal class DatabaseQ
    {
        private string connectionString;



        public DatabaseQ()
        {
            // Retrieve the connection string from App.config
            connectionString = ConfigurationManager.ConnectionStrings["RealStateZ"].ConnectionString;
        }





        public int ExecuteNonQuery(string query, SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    conn.Open();

                    // Debugging: Log the query and parameters
                    Console.WriteLine($"Executing Query: {query}");
                    foreach (var param in parameters)
                    {
                        Console.WriteLine($"{param.ParameterName}: {param.Value}");
                    }

                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Rows affected: {rowsAffected}");

                    return rowsAffected;
                }
            }
        }





        public object ExecuteScalar(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }




        public DataTable ExecuteReader(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        return dt;
                    }
                }
            }
        }






        public bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Optionally, log the exception or display a message
                System.Windows.Forms.MessageBox.Show("Database Connection Failed: " + ex.Message);
                return false;
            }
        }





        // Create (Register a New User)
        public int CreateUser(User user)
        {
            string query = "INSERT INTO Usertable (Username, [Password], [Role], Email, Address, Mobile) VALUES (@Username, @Password, @Role, @Email, @Address, @Mobile); SELECT CAST(scope_identity() AS int);";

            SqlParameter[] parameters = new SqlParameter[]
            {
                 new SqlParameter("@Username", user.Username),
                 new SqlParameter("@Password", user.Password),
                 new SqlParameter("@Role", user.Role),
                 new SqlParameter("@Email", user.Email),
                 new SqlParameter("@Address", user.Address),
                 new SqlParameter("@Mobile", user.Mobile)

            };

            // Execute the query and return the new UserID
            return (int)ExecuteScalar(query, parameters);
        }






        public void CreateUserProfile(Profile profile)
        {
            string query = @"
            INSERT INTO Profiletable (UserID, Username, Email, Address, Mobile)
            VALUES (@UserID, @Username, @Email, @Address, @Mobile);";

            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@UserID", profile.UserID),
            new SqlParameter("@Username", string.IsNullOrEmpty(profile.Username) ? string.Empty : profile.Username),
            new SqlParameter("@Email", string.IsNullOrEmpty(profile.Email) ? string.Empty : profile.Email),
            new SqlParameter("@Address", string.IsNullOrEmpty(profile.Address) ? string.Empty : profile.Address),
            new SqlParameter("@Mobile", string.IsNullOrEmpty(profile.Mobile) ? string.Empty : profile.Mobile)
            };

            ExecuteNonQuery(query, parameters);
        }






        // Read (Get User by Username)
        public User GetUserByUsername(string username)
        {
            string query = "SELECT * FROM Usertable WHERE Username COLLATE SQL_Latin1_General_CP1_CS_AS = @Username";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", username)
            };

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                Username = reader.GetString(reader.GetOrdinal("Username")),
                                Password = reader.GetString(reader.GetOrdinal("Password")),
                                Role = reader.GetString(reader.GetOrdinal("Role")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                Mobile = reader.GetString(reader.GetOrdinal("Mobile"))
                                // Add other fields as necessary
                            };
                        }
                        else
                        {
                            return null; // User not found
                        }
                    }
                }
            }
        }



        public User GetUserByID(int userId)
        {
            string query = "SELECT * FROM Usertable WHERE UserID = @UserID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userId)
            };

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                Username = reader.GetString(reader.GetOrdinal("Username")),
                                Password = reader.GetString(reader.GetOrdinal("Password")),
                                Role = reader.GetString(reader.GetOrdinal("Role")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                Mobile = reader.GetString(reader.GetOrdinal("Mobile"))
                                // Add other fields as necessary
                            };
                        }
                        else
                        {
                            return null; // User not found
                        }
                    }
                }
            }
        }




    }

}

