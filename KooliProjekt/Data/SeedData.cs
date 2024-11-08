using KooliProjekt.Data;
using System.Linq;

namespace KooliProjekt.Data
{
    public static class SeedData
    {
        public static void Generate(ApplicationDbContext context)
        {
            if (context.amount.Any())
            {
                return;
            }

            //var list = new TodoList
            //{
            //    Title = "List 1",
            //    Items = new List<TodoItem>
            //    {
            //        new TodoItem { Title = "Item 1.1" },
            //        new TodoItem { Title = "Item 1.2" },
            //        new TodoItem { Title = "Item 1.3" },
            //        new TodoItem { Title = "Item 1.4" },
            //        new TodoItem { Title = "Item 1.5" },
            //        new TodoItem { Title = "Item 1.6" },
            //        new TodoItem { Title = "Item 1.7" },
            //        new TodoItem { Title = "Item 1.8" },
            //        new TodoItem { Title = "Item 1.9" },
            //        new TodoItem { Title = "Item 1.10" }
            //    }
            //};

            //context.TodoLists.Add(list);

            context.SaveChanges();
        }
    }
}