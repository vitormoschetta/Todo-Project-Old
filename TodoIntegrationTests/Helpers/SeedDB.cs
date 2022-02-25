using System.Collections.Generic;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoIntegrationTests.Helpers
{
    public static class SeedDB
    {
        public static void InitializeDbForTests(AppDbContext db)
        {
            db.TodoItem.AddRange(GetSeedingMessages());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(AppDbContext db)
        {
            db.TodoItem.RemoveRange(db.TodoItem);
            InitializeDbForTests(db);
        }

        public static List<TodoItem> GetSeedingMessages()
        {
            return new List<TodoItem>()
            {
                new TodoItem(){ Id = 1, Title = "Todo 1", Done = false },
                new TodoItem(){ Id = 2, Title = "Todo 2",  Done = true }
            };
        }
    }
}