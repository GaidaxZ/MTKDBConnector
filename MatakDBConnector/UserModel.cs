using System;
using System.Collections.Generic;

namespace MatakDBConnector
{
    public class UserModel : User
    {
        public List<User> getAllUsers(out string errorMessage)
        {
            List<User> allUsers = new List<User>();
            errorMessage = null;

            try
            {
                DbConnector.Connect();

                DbConnector.Command.CommandText = "SELECT * FROM playground.public.user";
                DbConnector.Reader = DbConnector.Command.ExecuteReader();

                while (DbConnector.Reader.Read())
                {
                    User user = new User();
                    allUsers.Add(user.UserMaker(DbConnector.Reader));
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
                DbConnector.Disconnect();
            }
        }
        
        public List<User> getAllUsersByOrgId(int orgId, out string errorMessage)
        {
            List<User> allUsers = new List<User>();
            errorMessage = null;

            try
            {
                DbConnector.Connect();

                DbConnector.Command.CommandText = "SELECT * FROM playground.public.user WHERE org_id = (@p)";
                DbConnector.Command.Parameters.AddWithValue("p", orgId);
                DbConnector.Reader = DbConnector.Command.ExecuteReader();

                while (DbConnector.Reader.Read())
                {
                    User user = new User();
                    allUsers.Add(user.UserMaker(DbConnector.Reader));
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
                DbConnector.Disconnect();
            }
        }
    }
}