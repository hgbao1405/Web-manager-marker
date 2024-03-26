
using Shared.Archetype;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharacterRepo.Models
{
    public class Image: archetype
    {
        public Image() { }
        [Required]
        public int Id_marker { get; set; }
        [Required]
        public string Link { get; set; }
        [Required]
        public int Serial { get; set; }
        [Required]
        public int Width { get; set; }
        [Required]
        public int Height { get; set; }
        [Required]
        [ForeignKey("Id_marker")]
        public Marker Marker { get; set; }

        public static ImageDTO ConvertDTO(Image i)
        {
            return new ImageDTO
            {
                id=i.id,
                Link=i.Link,
                Width=i.Width,
                Height = i.Height,
                Id_marker = i.Id_marker
            };
        }
        public static Image Convert(ImageDTO i,string Link)
        {
            return new Image
            {
                id = i.id,
                Link = Link,
                Width = i.Width,
                Height = i.Height,
                Id_marker = i.Id_marker
            };
        }

    }
    public class ImageDTO { 
        public int id { get; set; }
        public string Link { get; set; }
        public int Serial { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Id_marker { get; set; }
    }
}