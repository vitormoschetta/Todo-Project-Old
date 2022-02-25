using FluentValidation;
using TodoApi.Models;

namespace TodoApi.Validations
{
    public class TodoItemValidator : AbstractValidator<TodoItem>
    {
        public TodoItemValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MinimumLength(5);
        }
    }
}