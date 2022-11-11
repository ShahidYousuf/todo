// See https://aka.ms/new-console-template for more information

using System;
using todo;
class Application 
{
    static void Main()
    {
        Console.WriteLine("Welcome to todos, the current time is " + DateTime.Now);
        string[] arguments = Environment.GetCommandLineArgs();
        Persistence persistence = Persistence.FILE;
        Console.WriteLine("The persistence is of type {0}", persistence);
        Console.WriteLine("Command Line arguments: {0}", string.Join(", ", arguments));

        Todo todo = new Todo("First todo created");
        todo.Id = 10;
        Console.WriteLine("{0}", todo);
    }

    enum Persistence
    {
        FILE,
        DB,
    }

}

