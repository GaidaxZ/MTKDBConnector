using System;
using System.Data;
using Npgsql;

namespace MatakDBConnector
{
    internal static class DbConnector
    {     
        private static NpgsqlConnection connection = new NpgsqlConnection(ConfigParser.ConnString);
        public static NpgsqlConnection Connection => connection;
        public static NpgsqlCommand Command = new NpgsqlCommand();
        public static NpgsqlDataReader Reader = null;
        

        public static void Connect()
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

        public static void Disconnect()
        {
            {
                try
                {
                    if (Connection.State == ConnectionState.Open)
                    {
                        Connection.Close();
                        Console.WriteLine("Connection closed."); 
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public static void Dispose()
        {
            Connection?.Dispose();
        }
    }
}