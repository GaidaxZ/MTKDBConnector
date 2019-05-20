using System;
using System.Collections.Generic;

namespace MatakDBConnector
{
    public class EscortOrgModel : EscortOrg
    {
        public List<EscortOrg> GetAllEscortOrgs(out string errorMessage)
        {
            List<EscortOrg> allEscortOrgs = new List<EscortOrg>();
            errorMessage = null;

            try
            {
                DbConnector.Connect();
                
                DbConnector.Command.CommandText = "SELECT * FROM escort_org";
                DbConnector.Reader = DbConnector.Command.ExecuteReader();

                while (DbConnector.Reader.Read())
                {
                    EscortOrg escortOrg = new EscortOrg();
                    allEscortOrgs.Add(escortOrg.EscortOrgMaker(DbConnector.Reader));
                }

                return allEscortOrgs;
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
        
        public List<EscortOrg> GetAllEscortOrgsByRouteId(int routeId, out string errorMessage)
        {
            List<EscortOrg> allEscortOrgs = new List<EscortOrg>();
            errorMessage = null;

            try
            {
                DbConnector.Connect();
                
                DbConnector.Command.CommandText = "SELECT * FROM organization WHERE route_id = (@p)";
                DbConnector.Command.Parameters.AddWithValue("p", routeId);
                DbConnector.Reader = DbConnector.Command.ExecuteReader();

                while (DbConnector.Reader.Read())
                {
                    EscortOrg escortOrg = new EscortOrg();
                    allEscortOrgs.Add(escortOrg.EscortOrgMaker(DbConnector.Reader));
                }

                return allEscortOrgs;
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