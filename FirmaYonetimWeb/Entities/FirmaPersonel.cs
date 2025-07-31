using FirmaYonetimWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace FirmaYonetimWeb.Entities
{
    public class FirmaPersonel
    {
        public List<AppUser> users { get; set; }

       public CreateUserModel createUser { get; set; }
       
       public UpdateUserModel updateUser { get; set; }
    }
}
