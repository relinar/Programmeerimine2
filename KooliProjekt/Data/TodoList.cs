namespace KooliProjekt.Data
{
    public class TodoList
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<TodoItem> Items { get; set; } = new List<TodoItem>();
    }
}
