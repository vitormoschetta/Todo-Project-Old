using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public bool Done { get; set; }
        public string Title { get; set; }
    }
}