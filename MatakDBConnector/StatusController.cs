using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class StatusController : DbConnector
    {
        public List<Status> getAllStati(out string errorMessage)
        {
            List<Status> allStati = new List<Status>();
            errorMessage = null;

            try
            {
                Connect();

                Command.CommandText = "SELECT * FROM status";
                Reader = Command.ExecuteReader();

                while (Reader.Read())
                {
                    Status status = new Status();
                    allStati.Add(status.StatusMaker(Reader));
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
                Disconnect();
            }
        }
    }
}