using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class UserModel : User
    {
        public List<User> GetAllUsers(out string errorMessage)
        {
            List<User> allUsers = new List<User>();
            errorMessage = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;

                    command.CommandText = "SELECT * FROM postgres.cyberschema1.user";
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        User user = new User();
                        allUsers.Add(user.UserMaker(reader));
                    }

                    return allUsers;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }

        public User GetUserByUserId(int userId, out string errorMessage)
        {
            errorMessage = null;
            
            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT * FROM postgres.cyberschema1.user WHERE user_id = (@userId)";
                    command.Parameters.AddWithValue("userId", userId);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        return UserMaker(reader);
                    }

                    errorMessage = "User not found";
                    return null;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }

        public User GetUserByEmail(string email, out string errorMessage)
        {
            errorMessage = null;
            
            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT * FROM postgres.cyberschema1.user WHERE email = (@email)";
                    command.Parameters.AddWithValue("email", email);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        return UserMaker(reader);
                    }

                    errorMessage = "User not found";
                    return null;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
        
        public List<User> GetAllUsersByOrgId(int orgId, out string errorMessage)
        {
            List<User> allUsers = new List<User>();
            errorMessage = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT * FROM postgres.cyberschema1.user WHERE org_id = (@p)";
                    command.Parameters.AddWithValue("p", orgId);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        User user = new User();
                        allUsers.Add(user.UserMaker(reader));
                    }

                    return allUsers;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }

        public string GetPasswordJson(string username, out string errorMessage)
        {
            errorMessage = null;
            string result = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT password FROM postgres.cyberschema1.user WHERE nickname = (@p)";
                    command.Parameters.AddWithValue("p", username);
                    NpgsqlDataReader reader = command.ExecuteReader();
                
                    while (reader.Read())
                    {
                        result = reader.GetString(0);
                    }
                    
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
        
        public string SetPasswordJson(string username, string passwordJson, out string errorMessage)
        {
            errorMessage = null;
            string result = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "UPDATE postgres.cyberschema1.user SET password = (@passwordJson) WHERE nickname = (@nickname)";
                    command.Parameters.AddWithValue("passwordJson", passwordJson);
                    command.Parameters.AddWithValue("nickname", username);
                    NpgsqlDataReader reader = command.ExecuteReader();
                
                    while (reader.Read())
                    {
                        result = reader.GetString(0);
                    }
                    
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
        
        public int AddNewUser(User newUser, out string errorMessage)
        {
            errorMessage = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;                                     
                
                    command.CommandText =
                        "INSERT INTO postgres.cyberschema1.user (password, phone_id, last_name, first_name, permission_id, org_id, email, nickname) VALUES (@password, @phone_id, @last_name, @first_name, @permission_id, @org_id, @email, @nickname) RETURNING user_id";
                    newUserCommandHelper(newUser, command);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
    }
}