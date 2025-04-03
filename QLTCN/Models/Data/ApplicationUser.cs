using Microsoft.AspNetCore.Identity;

namespace QLTCCN.Models.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
