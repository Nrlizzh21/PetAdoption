using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PetAdoptionAPI.Models
{
    public class Adoptions
    {
        [Key]
        public int AdoptionID { get; set; }

        [Required]
        public int PetID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public string Status { get; set; } = "Pending";

        [Required]
        public DateTime RequestDate { get; set; } = DateTime.UtcNow;

        public string? PetName { get; set; } // Remove [Required]
        public string? UserName { get; set; } // Remove [Required]

        public Pets? Pet { get; set; } // Navigation property
        public Users? User { get; set; } // Navigation property


        

           
        }

    }

