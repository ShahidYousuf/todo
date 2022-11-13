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

        public string TitleSeparator { get; } = "^^^";

        public string Title { get; set; }

        public int Id { get; set; }

        public bool Completed { get; set; }

        public override string ToString()
        {
            string title = EncodeTitle(Title);
            return String.Format("{0} {1} {2}", Id, title, Completed);
        }

        public string EncodeTitle(string title)
        {
            title = title.Replace(" ", TitleSeparator);
            return title;
        }

        public string DecodeTitle(string title)
        {
            title = title.Replace(TitleSeparator, " ");
            return title;
        }

        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(Id);
            Console.ResetColor();
            Console.Write(" {0} ", DecodeTitle(Title));
            Console.ForegroundColor = ConsoleColor.DarkRed;
            if (Completed) Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(Completed);
            Console.ResetColor();
            Console.WriteLine();
        }

    }
}

