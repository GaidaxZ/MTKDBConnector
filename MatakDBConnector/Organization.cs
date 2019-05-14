using Npgsql;

namespace MatakDBConnector
{
    public class Organization : DbConnector
    {
        private int _orgId;
        private string _name;
        private int _mainUserId;
        private int _countryId;
        private int _addressId;
        private int _faxId;
        private int _phoneId;

        public Organization()
        {
            _orgId = 0;
            _name = "0";
            _mainUserId = 0;
            _countryId = 0;
            _addressId = 0;
            _faxId = 0;
            _phoneId = 0;
        }

        public Organization(int orgId, string name, int mainUserId, int countryId, int addressId, int faxId, int phoneId)
        {
            this._orgId = orgId;
            this._name = name;
            this._mainUserId = mainUserId;
            this._countryId = countryId;
            this._addressId = addressId;
            this._faxId = faxId;
            this._phoneId = phoneId;
        }
        
        public Organization OrganizationMaker(NpgsqlDataReader reader)
        {
            Organization organization = new Organization();
            
            organization.OrgId = reader.GetInt32(0);
            organization.Name = reader.GetString(1);
            organization.MainUserId = reader.GetInt32(2);
            organization.CountryId = reader.GetInt32(3);
            organization.AddressId = reader.GetInt32(4);
            organization.FaxId = reader.GetInt32(5);
            organization.PhoneId = reader.GetInt32(8);
                
            return organization;
        }

        public int OrgId
        {
            get => _orgId;
            set => _orgId = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public int MainUserId
        {
            get => _mainUserId;
            set => _mainUserId = value;
        }

        public int CountryId
        {
            get => _countryId;
            set => _countryId = value;
        }

        public int AddressId
        {
            get => _addressId;
            set => _addressId = value;
        }

        public int FaxId
        {
            get => _faxId;
            set => _faxId = value;
        }

        public int PhoneId
        {
            get => _phoneId;
            set => _phoneId = value;
        }
    }
}