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