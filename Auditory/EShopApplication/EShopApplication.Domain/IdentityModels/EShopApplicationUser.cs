using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EShopApplication.Domain.DomainModels;

namespace EShopApplication.Domain.IdentityModels
{
    public class EShopApplicationUser : IdentityUser
    {
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }
        
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
        
        [Display(Name = "Address")]
        public string? Address { get; set; }
        
        public virtual ShoppingCart? ShoppingCart { get; set; }
    }
}