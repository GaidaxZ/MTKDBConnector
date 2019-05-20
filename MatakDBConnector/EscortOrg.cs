using System;
using Npgsql;

namespace MatakDBConnector
{
    public class EscortOrg
    {
        private int _orgId;
        private int _routeId;

        public EscortOrg()
        {
            _orgId = 0;
            _routeId = 0;
        }

        public EscortOrg(int orgId, int routeId)
        {
            _orgId = orgId;
            _routeId = routeId;
        }
        
        protected void newEscortOrgCommandHelper(EscortOrg escortOrg)
        {
            DbConnector.Command.Parameters.AddWithValue("org_id", escortOrg.OrgId);
            DbConnector.Command.Parameters.AddWithValue("route_id", escortOrg.RouteId);
            DbConnector.Command.Parameters.AddWithValue("created", DateTime.Now);
            DbConnector.Command.Parameters.AddWithValue("updated", DateTime.Now);
        }

        public EscortOrg EscortOrgMaker(NpgsqlDataReader reader)
        {
            OrgId = reader.GetInt32(0);
            RouteId = reader.GetInt32(1);
                
            return this;
        }

        public int OrgId
        {
            get => _orgId;
            set => _orgId = value;
        }

        public int RouteId
        {
            get => _routeId;
            set => _routeId = value;
        }
    }
}