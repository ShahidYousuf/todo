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
        var commandwithOptions = commandParser.CommandWithOptions();
        
        Console.WriteLine("Base command is {0}, Options: ", commandwithOptions.Command);

        if (commandwithOptions.OptionValues.Count > 0)
        {
            foreach (var (key, value) in commandwithOptions.OptionValues)
            {
                Console.WriteLine("{0} = {1}", key, value);

            }
        } else
        {
            Console.WriteLine("No options");
        }
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
        OUTPUT,
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
        public struct BaseCommandWithOptions
        {
           
            public Command Command { get; set; }
            public Dictionary<string, string> OptionValues { get; set; }

        }
        public BaseCommandWithOptions CommandWithOptions()
        {
            // todo list -o complted
            // todo edit -i 5 -t "hello world"

            BaseCommandWithOptions commandWithOptions = new BaseCommandWithOptions();
            Dictionary<string, string> map = new Dictionary<string, string>();
            map.Add("", "");
            commandWithOptions.OptionValues = map;
            switch (Arguments.Length)
            {
                case 1:
                    commandWithOptions.Command = GetCommand();
                    break;
                case 2:
                    commandWithOptions.Command = GetCommand();
                    break;
                case 3:
                    commandWithOptions.Command = Command.HELP;
                    map.Add("-c", Arguments[1]);
                    commandWithOptions.OptionValues = map;
                    break;
                case 4:
                    commandWithOptions.Command = GetCommand();
                    map.Add(Arguments[2], Arguments[3]);
                    commandWithOptions.OptionValues = map;
                    break;
                case 5:
                    commandWithOptions.Command = Command.HELP;
                    map.Add("-c", Arguments[1]);
                    commandWithOptions.OptionValues = map;
                    break;
                case 6:
                    commandWithOptions.Command = GetCommand();
                    map.Add(Arguments[2], Arguments[3]);
                    map.Add(Arguments[4], Arguments[5]);
                    commandWithOptions.OptionValues = map;
                    break;
                default:
                    commandWithOptions.Command = Command.HELP;
                    break;
            }
            return commandWithOptions;
                
        }

        private Command GetCommand()
        {
            if (Arguments.Length <= 1) return Command.HELP;
            switch (Arguments[1])
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

