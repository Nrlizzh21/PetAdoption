
using System;
using System.ComponentModel.DataAnnotations;
namespace PetAdoptionAPI.Models
{
    public class Pets
    {
        [Key]
        public int PetID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Age { get; set; }

        [Required]
        [MaxLength(100)]
        public string Breed { get; set; }

        [Required]
        [MaxLength(100)]
        public string AdoptionStatus { get; set; }
        
    }
}


