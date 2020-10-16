using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Budget.Web.Models
{
    public class ApplicationUser : IdentityUser
    {        
        public ICollection<Payment> Payments { get; set; }
        
    }

    
}
