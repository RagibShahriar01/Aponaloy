using APONALOY.GetSet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace APONALOY.DatabaseClass
{
    internal class DataAccess
    {

        private string connectionString;



        public DataAccess()
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







        public void AddFlatInfo(int userId,
                         string flatinfo,
                         decimal price,
                         decimal parkingfee,
                         string purpose)
        {
            // 1) First, load the user’s details
            string userSql = @"
            SELECT Username
            FROM Usertable
            WHERE UserID = @UserID";

            var userTable = ExecuteReader(userSql, new SqlParameter("@UserID", userId));
            if (userTable.Rows.Count == 0)
                throw new Exception($"No user found with ID {userId}");

            DataRow u = userTable.Rows[0];
            string username = u["Username"].ToString();


            // 2) Now insert into ProductsBuy with all columns
            string insert = @"
            INSERT INTO dbo.flatBuyRent
            (UserID, Username, FlatInfo, Price, ParkingFee, Purpose)
            VALUES
            (@UserID, @Username, @FlatInfo, @Price, @ParkingFee, @Purpose)";


            var parameters = new[]
            {
                new SqlParameter("@UserID",      userId),
                new SqlParameter("@Username",    username),
                new SqlParameter("@FlatInfo",    flatinfo),
                new SqlParameter("@Price",       price),
                new SqlParameter("@ParkingFee",  parkingfee),
                new SqlParameter("@Purpose",     purpose),

            };

            ExecuteNonQuery(insert, parameters);


        }







        public DataRow GetUserProfileById(int userId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                SELECT 
                    ProfileID, 
                    UserID, 
                    Username, 
                    Email, 
                    Address,
                    Mobile
                FROM Profiletable
                WHERE UserID = @UserID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserID", userId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable resultTable = new DataTable();

                    adapter.Fill(resultTable);

                    if (resultTable.Rows.Count > 0)
                    {
                        return resultTable.Rows[0]; // Return the first row
                    }
                    else
                    {
                        return null; // No matching user profile found
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching user profile: {ex.Message}");
            }
        }






        public void UpdateUserProfile(int userId, string email, string address, string mobile)
        {
            string query = @"
        BEGIN TRANSACTION;

        -- Update Profiletable
        IF EXISTS (SELECT 1 FROM Profiletable WHERE UserID = @UserID)
        BEGIN
            UPDATE Profiletable
            SET
                Email = @Email,
                Address = Address,
                Mobile = @Mobile
            WHERE UserID = @UserID;
        END
        ELSE
        BEGIN
            INSERT INTO Profiletable (UserID, Email, Address, Mobile)
            VALUES (@UserID, @Email, @Address, @Mobile);
        END

        -- Update Usertable
        UPDATE Usertable
        SET Email = @Email, UpdatedDate = GETDATE()
        WHERE UserID = @UserID;

        COMMIT TRANSACTION;
    ";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userId),
                new SqlParameter("@Email", string.IsNullOrWhiteSpace(email) ? (object)DBNull.Value : email),
                new SqlParameter("@Address", string.IsNullOrWhiteSpace(address) ? (object)DBNull.Value : address),
                new SqlParameter("@Mobile", string.IsNullOrWhiteSpace(mobile) ? (object)DBNull.Value : mobile)
            };

            ExecuteNonQuery(query, parameters);
        }









        // Return all requests (for admin)
        public DataTable GetAllFlatBuyRent()
        {
            string sql = @"
        SELECT FlatBuyRentID, UserID, Username, FlatInfo, Price, ParkingFee, Purpose, Status, CreatedAt
        FROM dbo.flatBuyRent
        ORDER BY CreatedAt DESC";
            return ExecuteReader(sql); // ExecuteReader already returns a DataTable
        }






        // Return only rows for a specific user (for MyAccount)
        public DataTable GetFlatBuyRentByUser(int userId)
        {
            string sql = @"
        SELECT FlatBuyRentID, UserID, Username, FlatInfo, Price, ParkingFee, Purpose, Status, CreatedAt
        FROM dbo.flatBuyRent
        WHERE UserID = @UserID
        ORDER BY CreatedAt DESC";
            return ExecuteReader(sql, new SqlParameter("@UserID", userId));
        }







        // Update a single row Status (e.g. "Approved")
        public int UpdateFlatStatus(int flatBuyRentId, string newStatus)
        {
            string sql = @"
        UPDATE dbo.flatBuyRent
        SET Status = @Status
        WHERE FlatBuyRentID = @Id";
            var p = new[]
            {
        new SqlParameter("@Status", newStatus),
        new SqlParameter("@Id", flatBuyRentId)
            };

            return ExecuteNonQuery(sql, p);
        }








        // Add a message to the Messages table for a user (sender could be "Admin")


        // DatabaseQ.cs — AddMessage (store UTC)
        public int AddMessage(int userId, string sender, string messageText)
        {
            string sql = @"
                         INSERT INTO dbo.Messages (UserID, Sender, MessageText, SentAt)
                         VALUES (@UserID, @Sender, @MessageText, @SentAt)";

            var p = new[]
            {
                 new SqlParameter("@UserID", userId),
                 new SqlParameter("@Sender", string.IsNullOrEmpty(sender) ? (object)DBNull.Value : sender),
                 new SqlParameter("@MessageText", messageText ?? string.Empty),
                 new SqlParameter("@SentAt", DateTime.UtcNow) // store UTC
            };

            return ExecuteNonQuery(sql, p);
        }





        // Get messages for a user (optionally latest first)
        public DataTable GetMessagesForUser(int userId)
        {
            string sql = @"
                         SELECT MessageID, UserID, Sender, MessageText, SentAt
                         FROM dbo.Messages
                         WHERE UserID = @UserID
                         ORDER BY SentAt DESC";

            return ExecuteReader(sql, new SqlParameter("@UserID", userId));
        }







        // Search flatBuyRent table by username, flatinfo, purpose or status (case-insensitive depending on DB collation)
        public DataTable SearchFlatBuyRent(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return GetAllFlatBuyRent();
            }

            string sql = @"
                         SELECT FlatBuyRentID, UserID, Username, FlatInfo, Price, ParkingFee, Purpose, Status, CreatedAt
                         FROM dbo.flatBuyRent
                         WHERE Username LIKE @like
                         OR FlatInfo LIKE @like
                         OR Purpose LIKE @like
                         OR Status LIKE @like
                         ORDER BY CreatedAt DESC";

            var param = new SqlParameter("@like", "%" + searchTerm + "%");
            return ExecuteReader(sql, param);
        }







        public bool RecoverUserPassword(string email, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {


                    string query = @"
                UPDATE Usertable
                SET Password = @Password
                WHERE Email = @Email";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Email", email);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error resetting password: {ex.Message}");
            }
        }






        // Add using at top of file if not present:
        // using System.Data.SqlClient;

        public DataTable GetAllUserProfiles()
        {
            string sql = @"
                         SELECT 
                         u.UserID,
                         u.Username,
                         u.Email,
                         u.Role,
                         u.CreatedDate,
                         p.ProfileID,
                         p.Address,
                         p.Mobile
                         FROM dbo.Usertable u
                         LEFT JOIN dbo.Profiletable p ON u.UserID = p.UserID
                         WHERE u.Role = @Role
                         ORDER BY u.UserID DESC";

            return ExecuteReader(sql, new SqlParameter("@Role", "User"));
        }








        public DataTable SearchUserProfiles(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return GetAllUserProfiles();
            }

            string sql = @"
                         SELECT 
                         u.UserID,
                         u.Username,
                         u.Email,
                         u.Role,
                         u.CreatedDate,
                         p.ProfileID,
                         p.Address,
                         p.Mobile
                         FROM dbo.Usertable u
                         LEFT JOIN dbo.Profiletable p ON u.UserID = p.UserID
                         WHERE u.Role = @Role
                         AND (
                         u.Username LIKE @like
                         OR u.Email LIKE @like
                         OR u.Role LIKE @like
                         OR p.Address LIKE @like
                         OR p.Mobile LIKE @like
                         )
                         ORDER BY u.UserID DESC";

            var parameters = new[]
            {
                   new SqlParameter("@like", "%" + searchTerm + "%"),
                   new SqlParameter("@Role", "User")
            };

            return ExecuteReader(sql, parameters);
        }









        public int DeleteUserCascade(int userId)
        {
            // Delete child's rows first, then the user row. Use explicit transaction.
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;

                            cmd.CommandText = "DELETE FROM dbo.flatBuyRent WHERE UserID = @UserID";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@UserID", userId);
                            int rowsFlat = cmd.ExecuteNonQuery();

                            cmd.CommandText = "DELETE FROM dbo.Profiletable WHERE UserID = @UserID";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@UserID", userId);
                            int rowsProfile = cmd.ExecuteNonQuery();

                            // optional: delete messages / other related tables if exists
                            cmd.CommandText = "DELETE FROM dbo.Messages WHERE UserID = @UserID";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@UserID", userId);
                            int rowsMessages = cmd.ExecuteNonQuery();

                            cmd.CommandText = "DELETE FROM dbo.Usertable WHERE UserID = @UserID";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@UserID", userId);
                            int rowsUser = cmd.ExecuteNonQuery();

                            tran.Commit();
                            return rowsFlat + rowsProfile + rowsMessages + rowsUser; // total rows deleted
                        }
                    }
                    catch
                    {
                        try { tran.Rollback(); } catch { /* swallow rollback errors */ }
                        throw; // bubble up to caller
                    }
                }
            }
        }




    }
}
