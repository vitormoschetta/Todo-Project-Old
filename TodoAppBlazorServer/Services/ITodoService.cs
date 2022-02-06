using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.Services
{
    public interface ITodoService
    {
        Task<(HttpStatusCode, IEnumerable<Todo>)> GetAll();
    }
}