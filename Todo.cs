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

        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(Id);
            Console.ResetColor();
            Console.Write(" {0} ", Title);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            if (Completed) Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(Completed);
            Console.ResetColor();
            Console.WriteLine();
        }

    }
}

