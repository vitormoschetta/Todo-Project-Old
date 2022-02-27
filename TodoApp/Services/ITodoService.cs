using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.Services
{
    public interface ITodoService
    {
        Task<(HttpStatusCode, IEnumerable<Todo>)> GetAll();
        Task<(HttpStatusCode, GenericResponse)> Add(Todo todo);
        Task<(HttpStatusCode, GenericResponse)> Delete(int id);
    }
}