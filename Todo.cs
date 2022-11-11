using System;
namespace todo
{
    public class Todo
    {
        public Todo(string title)
        {
            Title = title;
            Completed = false;
        }

        public string Title { get; set; }

        public int Id { get; set; }

        public bool Completed { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1} {2}", Id, Title, Completed);
        }
    }
}

