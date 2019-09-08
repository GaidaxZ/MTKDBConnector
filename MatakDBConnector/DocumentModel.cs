using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class DocumentModel : Document
    {
        public int AddNewDocument(Document newDocument, out string errorMessage)
        {
            errorMessage = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;                                     
                
                    command.CommandText =
                        "INSERT INTO document (route_id, description, created, updated, created_by_user_id, updated_by_user_id) VALUES (@route_id, @description, @created, @updated, @created_by_user_id, @updated_by_user_id ) RETURNING document_id";
                    newDocumentCommandHelper(newDocument, command, true);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
        
        //public string GetDocumentHandleByDocId()
    }
}