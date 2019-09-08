using System;
using Npgsql;

namespace MatakDBConnector
{
    public class Document
    {
        private int _documentId;
        private int _routeId;
        private string _description;
        private DateTime _created;
        private DateTime _updated;
        private int _createdByUserId;
        private int _updatedByUserId;

        public Document(int documentId, int routeId, string d, DateTime created, DateTime updated, int createdByUserId, int updatedByUserId)
        {
            _documentId = documentId;
            this._routeId = routeId;
            this._description = d;
            this._created = created;
            this._updated = updated;
            this._createdByUserId = createdByUserId;
            this._updatedByUserId = updatedByUserId;
        }

        public Document(int routeId, string d, DateTime created, DateTime updated, int createdByUserId, int updatedByUserId)
        {
            this._routeId = routeId;
            this._description = d;
            this._created = created;
            this._updated = updated;
            this._createdByUserId = createdByUserId;
            this._updatedByUserId = updatedByUserId;
        }
        
        public Document DocumentMaker(NpgsqlDataReader reader)
        {
            DocumentId = reader.GetInt32(0);
            RouteId = reader.GetInt32(1);
            Description = reader.GetString(2);
            Created = reader.GetDateTime(3);
            Updated = reader.GetDateTime(4);;
            CreatedByUserId = reader.GetInt32(5);
            UpdatedByUserId = reader.GetInt32(6);
            
            return this;
        }
        
        protected void newDocumentCommandHelper(Document document, NpgsqlCommand command, Boolean isNew)
        {
            command.Parameters.AddWithValue("route_id", document.RouteId);
            command.Parameters.AddWithValue("description", document.Description);
            command.Parameters.AddWithValue("created", document.Created);
            command.Parameters.AddWithValue("updated", document.Updated);
            command.Parameters.AddWithValue("created_by_user_id", document.CreatedByUserId);
            if (isNew)
            {
                command.Parameters.AddWithValue("updated_by_user_id", document.CreatedByUserId);
            }
            else
            {
                command.Parameters.AddWithValue("updated_by_user_id", document.UpdatedByUserId);
            }
        }

        public int DocumentId
        {
            get => _documentId;
            set => _documentId = value;
        }

        public int RouteId
        {
            get => _routeId;
            set => _routeId = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }

        public DateTime Created
        {
            get => _created;
            set => _created = value;
        }

        public DateTime Updated
        {
            get => _updated;
            set => _updated = value;
        }

        public int CreatedByUserId
        {
            get => _createdByUserId;
            set => _createdByUserId = value;
        }

        public int UpdatedByUserId
        {
            get => _updatedByUserId;
            set => _updatedByUserId = value;
        }
    }
}