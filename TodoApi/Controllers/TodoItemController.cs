using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemController : ApiBaseController
    {
        private readonly AppDbContext _context;

        public TodoItemController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Create([FromBody] TodoItem command)
        {
            GenericResponse genericResponse;

            try
            {
                await _context.TodoItem.AddAsync(command);

                await _context.SaveChangesAsync();

                genericResponse = new GenericResponse("Sucessfuly");

                return CustomResponse(genericResponse);
            }
            catch (Exception ex)
            {
                genericResponse = new GenericResponse(ex.Message, EOutputType.Failure);
            }

            return CustomResponse(genericResponse);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<GenericResponse>> Update(int id, [FromBody] TodoItem command)
        {
            GenericResponse genericResponse;

            if (id != command.Id)
            {
                genericResponse = new GenericResponse("Invalid ID", EOutputType.InvalidInput);
                return CustomResponse(genericResponse);
            }

            try
            {
                var todoItem = await _context.TodoItem.FindAsync(id);

                if (todoItem is null)
                {
                    genericResponse = new GenericResponse("TodoItem not found", EOutputType.NotFound);
                    return CustomResponse(genericResponse);
                }

                _context.TodoItem.Update(todoItem);

                await _context.SaveChangesAsync();

                genericResponse = new GenericResponse("Sucessfuly");
            }
            catch (Exception ex)
            {
                genericResponse = new GenericResponse(ex.Message, EOutputType.Failure);
            }

            return CustomResponse(genericResponse);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<GenericResponse>> Delete(int id)
        {
            GenericResponse genericResponse;

            try
            {
                var todoItem = await _context.TodoItem.FindAsync(id);

                if (todoItem is null)
                {
                    genericResponse = new GenericResponse("TodoItem not found", EOutputType.NotFound);
                    return CustomResponse(genericResponse);
                }

                _context.TodoItem.Remove(todoItem);

                await _context.SaveChangesAsync();

                genericResponse = new GenericResponse("Sucessfuly");
            }
            catch (Exception ex)
            {
                genericResponse = new GenericResponse(ex.Message, EOutputType.Failure);
            }

            return CustomResponse(genericResponse);
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