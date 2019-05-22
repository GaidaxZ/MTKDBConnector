using System;
using System.Data;
using Npgsql;

namespace MatakDBConnector
{
    internal static class DbConnector
    {     
        private static readonly NpgsqlConnection Connection = new NpgsqlConnection(ConfigParser.ConnString);
        internal static NpgsqlCommand Command;
        internal static NpgsqlDataReader Reader;
        

        internal static void Connect()
        {   
            {
                try
                {
                    Connection.Open();
                    Command = new NpgsqlCommand();
                    Command.Connection = Connection;
                    if (Connection.State == ConnectionState.Open)
                    {
                        Console.WriteLine("Successfully opened postgreSQL Connection.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to open postgreSQL Connection.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        internal static void Disconnect()
        {
            {
                try
                {
                    if (Connection.State == ConnectionState.Open)
                    {
                        Connection.Close();
                        Console.WriteLine("Connection closed."); 
                    }
                    Reader = null;
                    Command = null;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        internal static void Dispose()
        {
            Connection?.Dispose();
        }
    }
}