using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace MatakDBConnector
{
    public class RouteController : Route
    {
        public int AddNewRoute(Route newRoute, out string errorMessage)
        {
            //TODO: consider improving this method by using inheritance properties
            //TODO: change to store procedures
            errorMessage = null;
            
            try
            {
                Connect();
                
                Command.CommandText =
                    "INSERT INTO route (name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, created, updated, trip_area) VALUES (@name, @start_datetime, @end_datetime, @geojson_doc_id, @reason_id, @priority_id, @status_id, @org_id, @created_by_user_id, @sent_to_user_id, @approved_by_user_id, @note, @created, @updated, st_geomfromgeojson(@trip_area)) RETURNING route_id";
                Command.Parameters.AddWithValue("name", newRoute.Name);
                Command.Parameters.AddWithValue("start_datetime", newRoute.StartDatetime);
                Command.Parameters.AddWithValue("end_datetime", newRoute.EndDatetime);
                Command.Parameters.AddWithValue("geojson_doc_id", 0);
                Command.Parameters.AddWithValue("reason_id", newRoute.ReasonId);
                Command.Parameters.AddWithValue("priority_id", newRoute.PriorityId);
                Command.Parameters.AddWithValue("status_id", newRoute.StatusId);
                Command.Parameters.AddWithValue("org_id", newRoute.OrgId);
                Command.Parameters.AddWithValue("created_by_user_id", newRoute.CreatedByUserId);
                Command.Parameters.AddWithValue("sent_to_user_id", newRoute.SentToUserId);
                Command.Parameters.AddWithValue("approved_by_user_id", 0);
                Command.Parameters.AddWithValue("note", newRoute.Note);
                Command.Parameters.AddWithValue("created", DateTime.Now);
                Command.Parameters.AddWithValue("updated", DateTime.Now);
                Command.Parameters.AddWithValue("trip_area", newRoute.GeoJsonString);

                return Convert.ToInt32(Command.ExecuteScalar());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                errorMessage = e.ToString();
                throw;
            }
            finally
            {
                Disconnect();
            }
        }

        public int AddNewRoute(string name, DateTime startDateTime, DateTime endDateTime, int reasonId,
            int priorityId, int statusId, int orgId, int createdByUserId, int sentToUserId, string note, string geoJsonString, out string errorMessage)
        {
            errorMessage = null;

            try
            {
                Connect();

                Command.CommandText =
                    "INSERT INTO route (name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, created, updated, trip_area) VALUES (@name, @start_datetime, @end_datetime, @geojson_doc_id, @reason_id, @priority_id, @status_id, @org_id, @created_by_user_id, @sent_to_user_id, @approved_by_user_id, @note, @created, @updated, st_geomfromgeojson(@trip_area)) RETURNING route_id";
                Command.Parameters.AddWithValue("name", name);
                Command.Parameters.AddWithValue("start_datetime", startDateTime);
                Command.Parameters.AddWithValue("end_datetime", endDateTime);
                Command.Parameters.AddWithValue("geojson_doc_id", 0);
                Command.Parameters.AddWithValue("reason_id", reasonId);
                Command.Parameters.AddWithValue("priority_id", priorityId);
                Command.Parameters.AddWithValue("status_id", statusId);
                Command.Parameters.AddWithValue("org_id", orgId);
                Command.Parameters.AddWithValue("created_by_user_id", createdByUserId);
                Command.Parameters.AddWithValue("sent_to_user_id", sentToUserId);
                Command.Parameters.AddWithValue("approved_by_user_id", 0);
                Command.Parameters.AddWithValue("note", note);
                Command.Parameters.AddWithValue("created", DateTime.Now);
                Command.Parameters.AddWithValue("updated", DateTime.Now);
                Command.Parameters.AddWithValue("trip_area", geoJsonString);

                return Convert.ToInt32(Command.ExecuteScalar());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                errorMessage = e.ToString();
                throw;
            }
            finally
            {
                Disconnect();
            }
        }
        public Route GetRouteById(int RouteID, out string errorMessage)
        {
            errorMessage = null;
            
            try
            {
                Connect();
                
                Command.CommandText = "SELECT route_id, name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, st_asgeojson(trip_area, 15, 0) FROM route WHERE route_id = (@p)";
                Command.Parameters.AddWithValue("p", RouteID);

                Reader = Command.ExecuteReader();

                while (Reader.Read())
                {
                    return RouteMaker(Reader);
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                errorMessage = e.ToString();
                throw;
            }
            finally
            {
                Disconnect();
            }
        }
        
        public int GetRoutesCountByOrgId(int OrgId, out string errorMessage)
        {   
            errorMessage = null;
            var count = -1;
            
            try
            {
                Connect();
                
                Command.CommandText = "SELECT count(*) FROM route WHERE org_id = (@p)";
                Command.Parameters.AddWithValue("p", OrgId);

                Reader = Command.ExecuteReader();

                while (Reader.Read())
                {
                     count = Reader.GetInt32(0);
                }

                return count;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                errorMessage = e.ToString();
                throw;
            }
            finally
            {
                Disconnect();
            }
        }

        public List<Route> GetAllRoutes(out string errorMessage)
        {
            errorMessage = null;
            List<Route> allRoutes = new List<Route>();
            
            try
            {
                Connect();

                Command.CommandText = "SELECT route_id, name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, st_asgeojson(trip_area, 15, 0) FROM route";

                Reader = Command.ExecuteReader();

                while (Reader.Read())
                {
                    allRoutes.Add(RouteMaker(Reader));
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
                Disconnect();
            }
        }

        public List<Route> GetAllRoutesByOrgId(int orgId, out string errorMessage)
        {
            errorMessage = null;
            List<Route> allRoutesByOrgId = new List<Route>();
            
            try
            {
                Connect();
                
                Command.CommandText = "SELECT route_id, name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, st_asgeojson(trip_area, 15, 0) FROM route WHERE org_id = (@p)";
                Command.Parameters.AddWithValue("p", orgId);

                Reader = Command.ExecuteReader();

                while (Reader.Read())
                {
                    allRoutesByOrgId.Add(RouteMaker(Reader));          
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
                Disconnect();
            }
        }
        
         public List<Route> GetAllRoutesByCreatorId(int createdByUserId, out string errorMessage)
        {
            errorMessage = null;
            List<Route> allRoutesByCreatorId = new List<Route>();
            
            try
            {
                Connect();

                Command.CommandText = "SELECT route_id, name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, st_asgeojson(trip_area, 15, 0) FROM route WHERE created_by_user_id = (@p)";
                Command.Parameters.AddWithValue("p", createdByUserId);
                
                Reader = Command.ExecuteReader();
                
                while (Reader.Read())
                {
                    allRoutesByCreatorId.Add(RouteMaker(Reader));   
                }

                return allRoutesByCreatorId;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                errorMessage = e.ToString();
                throw;
            }
            finally
            {
                Disconnect();
            }
        }
    }
}