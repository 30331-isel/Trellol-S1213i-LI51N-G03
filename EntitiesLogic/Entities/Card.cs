using System;
using System.ComponentModel.DataAnnotations;

namespace EntitiesLogic.Entities
{
    public class Card : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Board Name")]
        public string BoardName { get; set; }

        [Required]
        [Display(Name = "List Name")]
        public string ListName { get; set; }

        [Required]
        [Display(Name = "Card Name")]
        public string Name { get; set; }

        [Display(Name = "Archive Card")]
        public bool isArchived { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [Required]
        [Range(1, short.MaxValue, ErrorMessage = "Enter a value bigger than {1}")]
        [Display(Name = "Position")]
        public int Position { get; set; }

    }
}
