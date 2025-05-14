using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class Tasks
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Title field is required.")]
        public string Title { get; set; } = string.Empty; // Initialize to empty string

        public string? Description { get; set; } // Make nullable since it's optional

        public bool IsCompleted { get; set; }

        public DateTime CreatedAt { get; set; }


        public DateTime? DueDate { get; set; }
    }
}