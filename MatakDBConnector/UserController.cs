using System;
using System.Collections.Generic;

namespace MatakDBConnector
{
    public class UserController : DbConnector
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
                    User user = new User();
                    allUsers.Add(user.UserMaker(Reader));
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
        
        public List<User> getAllUsersByOrgId(int orgId, out string errorMessage)
        {
            List<User> allUsers = new List<User>();
            errorMessage = null;

            try
            {
                Connect();

                Command.CommandText = "SELECT * FROM playground.public.user WHERE org_id = (@p)";
                Command.Parameters.AddWithValue("p", orgId);
                Reader = Command.ExecuteReader();

                while (Reader.Read())
                {
                    User user = new User();
                    allUsers.Add(user.UserMaker(Reader));
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