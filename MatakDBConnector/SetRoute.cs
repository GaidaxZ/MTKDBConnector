using System;
using Npgsql;

namespace MatakDBConnector
{
    public class SetRoute
    {
        public int AddNewRoute(Route newRoute, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                DbConnector database = new DbConnector();
                database.Connect();

                var command = new NpgsqlCommand();
                command.Connection = database.Connection;
                command.CommandText =
                    "INSERT INTO route (name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, created, updated) VALUES (@name, @start_datetime, @end_datetime, @geojson_doc_id, @reason_id, @priority_id, @status_id, @org_id, @created_by_user_id, @sent_to_user_id, @approved_by_user_id, @note, @created, @updated )";
                command.Parameters.AddWithValue("name", newRoute.Name);
                command.Parameters.AddWithValue("start_datetime", newRoute.StartDatetime);
                command.Parameters.AddWithValue("end_datetime", newRoute.EndDatetime);
                command.Parameters.AddWithValue("geojson_doc_id", 0);
                command.Parameters.AddWithValue("reason_id", newRoute.ReasonId);
                command.Parameters.AddWithValue("priority_id", newRoute.PriorityId);
                command.Parameters.AddWithValue("status_id", newRoute.StatusId);
                command.Parameters.AddWithValue("org_id", newRoute.OrgId);
                command.Parameters.AddWithValue("created_by_user_id", newRoute.CreatedByUserId);
                command.Parameters.AddWithValue("sent_to_user_id", newRoute.SentToUserId);
                command.Parameters.AddWithValue("approved_by_user_id", 0);
                command.Parameters.AddWithValue("note", newRoute.Note);
                command.Parameters.AddWithValue("created", DateTime.Now);
                command.Parameters.AddWithValue("updated", DateTime.Now);

                command.ExecuteNonQuery();
                database.Disconnect();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                errorMessage = e.ToString();
                throw;
            }
            return 1;
        }

        public int AddNewRoute(string name, DateTime startDateTime, DateTime endDateTime, int reasonId,
            int priorityId, int statusId, int orgId, int createdByUserId, int sentToUserId, string note, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                DbConnector database = new DbConnector();
                database.Connect();
                
                var command = new NpgsqlCommand();
                command.Connection = database.Connection;
                command.CommandText = "INSERT INTO route (name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, created, updated) VALUES (@name, @start_datetime, @end_datetime, @geojson_doc_id, @reason_id, @priority_id, @status_id, @org_id, @created_by_user_id, @sent_to_user_id, @approved_by_user_id, @note, @created, @updated )";
                command.Parameters.AddWithValue("name", name);
                command.Parameters.AddWithValue("start_datetime", startDateTime);
                command.Parameters.AddWithValue("end_datetime", endDateTime);
                command.Parameters.AddWithValue("geojson_doc_id", 0);
                command.Parameters.AddWithValue("reason_id", reasonId);
                command.Parameters.AddWithValue("priority_id", priorityId);
                command.Parameters.AddWithValue("status_id", statusId);
                command.Parameters.AddWithValue("org_id", orgId);
                command.Parameters.AddWithValue("created_by_user_id", createdByUserId);
                command.Parameters.AddWithValue("sent_to_user_id", sentToUserId);
                command.Parameters.AddWithValue("approved_by_user_id", 0);
                command.Parameters.AddWithValue("note", note);
                command.Parameters.AddWithValue("created", DateTime.Now);
                command.Parameters.AddWithValue("updated", DateTime.Now);

                command.ExecuteNonQuery();
                database.Disconnect();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                errorMessage = e.ToString();
                throw;
            }
            
            return 1;
        }

    }
}