using System.ComponentModel.DataAnnotations.Schema;
using System.Security.AccessControl;
using Shared.Archetype;

namespace CharacterRepo.Models
{
    public class Character: archetype
    {
        public Character() { }
        public int Id_marker { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public string? type { get; set; }
        [ForeignKey("Id_marker")]
        public Marker Marker { get; set; }

        public static CharacterDTO ConvertDTO(Character character)
        {
            return new CharacterDTO
            {
                Id_marker = character.Id_marker,
                name= character.name,
                description= character.description,
                type = character.type,
            };
        }

        public static Character Convert(CharacterDTO character)
        {
            return new Character
            {
                Id_marker = character.Id_marker,
                name = character.name,
                description = character.description,
                type = character.type,
            };
        }
    }
    public class CharacterDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public string? type { get; set; }
        public int Id_marker { get; set; }

    }
}
