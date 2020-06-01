using System.ComponentModel.DataAnnotations;

namespace Stocky.Model.Admin
{
    public class CategoryModel
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
