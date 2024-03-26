using System.ComponentModel.DataAnnotations;

namespace CharacterRepo.Models
{
    public class TypeMaker
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string icon { get; set; }
    }
}