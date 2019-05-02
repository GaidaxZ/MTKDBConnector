using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class StatusController
    {
        public List<Status> getAllStati(out string errorMessage)
        {
            List<Status> allStati = new List<Status>();
            errorMessage = null;
            
            try
            {
                DbConnector database = new DbConnector();
                database.Connect();
                
                var command = new NpgsqlCommand();
                command.Connection = database.Connection;
                command.CommandText = "SELECT * FROM status";

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Status status = new Status();

                    status.Id = reader.GetInt32(0);
                    status.Description = reader.GetString(1);
                    status.Color = reader.GetString(2);
                    
                    allStati.Add(status);
                }
                database.Disconnect();
                return allStati;
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