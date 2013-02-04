using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace EntitiesLogic.Entities
{
    public class List : IEntity
    {
        private IEnumerable<Card> _cards = new LinkedList<Card>();


        public string BoardName { get; set; }
        
        [Required]
        [Key]
        [RegularExpression(@"^[\w\d ]*$", ErrorMessage = "Invalid character(s).")]
        [StringLength(20, MinimumLength = 3)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Range(1, short.MaxValue, ErrorMessage = "Enter a value bigger than {1}")]
        public short Position { get; set; }

    }
}
