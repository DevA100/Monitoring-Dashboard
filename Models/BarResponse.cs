namespace DashboardProject.Models
{
    public class BarResponse
    {
        public BarResponse()
        {
            categories = new string[] { };
            Values = new int[] { };
        }

        public string[] categories { get; set; }
        public int[] Values { get; set; }
    }
}
