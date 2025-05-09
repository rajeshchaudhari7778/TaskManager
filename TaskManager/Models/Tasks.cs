using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Title field is required.")]
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}