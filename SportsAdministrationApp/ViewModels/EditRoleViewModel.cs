using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsAdministrationApp.ViewModels
{
    public class EditRoleViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You must enter role name")]
        public string RoleName { get; set; }
        public List<string> Users { get; set; }
    }
}
