// See https://aka.ms/new-console-template for more information

using System;
using todo;
class Application 
{
    static void Main()
    {
        Console.WriteLine("Welcome to todos, the current time is " + DateTime.Now);
        Persistence persistence = Persistence.FILE;
        Console.WriteLine("The persistence is of type {0}", persistence);
        CommandParser commandParser = new CommandParser();
        Command command = commandParser.GetCommand();
        Console.WriteLine("Base command is {0}", command);

        Todo todo = new Todo("First todo created");
        todo.Id = 10;
        todo.Print();
    }

    enum Persistence
    {
        FILE,
        DB,
    }

    public enum Command
    {
        LIST,
        GET,
        CREATE,
        EDIT,
        REMOVE,
        CHECK,
        UNCHECK,
        HELP,
    }

    public enum CommandOption
    {
        COMPLETED,
        PENDING,
        INDEX,
        TITLE,
    }

    class CommandParser
    {
        public CommandParser()
        {
            Arguments = Environment.GetCommandLineArgs();
        }

        public string[] Arguments { get; set; }

        private string BaseCommand()
        {
            if (Arguments.Length >= 2)
            {
                return Arguments[1];
            }
            return "help";
        }

        public Command GetCommand()
        {
            switch (BaseCommand())
            {
                case "list":
                    return Command.LIST;
                case "get":
                    return Command.GET;
                case "create":
                    return Command.CREATE;
                case "edit":
                    return Command.EDIT;
                case "remove":
                    return Command.REMOVE;
                case "check":
                    return Command.CHECK;
                case "uncheck":
                    return Command.UNCHECK;
                case "help":
                    return Command.HELP;
                default:
                    return Command.HELP;
            }
        }


    }

}

