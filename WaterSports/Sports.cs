namespace WaterSports
{
    public abstract class Sports : ISports
    {
        private string service_chosen;
        private string sport_type;

        public Sports()
        {
        }

        public Sports(string service_chosen, string sport_type)
        {
            this.service_chosen = service_chosen;
            this.sport_type = sport_type;
        }
        public string Service_Chosen
        {
            get { return service_chosen; }
            set { service_chosen = value; }
        }

        public string Sport_type
        {
            get { return sport_type; }
            set { sport_type = value; }
        }
    }
}
