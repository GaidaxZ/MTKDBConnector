using System;
using System.Data;
using System.IO;
using Npgsql;

namespace MatakDBConnector
{
    public class DbConnector : IDisposable
    {
        private NpgsqlConnection connection = new NpgsqlConnection(ConfigParser.ConnString);
        public NpgsqlConnection Connection => connection;
        
        ~DbConnector()
        {
            if (connection.State == ConnectionState.Open)
            {
                Disconnect();
            }
            Dispose();
        }
        public void Connect()
        {   
            {
                try
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        Console.WriteLine("Successfully opened postgreSQL connection.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to open postgreSQL connection.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public void Disconnect()
        {
            {
                try
                {
                    connection.Close();
                    Console.WriteLine("Connection closed.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public void Dispose()
        {
            connection?.Dispose();
        }
    }
}