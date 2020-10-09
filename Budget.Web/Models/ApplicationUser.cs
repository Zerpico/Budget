using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Budget.Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        public int FinanceUserId { get; set; }

        public DateTime? LastLoginDate { get; set; }
        
    }

    
}
