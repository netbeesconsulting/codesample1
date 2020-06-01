using System.ComponentModel.DataAnnotations;

namespace Stocky.Model.Admin
{
    public class CustomerModel 
    {
        public long Id { get; set; }
        [Required]
        public string Company { get; set; }
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
