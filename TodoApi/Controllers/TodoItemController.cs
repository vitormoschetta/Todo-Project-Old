using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Models;

namespace src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemController : ControllerBase
    {
        protected readonly AppDbContext _context;

        public TodoItemController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<GenericResponse> Create([FromBody] TodoItem command)
        {
            try
            {
                await _context.TodoItem.AddAsync(command);

                await _context.SaveChangesAsync();

                return new GenericResponse(true, "Create sucessfuly");
            }
            catch (Exception ex)
            {
                return new GenericResponse(false, ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<GenericResponse> Update(int id, [FromBody] TodoItem command)
        {
            if (id != command.Id) return new GenericResponse(false, "Id inválido. ");

            try
            {
                var todoItem = await _context.TodoItem.FindAsync(id);

                if (todoItem is null)
                {
                    return new GenericResponse(false, "TodoItem not found");
                }

                _context.TodoItem.Update(todoItem);

                await _context.SaveChangesAsync();

                return new GenericResponse(true, "Update sucessfuly");
            }
            catch (Exception ex)
            {
                return new GenericResponse(false, ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<GenericResponse> Delete(int id, [FromBody] TodoItem command)
        {
            try
            {
                var todoItem = await _context.TodoItem.FindAsync(id);

                if (todoItem is null)
                {
                    return new GenericResponse(false, "TodoItem not found");
                }

                _context.TodoItem.Remove(todoItem);

                await _context.SaveChangesAsync();

                return new GenericResponse(true, "Create sucessfuly");
            }
            catch (Exception ex)
            {
                return new GenericResponse(false, ex.Message);
            }
        }


        [HttpGet()]
        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            return await _context.TodoItem.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<TodoItem> GetById(int id)
        {
            return await _context.TodoItem.FindAsync(id);
        }
    }
}