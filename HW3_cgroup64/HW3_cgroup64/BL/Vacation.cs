namespace HW2_cgroup64.BL
{
    public class Vacation
    {
        int id;
        string userEmail;
        int flatId;
        DateTime startDate;
        DateTime endDate;
        //static List <Vacation> vacationList=new List<Vacation>();
    
        public Vacation(int id, string userEmail, int flatId, DateTime startDate, DateTime endDate)
        {
            Id = id;
            UserEmail = userEmail;
            FlatId = flatId;
            StartDate = startDate;
            EndDate = endDate;
        }

        public Vacation() { }

        public int Id { get => id; set => id = value; }
        public string UserEmail { get => userEmail; set => userEmail = value; }
        public int FlatId { get => flatId; set => flatId = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }

        public int insert()
        {
            DBservices dbs = new DBservices();
            List<Vacation> vacationList= dbs.ReadVacations();

            if (vacationList.Any(vac => (vac.flatId == this.flatId) && 
            (!(vac.endDate<this.startDate || vac.startDate>this.endDate))))
            {
                return 0;
            }
            return dbs.InsertVacation(this);
        }
        static public List<Vacation> Read()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadVacations();
        }

        //HW2
        //static public List<Vacation> ReadByDates(DateTime startDate, DateTime endDate)
        //{
        //    List<Vacation> selectedVac=new List<Vacation>();
        //    foreach(Vacation vac in vacationList)
        //    {
        //        if((vac.endDate <= endDate) && (vac.startDate >= startDate))
        //            selectedVac.Add(vac);
        //    }
        //    return selectedVac;
        //}
    }
}
