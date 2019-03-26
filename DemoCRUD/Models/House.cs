using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DemoCRUD.Models
{
    public class House
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Acreage { get; set; }
        [Required]
        public string Caption { get; set; }
        [Required]
        public string Type { get; set; }
        [Required(ErrorMessage = "*Please enter your address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "*Please enter your name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "*Please enter your phone number")]
        public string Phone { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "*Please enter price")]
        public string Price { get; set; }
        [Required(ErrorMessage ="*Images is required")]
        public List<IFormFile> ListImageFile { get; set; }
        public string FirstImageName { get; set; }
    }
    
}
