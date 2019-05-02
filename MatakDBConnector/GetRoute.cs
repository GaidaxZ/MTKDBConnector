using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class GetRoute
    {           
        public Route ById(int RouteID, out string errorMessage)
        {
            //TODO: what to return if route ID is not found? Suggestion if route_id is 0 then no route.
            
            //TODO: getRouteByCreator
            //TODO: getRouteByOrgID
            //TODO: count of routes by orgID
            //TODO: merge set and get route to one class
            
            errorMessage = null;
            Route route = new Route();
            try
            {
                DbConnector database = new DbConnector();
                database.Connect();
                
                var command = new NpgsqlCommand();
                command.Connection = database.Connection;
                command.CommandText = "SELECT route_id, name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, st_asgeojson(trip_area, 15, 0) FROM route WHERE route_id = (@p)";
                //TODO: change to store procedures
                //TODO: change * to actual column names 
                //TODO: to generate geoJson use PostGIS method INSIDE select
                command.Parameters.AddWithValue("p", RouteID);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    route.RouteId = reader.GetInt32(0);
                    route.Name = reader.GetString(1);
                    route.StartDatetime = reader.GetDateTime(2);
                    route.EndDatetime = reader.GetDateTime(3);
                    route.GeojsonDocId = reader.GetInt32(4);
                    route.ReasonId = reader.GetInt32(5);
                    route.PriorityId = reader.GetInt32(6);
                    route.StatusId = reader.GetInt32(7);
                    route.OrgId = reader.GetInt32(8);
                    route.CreatedByUserId = reader.GetInt32(9);
                    route.SentToUserId = reader.GetInt32(10);
                    route.ApprovedByUserId = reader.GetInt32(11);
                    route.Note = reader.GetString(12);
                    route.GeoJsonString = reader.GetString(13);
                }
                
                database.Disconnect();

                return route;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                errorMessage = e.ToString();
                throw;
            }

        }

        public List<Route> AllRoutes(out string errorMessage)
        {
            List<Route> allRoutes = new List<Route>();
            errorMessage = null;
            try
            {
                DbConnector database = new DbConnector();
                database.Connect();
                
                var command = new NpgsqlCommand();
                command.Connection = database.Connection;
                command.CommandText = "SELECT route_id, name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, st_asgeojson(trip_area, 15, 0) FROM route";

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Route route = new Route();
                    
                    route.RouteId = reader.GetInt32(0);
                    route.Name = reader.GetString(1);
                    route.StartDatetime = reader.GetDateTime(2);
                    route.EndDatetime = reader.GetDateTime(3);
                    route.GeojsonDocId = reader.GetInt32(4);
                    route.ReasonId = reader.GetInt32(5);
                    route.PriorityId = reader.GetInt32(6);
                    route.StatusId = reader.GetInt32(7);
                    route.OrgId = reader.GetInt32(8);
                    route.CreatedByUserId = reader.GetInt32(9);
                    route.SentToUserId = reader.GetInt32(10);
                    route.ApprovedByUserId = reader.GetInt32(11);
                    route.Note = reader.GetString(12);
                    route.GeoJsonString = reader.GetString(13);
                    
                    allRoutes.Add(route);
                }
                database.Disconnect();
                return allRoutes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                errorMessage = e.ToString();
                throw;
            }
        }

        public List<Route> AllRoutesByOrgId(int orgId, out string errorMessage)
        {
            List<Route> allRoutesByOrgId = new List<Route>();
            errorMessage = null;
            try
            {
                DbConnector database = new DbConnector();
                database.Connect();
                
                var command = new NpgsqlCommand();
                command.Connection = database.Connection;
                command.CommandText = "SELECT route_id, name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, st_asgeojson(trip_area, 15, 0) FROM route WHERE org_id = (@p)";
                command.Parameters.AddWithValue("p", orgId);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Route route = new Route();
                    
                    route.RouteId = reader.GetInt32(0);
                    route.Name = reader.GetString(1);
                    route.StartDatetime = reader.GetDateTime(2);
                    route.EndDatetime = reader.GetDateTime(3);
                    route.GeojsonDocId = reader.GetInt32(4);
                    route.ReasonId = reader.GetInt32(5);
                    route.PriorityId = reader.GetInt32(6);
                    route.StatusId = reader.GetInt32(7);
                    route.OrgId = reader.GetInt32(8);
                    route.CreatedByUserId = reader.GetInt32(9);
                    route.SentToUserId = reader.GetInt32(10);
                    route.ApprovedByUserId = reader.GetInt32(11);
                    route.Note = reader.GetString(12);
                    route.GeoJsonString = reader.GetString(13);
                    
                    allRoutesByOrgId.Add(route);
                    
                }
                
                database.Disconnect();
                return allRoutesByOrgId;

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