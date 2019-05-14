using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class UserController : User
    {
        public List<User> getAllUsers(out string errorMessage)
        {
            List<User> allUsers = new List<User>();
            errorMessage = null;

            try
            {
                Connect();

                Command.CommandText = "SELECT * FROM playground.public.user";
                Reader = Command.ExecuteReader();

                while (Reader.Read())
                {
                    allUsers.Add(UserMaker(Reader));
                }

                return allUsers;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                errorMessage = e.ToString();
                throw;
            }
            finally
            {
                Disconnect();
            }
        }
    }
}