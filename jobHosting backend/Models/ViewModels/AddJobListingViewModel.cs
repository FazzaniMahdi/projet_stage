using JobHosting.Models;

namespace jobHosting.Models.ViewModels
{
    public class AddJobListingViewModel
    {
        public int JobId { get; set; }
        public String JobName { get; set; } = String.Empty;
        public String JobDescription { get; set; } = String.Empty;
        public DateTime ExpirationDate { get; set; } = DateTime.MinValue;
        public List<String> JobRequirements { get; set; } = new List<String>();
        public List<String> Missions { get; set; } = new List<String>();
        public String JobLocation { get; set; } = String.Empty;
        public double JobHourlyPay { get; set; } = Double.MinValue;
        public String JobType { get; set; } = String.Empty;
        public int JobPositionsAvailable { get; set; } = 0;
        public string JobsListerId { get; set; } = string.Empty;

        override
        public String ToString()
        {
            return ($"{JobName} ${JobDescription} ${JobLocation} ${JobHourlyPay} ${JobsListerId}");
        }
    }
}

