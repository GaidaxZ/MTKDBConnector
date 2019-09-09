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

        public int UpdateDocumentId(Document document, out string errorMessage)
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
                        "UPDATE document SET route_id = (@route_id), description = (@description), created = (@created), updated = (@updated), created_by_user_id = (@created_by_user_id), updated_by_user_id = (@updated_by_user_id) WHERE document_id = (@document_id) RETURNING document_id";
                    newDocumentCommandHelper(document, command, false);
                    command.Parameters.AddWithValue("document_id", document.DocumentId);

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

        public string GetDocumentHandleByDocId(int documentId, out string errorMessage)
        {
            errorMessage = null;
            string documentHandle = null;
            
            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT route_id FROM document WHERE route_id = (@documentId)";
                    command.Parameters.AddWithValue("documentId", documentId);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        documentHandle = reader.GetInt32(0) + "_" + documentId;
                    }

                    return documentHandle;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }

        public Document GetDocumentById(int documentId, out string errorMessage)
        {
            errorMessage = null;
            
            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT document_id, route_id, description, created, updated, created_by_user_id, updated_by_user_id FROM document WHERE route_id = (@documentId)";
                    command.Parameters.AddWithValue("documentId", documentId);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        return DocumentMaker(reader);
                    }

                    errorMessage = "Specified document ID was not found in the database - " + documentId;
                    return null;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }

        public List<Document> GetAllDocumentsByRouteId(int routeId, out string errorMessage)
        {
            errorMessage = null;
            List<Document> documents = new List<Document>();
            
            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT document_id, route_id, description, created, updated, created_by_user_id, updated_by_user_id FROM document WHERE route_id = (@routeId)";
                    command.Parameters.AddWithValue("routeId", routeId);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Document document = new Document();
                        documents.Add(document.DocumentMaker(reader));
                    }

                    return documents;
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
}