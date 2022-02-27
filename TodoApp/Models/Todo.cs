using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class Todo
    {
        public int Id { get; set; }
        
        public bool Done { get; set; }

        [Required(ErrorMessage = "Informe o título")]
        public string Title { get; set; }
    }
}