﻿using System;
namespace todo
{
    public class Router
    {
        public CommandParser parser;
        public Controller controller;
        public Router(CommandParser commandParser, Controller _controller)
        {
            parser = commandParser;
            controller = _controller;
        }

        public void Route()
        {
            var commandWithOptions = parser.CommandWithOptions();
            var command = commandWithOptions.Command;
            var optionValues = commandWithOptions.OptionValues;
            var result = "";
            switch (command)
            {
                case Command.LIST:
                    optionValues.TryGetValue(CommandOption.OUTPUT, out string? option);
                    result = controller.ListTodos(option);
                    Console.WriteLine(result);
                    break;
                case Command.GET:

                    optionValues.TryGetValue(CommandOption.INDEX, out string? index);
                    if (index is not null)
                    {
                        int id = int.Parse(index);
                        result = controller.GetTodo(id);
                        Console.WriteLine(result);
                    }
                    break;
                case Command.CREATE:
                    optionValues.TryGetValue(CommandOption.TITLE, out string? title);
                    result = controller.CreateTodo(title);
                    Console.WriteLine(result);
                    break;
                case Command.EDIT:
                    optionValues.TryGetValue(CommandOption.INDEX, out string? eindex);
                    if (eindex is not null)
                    {
                        int eid = int.Parse(eindex);
                        optionValues.TryGetValue(CommandOption.TITLE, out string? etitle);
                        result = controller.EditTodo(eid, etitle);
                        Console.WriteLine(result);
                    }
                    break;
                case Command.REMOVE:
                    optionValues.TryGetValue(CommandOption.INDEX, out string? dindex);
                    if (dindex is not null)
                    {
                        int did = int.Parse(dindex);
                        result = controller.DeleteTodo(did);
                        Console.WriteLine(result);
                    }
                    break;
                case Command.CHECK:
                    optionValues.TryGetValue(CommandOption.INDEX, out string? cindex);
                    if (cindex is not null)
                    {
                        int cid = int.Parse(cindex);
                        result = controller.CheckTodo(cid);
                        Console.WriteLine(result);
                    }
                    break;
                case Command.UNCHECK:
                    optionValues.TryGetValue(CommandOption.INDEX, out string? ucindex);
                    if (ucindex is not null)
                    {
                        int ucid = int.Parse(ucindex);
                        result = controller.UncheckTodo(ucid);
                        Console.WriteLine(result);
                    }
                    break;
                case Command.HELP:
                    optionValues.TryGetValue(CommandOption.COMMAND, out string? comd);
                    result = controller.HelpCommand(comd);
                    Console.WriteLine(result);
                    break;
                default:
                    result = controller.Help();
                    Console.WriteLine(result);
                    break;
            }
        }
    }
}
