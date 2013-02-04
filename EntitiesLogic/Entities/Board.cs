using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntitiesLogic.Entities
{
    public class Board : IEntity
    {
        private LinkedList<List> _lists = new LinkedList<List>();

        [Required]
        [Key]
        [RegularExpression(@"^[\w\d ]*$", ErrorMessage="Invalid character(s).")]
        [StringLength(30, MinimumLength=3)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(256, MinimumLength = 3)]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}