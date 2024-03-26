using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace MyWeb.Model
{
    public class Character: archetype
    {
        public Character() { }
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }
    }
}
