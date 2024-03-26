using Microsoft.AspNetCore.Components;
using Shared.Archetype;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterRepo.Models
{
    public class Background:archetype
    {
        public Background() { }
        public string Title { get; set; }
        public string Description { get; set; }
        [ForeignKey("Markerid")]
        public Marker Marker { get; set; }
        public int Markerid { get; set; }
    }
    public class BackgroundDTO
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Marker Marker { get; set; }
    }
}
