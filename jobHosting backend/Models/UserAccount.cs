using Microsoft.AspNetCore.Identity;

namespace JobHosting.Models
{
    public class UserAccount : IdentityUser
    {
        public string UserType { get; set; } = string.Empty;
        public string JHFullName { get; set; } = string.Empty;
        public string JHResume { get; set; } = string.Empty;
        public string JListerName { get; set; } = string.Empty;
        public string JListerWebsite { get; set; } = string.Empty;
        public string ProfilePhoto { get; set; } = string.Empty;
        public List<JobListing> Listings { get; set; } = new List<JobListing>();

        public UserAccount()
        {

        }

        public UserAccount(string Email, string UserName, string UserType)
        {
            this.Email = Email;
            this.UserName = UserName;
            this.UserType = UserType;
        }
        
        override
        public String ToString()
        {
            return $"{Id} {UserName} {UserType} {Email} {Listings}";
        }
    }
}
