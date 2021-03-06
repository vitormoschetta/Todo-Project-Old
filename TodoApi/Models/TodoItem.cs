using FluentValidation.Results;
using TodoApi.Validations;

namespace TodoApi.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public bool Done { get; set; }
        public string Title { get; set; }

        public ValidationResult Validate()
        {
            return new TodoItemValidator().Validate(this);
        }
    }
}