using System.Drawing;

namespace MatakDBConnector
{
    public class Vehicle
    {
        private int vehicleId;
        private string plateNumber;
        private int orgId;
        private int photoId;
        private int typeId;
        private string model;
        private string color;
        private string manufacturer;

        public Vehicle()
        {
            vehicleId = 0;
            plateNumber = "0";
            orgId = 0;
            photoId = 0;
            typeId = 0;
            model = "0";
            color = "0";
            manufacturer = "0";
        }

        public Vehicle(int vehicleId, string plateNumber, int orgId, int photoId, int typeId, string model, string color, string manufacturer)
        {
            this.vehicleId = vehicleId;
            this.plateNumber = plateNumber;
            this.orgId = orgId;
            this.photoId = photoId;
            this.typeId = typeId;
            this.model = model;
            this.color = color;
            this.manufacturer = manufacturer;
        }


        public int VehicleId
        {
            get => vehicleId;
            set => vehicleId = value;
        }

        public string PlateNumber
        {
            get => plateNumber;
            set => plateNumber = value;
        }

        public int OrgId
        {
            get => orgId;
            set => orgId = value;
        }

        public int PhotoId
        {
            get => photoId;
            set => photoId = value;
        }

        public int TypeId
        {
            get => typeId;
            set => typeId = value;
        }

        public string Model
        {
            get => model;
            set => model = value;
        }

        public string Color
        {
            get => color;
            set => color = value;
        }

        public string Manufacturer
        {
            get => manufacturer;
            set => manufacturer = value;
        }
    }
}