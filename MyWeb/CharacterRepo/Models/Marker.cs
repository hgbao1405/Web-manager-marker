using Shared.Archetype;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharacterRepo.Models
{
    public class Marker: archetype
    {
        [Required]
        public float x { get; set; }
        [Required]
        public float y { get; set; }
        [Required]
        public float z { get; set; }
        //phương hướng hình
        public float dgr { get; set; }
        public List<Image> Images { get; set; }
        [ForeignKey("typeid")]
        public TypeMaker type { get; set; }
        [Required]
        public int typeid { get; set; }
        public Character Characters { get; set; }
        public Background Backgrounds { get; set; }
    }
    public class MarkerDTO
    {
        public int id { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public float dgr { get; set; }
        public int typeid { get; set; }
    }
}