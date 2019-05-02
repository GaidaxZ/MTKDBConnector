namespace MatakDBConnector
{
    public class Organization
    {
        private int orgId;
        private string name;
        private int mainUserId;
        private int countryId;
        private int addressId;
        private int faxId;
        private int phoneId;

        public Organization()
        {
            orgId = 0;
            name = "0";
            mainUserId = 0;
            countryId = 0;
            addressId = 0;
            faxId = 0;
            phoneId = 0;
        }

        public Organization(int orgId, string name, int mainUserId, int countryId, int addressId, int faxId, int phoneId)
        {
            this.orgId = orgId;
            this.name = name;
            this.mainUserId = mainUserId;
            this.countryId = countryId;
            this.addressId = addressId;
            this.faxId = faxId;
            this.phoneId = phoneId;
        }

        public int OrgId
        {
            get => orgId;
            set => orgId = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public int MainUserId
        {
            get => mainUserId;
            set => mainUserId = value;
        }

        public int CountryId
        {
            get => countryId;
            set => countryId = value;
        }

        public int AddressId
        {
            get => addressId;
            set => addressId = value;
        }

        public int FaxId
        {
            get => faxId;
            set => faxId = value;
        }

        public int PhoneId
        {
            get => phoneId;
            set => phoneId = value;
        }
    }
}