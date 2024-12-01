using Microsoft.AspNetCore.Mvc;

namespace PetAdoptionAPI.Models
{
    public class ReportViewModel
    {
        public List<Users> Users { get; set; }
        public List<Pets> Pets { get; set; }
        public List<Adoptions> Adoptions { get; set; }
    }

}
