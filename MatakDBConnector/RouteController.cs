using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace MatakDBConnector
{
    public class RouteController: Route
    {
        public void AddNewRoute(Route newRoute, out string errorMessage)
        {
            //TODO: return id of the entity created
            errorMessage = null;
            DbConnector database = new DbConnector();
            
            try
            {
                database.Connect();

                var command = new NpgsqlCommand();
                command.Connection = database.Connection;
                command.CommandText =
                    "INSERT INTO route (name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, created, updated, trip_area) VALUES (@name, @start_datetime, @end_datetime, @geojson_doc_id, @reason_id, @priority_id, @status_id, @org_id, @created_by_user_id, @sent_to_user_id, @approved_by_user_id, @note, @created, @updated, st_geomfromgeojson(@trip_area))";
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
                command.Parameters.AddWithValue("trip_area", newRoute.GeoJsonString);

                command.ExecuteNonQuery();
                database.Disconnect();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                errorMessage = e.ToString();
                throw;
            }
            finally
            {
                if (database.Connection.State == ConnectionState.Open)
                {
                    database.Disconnect();   
                }
            }
        }

        public void AddNewRoute(string name, DateTime startDateTime, DateTime endDateTime, int reasonId,
            int priorityId, int statusId, int orgId, int createdByUserId, int sentToUserId, string note, string geoJsonString, out string errorMessage)
        {
            errorMessage = null;
            DbConnector database = new DbConnector();

            try
            {
                database.Connect();

                var command = new NpgsqlCommand();
                command.Connection = database.Connection;
                command.CommandText =
                    "INSERT INTO route (name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, created, updated, trip_area) VALUES (@name, @start_datetime, @end_datetime, @geojson_doc_id, @reason_id, @priority_id, @status_id, @org_id, @created_by_user_id, @sent_to_user_id, @approved_by_user_id, @note, @created, @updated, st_geomfromgeojson(@trip_area))";
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
                command.Parameters.AddWithValue("trip_area", geoJsonString);

                command.ExecuteNonQuery();
                database.Disconnect();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                errorMessage = e.ToString();
                throw;
            }
            finally
            {
                if (database.Connection.State == ConnectionState.Open)
                {
                    database.Disconnect();   
                }
            }
        }
        public Route GetRouteById(int RouteID, out string errorMessage)
        {
            //TODO: count of routes by orgID
            
            errorMessage = null;
            DbConnector database = new DbConnector();
            Route route = null;
            
            try
            {
                database.Connect();
                
                var command = new NpgsqlCommand();
                command.Connection = database.Connection;
                command.CommandText = "SELECT route_id, name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, st_asgeojson(trip_area, 15, 0) FROM route WHERE route_id = (@p)";
                //TODO: change to store procedures
                command.Parameters.AddWithValue("p", RouteID);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    route = RouteMaker(reader);
                }

                return route;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                errorMessage = e.ToString();
                throw;
            }
            finally
            {
                if (database.Connection.State == ConnectionState.Open)
                {
                    database.Disconnect();   
                }
            }
        }

        public List<Route> GetAllRoutes(out string errorMessage)
        {
            errorMessage = null;
            DbConnector database = new DbConnector();
            List<Route> allRoutes = new List<Route>();
            
            try
            {
                database.Connect();
                
                var command = new NpgsqlCommand();
                command.Connection = database.Connection;
                command.CommandText = "SELECT route_id, name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, st_asgeojson(trip_area, 15, 0) FROM route";

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Route route = RouteMaker(reader);
                    allRoutes.Add(route);
                }
                return allRoutes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                errorMessage = e.ToString();
                throw;
            }
            finally
            {
                if (database.Connection.State == ConnectionState.Open)
                {
                    database.Disconnect();   
                }
            }
        }

        public List<Route> GetAllRoutesByOrgId(int orgId, out string errorMessage)
        {
            errorMessage = null;
            DbConnector database = new DbConnector();
            List<Route> allRoutesByOrgId = new List<Route>();
            
            try
            {
                database.Connect();
                
                var command = new NpgsqlCommand();
                command.Connection = database.Connection;
                command.CommandText = "SELECT route_id, name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, st_asgeojson(trip_area, 15, 0) FROM route WHERE org_id = (@p)";
                command.Parameters.AddWithValue("p", orgId);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Route route = RouteMaker(reader);                    
                    allRoutesByOrgId.Add(route);             
                }

                return allRoutesByOrgId;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                errorMessage = e.ToString();
                throw;
            }
            finally
            {
                if (database.Connection.State == ConnectionState.Open)
                {
                    database.Disconnect();   
                }
            }
        }
        
         public List<Route> GetAllRoutesByCreatorId(int createdByUserId, out string errorMessage)
        {
            errorMessage = null;
            DbConnector database = new DbConnector();
            List<Route> allRoutesByOrgId = new List<Route>();
            
            try
            {
                database.Connect();
                
                var command = new NpgsqlCommand();
                command.Connection = database.Connection;
                command.CommandText = "SELECT route_id, name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, st_asgeojson(trip_area, 15, 0) FROM route WHERE created_by_user_id = (@p)";
                command.Parameters.AddWithValue("p", createdByUserId);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Route route = RouteMaker(reader);
                    allRoutesByOrgId.Add(route);
                }

                return allRoutesByOrgId;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                errorMessage = e.ToString();
                throw;
            }
            finally
            {
                if (database.Connection.State == ConnectionState.Open)
                {
                    database.Disconnect();   
                }
            }
        }
    }
}