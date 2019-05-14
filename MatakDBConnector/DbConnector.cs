using System;
using System.Data;
using Npgsql;

namespace MatakDBConnector
{
    public class DbConnector : IDisposable
    {
        private NpgsqlConnection connection = new NpgsqlConnection(ConfigParser.ConnString);
        public NpgsqlConnection Connection => connection;
        protected NpgsqlCommand Command = new NpgsqlCommand();
        public NpgsqlDataReader Reader = null;
        
        ~DbConnector()
        {
            Disconnect();
        }
        public void Connect()
        {   
            {
                try
                {
                    Connection.Open();
                    Command.Connection = Connection;
                    if (Connection.State == ConnectionState.Open)
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
                    if (Connection.State == ConnectionState.Open)
                    {
                        Connection.Close();
                        Console.WriteLine("Connection closed.");   
                        Dispose();
                    }
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
            Connection?.Dispose();
        }
    }
}