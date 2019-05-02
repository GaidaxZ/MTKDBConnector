namespace MatakDBConnector
{
    public class Status
    {
        private int id;
        private string description;
        private string color;

        public Status()
        {
            id = 0;
            description = "0";
            color = "0";
        }

        public Status(int id, string description, string color)
        {
            this.id = id;
            this.description = description;
            this.color = color;
        }

        public int Id
        {
            get => id;
            set => id = value;
        }

        public string Description
        {
            get => description;
            set => description = value;
        }

        public string Color
        {
            get => color;
            set => color = value;
        }
    }
}