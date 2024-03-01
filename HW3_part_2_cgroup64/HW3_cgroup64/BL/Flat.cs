namespace HW2_cgroup64.BL
{
    public class Flat
    {
        int id;
        string city;
        string address;
        int numOfRooms;
        double price;
        static List<Flat> FlatsList = new List<Flat>();

        public int Id { get => id; set => id = value; }
        public string City { get => city; set => city = value; }
        public string Address { get => address; set => address = value; }
        public int NumOfRooms { get => numOfRooms; set => numOfRooms = value; }
        public double Price { get => price; set => price = Discount(value); }


        public Flat(int id, string city, string address, double price, int numOfRooms)
        {
            Id = id;
            City = city;
            Address = address;
            NumOfRooms = numOfRooms;
            Price = price;
        }
        public Flat() { }

        public int Insert()
        {
            DBservices dbs = new DBservices();
            return dbs.InsertFlat(this);
        }
        public List<Flat> Read()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadFlats();
        }

        public double Discount(double val)
        {
            if (numOfRooms > 1 && val > 100)
                return val * 0.9;
            return val;
        }

        //HW1
        static public List<Flat> ReadByMaxAndCity(string city, double price)//return flats in city and under price
        {
            List<Flat> selectedFlats = new List<Flat>();
            foreach (Flat f in FlatsList)
            {
                if (f.city == city && f.price <= price)
                    selectedFlats.Add(f);
            }
            return selectedFlats;
        }
    }
}
