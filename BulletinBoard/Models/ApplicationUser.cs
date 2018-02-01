using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Project.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<GameOffer> GameOffers { get; set; }
    }
}
