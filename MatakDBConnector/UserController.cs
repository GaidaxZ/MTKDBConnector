using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class UserController
    {
        public List<User> getAllUsers(out string errorMessage)
        {
            List<User> allUsers = new List<User>();
            errorMessage = null;
            
            try
            {
                DbConnector database = new DbConnector();
                database.Connect();
                
                var command = new NpgsqlCommand();
                command.Connection = database.Connection;
                command.CommandText = "SELECT * FROM playground.public.user";

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    User user = new User();

                    user.UsedId = reader.GetInt32(0);
                    user.Password = reader.GetString(1);
                    user.PhoneId = reader.GetInt32(2);
                    user.LastName = reader.GetString(3);
                    user.FirstName = reader.GetString(4);
                    user.PermissionId = reader.GetInt32(5);
                    user.OrgId = reader.GetInt32(6);
                    user.Email = reader.GetString(7);
                    user.Nickname = reader.GetString(8);
                    
                    allUsers.Add(user);
                }
                database.Disconnect();
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
}