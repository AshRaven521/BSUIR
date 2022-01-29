namespace uwp_test
{
    public class City
    {

        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public override string ToString()
        {
            return Name;
        }

    }
}