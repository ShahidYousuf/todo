using System;

namespace todo
{
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
        COMMAND,
        NONE,
    }

    public class CommandParser
    {
        public CommandParser()
        {
            Arguments = Environment.GetCommandLineArgs();
        }

        public string[] Arguments { get; set; }
        public struct BaseCommandWithOptions
        {

            public Command Command { get; set; }
            public Dictionary<CommandOption, string> OptionValues { get; set; }

        }
        public BaseCommandWithOptions CommandWithOptions()
        {
            // todo list -o complted
            // todo edit -i 5 -t "hello world"

            BaseCommandWithOptions commandWithOptions = new BaseCommandWithOptions();
            Dictionary<CommandOption, string> map = new Dictionary<CommandOption, string>();
            //map.Add(CommandOption.NONE, "");
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
                    map.Add(CommandOption.COMMAND, Arguments[1]);
                    commandWithOptions.OptionValues = map;
                    break;
                case 4:
                    commandWithOptions.Command = GetCommand();
                    map.Add(GetCommandOption(Arguments[2]), Arguments[3]);
                    commandWithOptions.OptionValues = map;
                    break;
                case 5:
                    commandWithOptions.Command = Command.HELP;
                    map.Add(CommandOption.COMMAND, Arguments[1]);
                    commandWithOptions.OptionValues = map;
                    break;
                case 6:
                    commandWithOptions.Command = GetCommand();
                    map.Add(GetCommandOption(Arguments[2]), Arguments[3]);
                    map.Add(GetCommandOption(Arguments[4]), Arguments[5]);
                    commandWithOptions.OptionValues = map;
                    break;
                default:
                    commandWithOptions.Command = Command.HELP;
                    break;
            }
            return FormatBaseCommandWithOptions(commandWithOptions);

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

        private CommandOption GetCommandOption(string option)
        {
            switch (option)
            {
                case "-o":
                case "--output":
                    return CommandOption.OUTPUT;
                case "-i":
                case "--index":
                    return CommandOption.INDEX;
                case "-t":
                case "--title":
                    return CommandOption.TITLE;
                case "-c":
                case "--command":
                    return CommandOption.COMMAND;
                default:
                    return CommandOption.NONE;
            }
        }

        private BaseCommandWithOptions FormatBaseCommandWithOptions(BaseCommandWithOptions commandWithOptions)
        {
            Command command = commandWithOptions.Command;
            var optionValues = commandWithOptions.OptionValues;
            switch (command)
            {
                case Command.LIST:
                    string value = "";
                    if (optionValues.Count > 1)
                    {
                        optionValues.Clear();
                        command = Command.HELP;
                        optionValues.Add(CommandOption.COMMAND, "list");
                    }
                    if (optionValues.TryGetValue(CommandOption.OUTPUT, out value))
                    {
                        if (value == "completed" || value == "pending")
                        {
                            optionValues.Clear();
                            optionValues.Add(CommandOption.OUTPUT, value);
                        }
                        else
                        {
                            command = Command.HELP;
                            optionValues.Clear();
                            optionValues.Add(CommandOption.COMMAND, "list");
                        }

                    }
                    else
                    {
                        optionValues.Clear();
                        command = Command.HELP;
                        optionValues.Add(CommandOption.COMMAND, "list");
                    }
                    commandWithOptions.Command = command;
                    commandWithOptions.OptionValues = optionValues;
                    return commandWithOptions;
                case Command.GET:
                    string gvalue = "";
                    if (optionValues.Count > 1)
                    {
                        optionValues.Clear();
                        command = Command.HELP;
                        optionValues.Add(CommandOption.COMMAND, "get");
                    }
                    if (optionValues.TryGetValue(CommandOption.INDEX, out gvalue))
                    {
                        var isNumeric = int.TryParse(gvalue, out _);
                        if (isNumeric)
                        {
                            optionValues.Clear();
                            optionValues.Add(CommandOption.INDEX, gvalue);
                        }
                        else
                        {
                            optionValues.Clear();
                            command = Command.HELP;
                            optionValues.Add(CommandOption.COMMAND, "get");
                        }
                    }
                    else
                    {
                        optionValues.Clear();
                        command = Command.HELP;
                        optionValues.Add(CommandOption.COMMAND, "get");
                    }
                    commandWithOptions.Command = command;
                    commandWithOptions.OptionValues = optionValues;
                    return commandWithOptions;

                case Command.CREATE:
                    string cvalue = "";
                    if (optionValues.Count > 1)
                    {
                        optionValues.Clear();
                        command = Command.HELP;
                        optionValues.Add(CommandOption.COMMAND, "create");
                    }
                    if (optionValues.TryGetValue(CommandOption.TITLE, out cvalue))
                    {
                        optionValues.Clear();
                        optionValues.Add(CommandOption.TITLE, cvalue);
                    }
                    else
                    {
                        optionValues.Clear();
                        command = Command.HELP;
                        optionValues.Add(CommandOption.COMMAND, "create");
                    }
                    commandWithOptions.Command = command;
                    commandWithOptions.OptionValues = optionValues;
                    return commandWithOptions;

                case Command.EDIT:
                    string eivalue = "";
                    string etvalue = "";
                    if (optionValues.Count != 2)
                    {
                        optionValues.Clear();
                        command = Command.HELP;
                        optionValues.Add(CommandOption.COMMAND, "edit");
                    }
                    if (optionValues.TryGetValue(CommandOption.INDEX, out eivalue))
                    {
                        var isNumeric = int.TryParse(eivalue, out _);
                        if (isNumeric)
                        {
                            optionValues[CommandOption.INDEX] =  eivalue;
                        }
                        else
                        {
                            optionValues.Clear();
                            command = Command.HELP;
                            optionValues.Add(CommandOption.COMMAND, "edit");
                        }
                    }
                    else
                    {
                        optionValues.Clear();
                        command = Command.HELP;
                        optionValues.Add(CommandOption.COMMAND, "edit");

                    }
                    if (optionValues.TryGetValue(CommandOption.TITLE, out etvalue))
                    {
                        optionValues[CommandOption.TITLE] = etvalue;
                    }
                    else
                    {
                        optionValues.Clear();
                        command = Command.HELP;
                        optionValues.Add(CommandOption.COMMAND, "edit");
                    }
                    commandWithOptions.Command = command;
                    commandWithOptions.OptionValues = optionValues;
                    return commandWithOptions;

                case Command.REMOVE:
                    string rvalue = "";
                    if (optionValues.Count > 1)
                    {
                        optionValues.Clear();
                        command = Command.HELP;
                        optionValues.Add(CommandOption.COMMAND, "remove");
                    }
                    if (optionValues.TryGetValue(CommandOption.INDEX, out rvalue))
                    {
                        var isNumeric = int.TryParse(rvalue, out _);
                        if (isNumeric)
                        {
                            optionValues.Clear();
                            optionValues.Add(CommandOption.INDEX, rvalue);
                        }
                        else
                        {
                            optionValues.Clear();
                            command = Command.HELP;
                            optionValues.Add(CommandOption.COMMAND, "remove");
                        }
                    }
                    else
                    {
                        optionValues.Clear();
                        command = Command.HELP;
                        optionValues.Add(CommandOption.COMMAND, "remove");
                    }
                    commandWithOptions.Command = command;
                    commandWithOptions.OptionValues = optionValues;
                    return commandWithOptions;

                case Command.CHECK:
                    string chvalue = "";
                    if (optionValues.Count > 1)
                    {
                        optionValues.Clear();
                        command = Command.HELP;
                        optionValues.Add(CommandOption.COMMAND, "check");
                    }
                    if (optionValues.TryGetValue(CommandOption.INDEX, out chvalue))
                    {
                        var isNumeric = int.TryParse(chvalue, out _);
                        if (isNumeric)
                        {
                            optionValues.Clear();
                            optionValues.Add(CommandOption.INDEX, chvalue);
                        }
                        else
                        {
                            optionValues.Clear();
                            command = Command.HELP;
                            optionValues.Add(CommandOption.COMMAND, "check");
                        }
                    }
                    else
                    {
                        optionValues.Clear();
                        command = Command.HELP;
                        optionValues.Add(CommandOption.COMMAND, "check");
                    }
                    commandWithOptions.Command = command;
                    commandWithOptions.OptionValues = optionValues;
                    return commandWithOptions;

                case Command.UNCHECK:
                    string ucvalue = "";
                    if (optionValues.Count > 1)
                    {
                        optionValues.Clear();
                        command = Command.HELP;
                        optionValues.Add(CommandOption.COMMAND, "uncheck");
                    }
                    if (optionValues.TryGetValue(CommandOption.INDEX, out ucvalue))
                    {
                        var isNumeric = int.TryParse(ucvalue, out _);
                        if (isNumeric)
                        {
                            optionValues.Clear();
                            optionValues.Add(CommandOption.INDEX, ucvalue);
                        }
                        else
                        {
                            optionValues.Clear();
                            command = Command.HELP;
                            optionValues.Add(CommandOption.COMMAND, "uncheck");
                        }
                    }
                    else
                    {
                        optionValues.Clear();
                        command = Command.HELP;
                        optionValues.Add(CommandOption.COMMAND, "uncheck");
                    }
                    commandWithOptions.Command = command;
                    commandWithOptions.OptionValues = optionValues;
                    return commandWithOptions;

                case Command.HELP:
                    string hv = "";
                    if (optionValues.Count > 1)
                    {
                        optionValues.Clear();
                        command = Command.HELP;
                    }
                    if (optionValues.TryGetValue(CommandOption.COMMAND, out hv))
                    {
                        optionValues.Clear();
                        command = Command.HELP;
                        if (hv == "list" || hv == "get" || hv == "edit" || hv == "create" || hv == "remove" || hv == "check" || hv == "uncheck" )
                        {
                            optionValues.Add(CommandOption.COMMAND, hv);
                        }
                        else
                        {
                            optionValues.Clear();
                        }
                        
                    }
                    commandWithOptions.Command = command;
                    commandWithOptions.OptionValues = optionValues;
                    return commandWithOptions;

                default:
                    return commandWithOptions;
            }
        }


    }
}

