using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class StatusModel : Status
    {
        public List<Status> getAllStati(out string errorMessage)
        {
            List<Status> allStati = new List<Status>();
            errorMessage = null;

            try
            {
                DbConnector.Connect();

                DbConnector.Command.CommandText = "SELECT * FROM status";
                DbConnector.Reader = DbConnector.Command.ExecuteReader();

                while (DbConnector.Reader.Read())
                {
                    Status status = new Status();
                    allStati.Add(status.StatusMaker(DbConnector.Reader));
                }

                return allStati;
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