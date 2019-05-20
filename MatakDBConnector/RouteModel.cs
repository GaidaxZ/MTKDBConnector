using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace MatakDBConnector
{
    public class RouteModel : Route
    {
        public int AddNewRoute(Route newRoute, out string errorMessage)
        {
            //TODO: change to store procedures
            errorMessage = null;
            
            try
            {
                DbConnector.Connect();
                
                DbConnector.Command.CommandText =
                    "INSERT INTO route (name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, created, updated, trip_area) VALUES (@name, @start_datetime, @end_datetime, @geojson_doc_id, @reason_id, @priority_id, @status_id, @org_id, @created_by_user_id, @sent_to_user_id, @approved_by_user_id, @note, @created, @updated, st_geomfromgeojson(@trip_area)) RETURNING route_id";
                newRouteCommandHelper(newRoute);

                return Convert.ToInt32(DbConnector.Command.ExecuteScalar());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                errorMessage = e.ToString();
                throw;
            }
            finally
            {
                DbConnector.Disconnect();
            }
        }

        public int AddNewRoute(string name, DateTime startDateTime, DateTime endDateTime, int reasonId,
            int priorityId, int statusId, int orgId, int createdByUserId, int sentToUserId, string note, string geoJsonString, out string errorMessage)
        {
            Route newRoute = new Route(0, name, startDateTime, endDateTime, GeojsonDocId, reasonId, priorityId, 
                statusId, orgId, createdByUserId, sentToUserId, 0, note, geoJsonString );
            errorMessage = null;

            try
            {
                DbConnector.Connect();

                DbConnector.Command.CommandText =
                    "INSERT INTO route (name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, created, updated, trip_area) VALUES (@name, @start_datetime, @end_datetime, @geojson_doc_id, @reason_id, @priority_id, @status_id, @org_id, @created_by_user_id, @sent_to_user_id, @approved_by_user_id, @note, @created, @updated, st_geomfromgeojson(@trip_area)) RETURNING route_id";
                newRouteCommandHelper(newRoute);

                return Convert.ToInt32(DbConnector.Command.ExecuteScalar());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                errorMessage = e.ToString();
                throw;
            }
            finally
            {
                DbConnector.Disconnect();
            }
        }
        
        public int UpdateRouteId(Route newRoute, int routeId, out string errorMessage)
        {
            errorMessage = null;
            
            try
            {
                DbConnector.Connect();
                
                DbConnector.Command.CommandText = "UPDATE route SET name = (@name), start_datetime = (@start_datetime), end_datetime = (@end_datetime), geojson_doc_id = (@geojson_doc_id), reason_id = (@reason_id), priority_id = (@priority_id), status_id = (@status_id), org_id = (@org_id), created_by_user_id = (@created_by_user_id), sent_to_user_id = (@sent_to_user_id), approved_by_user_id = (@approved_by_user_id), note = (@note), created = (@created), updated = (@updated), trip_area = st_geomfromgeojson(@trip_area) WHERE route_id = (@routeId) RETURNING route_id";
                DbConnector.Command.Parameters.AddWithValue("routeId", routeId);
                newRouteCommandHelper(newRoute);

                return Convert.ToInt32(DbConnector.Command.ExecuteScalar());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                errorMessage = e.ToString();
                throw;
            }
            finally
            {
                DbConnector.Disconnect();
            }
        }
        
        public Route GetRouteById(int routeId, out string errorMessage)
        {
            errorMessage = null;
            
            try
            {
                DbConnector.Connect();
                
                DbConnector.Command.CommandText = "SELECT route_id, name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, st_asgeojson(trip_area, 15, 0) FROM route WHERE route_id = (@routeId)";
                DbConnector.Command.Parameters.AddWithValue("routeId", routeId);
                DbConnector.Reader = DbConnector.Command.ExecuteReader();

                while (DbConnector.Reader.Read())
                {
                    return RouteMaker(DbConnector.Reader);
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
                DbConnector.Disconnect();
            }
        }
        
        public int GetRoutesCountByOrgId(int orgId, out string errorMessage)
        {   
            errorMessage = null;
            var count = -1;
            
            try
            {
                DbConnector.Connect();
                
                DbConnector.Command.CommandText = "SELECT count(*) FROM route WHERE org_id = (@orgId)";
                DbConnector.Command.Parameters.AddWithValue("orgId", orgId);
                DbConnector.Reader = DbConnector.Command.ExecuteReader();

                while (DbConnector.Reader.Read())
                {
                     count = DbConnector.Reader.GetInt32(0);
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
                DbConnector.Disconnect();
            }
        }

        public List<Route> GetAllRoutes(out string errorMessage)
        {
            errorMessage = null;
            List<Route> allRoutes = new List<Route>();
            
            try
            {
                DbConnector.Connect();

                DbConnector.Command.CommandText = "SELECT route_id, name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, st_asgeojson(trip_area, 15, 0) FROM route";
                DbConnector.Reader = DbConnector.Command.ExecuteReader();

                while (DbConnector.Reader.Read())
                {
                    Route route = new Route();
                    allRoutes.Add(route.RouteMaker(DbConnector.Reader));
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
                DbConnector.Disconnect();
            }
        }

        public List<Route> GetAllRoutesByOrgId(int orgId, out string errorMessage)
        {
            errorMessage = null;
            List<Route> allRoutesByOrgId = new List<Route>();
            
            try
            {
                DbConnector.Connect();
                
                DbConnector.Command.CommandText = "SELECT route_id, name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, st_asgeojson(trip_area, 15, 0) FROM route WHERE org_id = (@orgId)";
                DbConnector.Command.Parameters.AddWithValue("orgId", orgId);
                DbConnector.Reader = DbConnector.Command.ExecuteReader();

                while (DbConnector.Reader.Read())
                {
                    Route route = new Route();
                    allRoutesByOrgId.Add(route.RouteMaker(DbConnector.Reader));          
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
                DbConnector.Disconnect();
            }
        }
        
         public List<Route> GetAllRoutesByCreatorId(int createdByUserId, out string errorMessage)
        {
            errorMessage = null;
            List<Route> allRoutesByCreatorId = new List<Route>();
            
            try
            {
                DbConnector.Connect();

                DbConnector.Command.CommandText = "SELECT route_id, name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, st_asgeojson(trip_area, 15, 0) FROM route WHERE created_by_user_id = (@createdByUserId)";
                DbConnector.Command.Parameters.AddWithValue("createdByUserId", createdByUserId);
                DbConnector.Reader = DbConnector.Command.ExecuteReader();
                
                while (DbConnector.Reader.Read())
                {
                    Route route = new Route();
                    allRoutesByCreatorId.Add(route.RouteMaker(DbConnector.Reader));   
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
                DbConnector.Disconnect();
            }
        }
    }
}