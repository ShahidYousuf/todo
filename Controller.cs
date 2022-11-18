using System;
namespace todo
{
    public class Controller
    {
        private Store store;
        public Controller(Store _store)
        {
            store = _store;
        }

        public void ListTodos(string? output)
        {
            List<Todo> todos = store.GetTodos();
            if (output == "completed")
            {
                todos = todos.FindAll(match: item => item.Completed);
            }
            if (output == "pending")
            {
                todos = todos.FindAll(match: item => !item.Completed);
            }
            foreach (var todo in todos)
            {
                todo.Print();
            }
        }

        public void GetTodo(int id)
        {
            Todo? todo = store.GetTodo(id);
            if (todo is not null)
            {
                todo.Print();
            }
            else
            {
                Console.WriteLine("Todo with id {0} not found", id);
            }
        }

        public string CreateTodo(string? title)
        {
            string result = "Todo not created, please try again!";
            if (title is not null) {
                Todo todo = new Todo(title);
                store.WriteTodo(todo);
                result = "Todo created successfully!";
            }
            return result;
        }

        public void EditTodo(int id, string? newTitle)
        {
            if (newTitle is not null)
            {
                store.EditTodo(id, newTitle);
                Console.WriteLine("Todo with id {0} edited successfully.", id);
            }
        }

        public void DeleteTodo(int id)
        {
            store.RemoveTodo(id);
            Console.WriteLine("Todo with id {0} removed successfully.", id);
        }

        public void CheckTodo(int id)
        {
            store.SetTodoChecked(id);
            Console.WriteLine("Todo with id {0} marked completed.", id);
        }

        public void UncheckTodo(int id)
        {
            store.SetTodoUnChecked(id);
            Console.WriteLine("Todo with id {0} marked pending.", id);
        }

        public void Help()
        {
            Console.WriteLine("todo, version 1.0 2022. Developed by Shahid Yousuf.");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Github: https://github.com/ShahidYousuf/todo");
            Console.ResetColor();
            Console.WriteLine("todo <command> [-option] [value]");
            Console.WriteLine("List of commands:");
            Dictionary<string, string> commands_help = new Dictionary<string, string>()
            {
                { "list", "- list all todos, accepts -o options to filter completed or pending todos"},
                { "get", "- get a todo given its index specified by -i option"},
                { "create", "- create a new todo providing its title specified by -t option"},
                { "edit", "- edit a todo title, specified by index -i and new title specified by -t option"},
                { "remove", "- remove a todo given its index specified by -i option"},
                { "check", "- mark a todo complete/checked given its index specified by -i option"},
                { "uncheck", "- mark a todo pending/unchecked given its index specified by -i option"},
                { "help", "- shows this help menu, or help for the above commands specified by -c option"},
            };
            foreach (var (key, value) in commands_help)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("\n{0} ", key);
                Console.ResetColor();
                Console.Write("{0}", value);
            }
            Console.WriteLine();
            Console.WriteLine("For more information about a command, type:");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("todo help -c <command>\n");
            Console.ResetColor();
        }

        public void HelpCommand(string? command)
        {
            if (command == null || command.Trim().Length == 0)
            {
                Help();
            }
            else
            {
                Console.WriteLine("Help for {0}", command);
            }
        }

        public void HelpList()
        {
            Console.WriteLine("Showing help for command `list`");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Description:");
            Console.ResetColor();
            Console.WriteLine("lists all todos, or todos specified by output option -o/--output as completed or pending");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Syntax");
            Console.ResetColor();
            Console.WriteLine("todo list [-o | --output] [completed | pending]");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Examples");
            Console.ResetColor();
            Console.WriteLine("todo list\t - list all todos.");
            Console.WriteLine("todo list -o completed\t - list all completed todos");
            Console.WriteLine("todo list -o pending\t - list all pending todos");
        }

        public void HelpGet()
        {
            Console.WriteLine("Showing help for command `get`");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Description:");
            Console.ResetColor();
            Console.WriteLine("gets a todo specified by index.");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Syntax");
            Console.ResetColor();
            Console.WriteLine("todo get [-i | --index] <index>");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Examples");
            Console.ResetColor();
            Console.WriteLine("todo get -i 5\t - gets a todo with index 5");
            Console.WriteLine("todo get --index 5\t - gets a todo with index 5");
        }

        public void HelpCreate()
        {
            Console.WriteLine("Showing help for command `create`");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Description:");
            Console.ResetColor();
            Console.WriteLine("creates a new todo with title specified by -t or --title option.");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Syntax");
            Console.ResetColor();
            Console.WriteLine("todo create [-t | --title] <title>");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Examples");
            Console.ResetColor();
            Console.WriteLine("todo create -t 'a sample title'\t - creates a new todo with the given title.");
            Console.WriteLine("todo create --title 'a sample title'\t - creates a new todo with the given title.");
        }

        public void HelpEdit()
        {
            Console.WriteLine("Showing help for command `edit`");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Description:");
            Console.ResetColor();
            Console.WriteLine("edits a todo title specified by index provinding new title using title option.");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Syntax");
            Console.ResetColor();
            Console.WriteLine("todo edit [-i | --index] <index> [-t | --title] <title>");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Examples");
            Console.ResetColor();
            Console.WriteLine("todo edit -i 5 -t 'new sample title'\t - edits a todo with index 5, and sets its new title.");
            Console.WriteLine("todo edit --index 5 --title 'new sample title'\t - edits a todo with index 5, and sets its new title");
        }

        public void HelpRemove()
        {
            Console.WriteLine("Showing help for command `remove`");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Description:");
            Console.ResetColor();
            Console.WriteLine("removes a todo from the todo listing specified by index.");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Syntax");
            Console.ResetColor();
            Console.WriteLine("todo remove [-i | --index] <index>");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Examples");
            Console.ResetColor();
            Console.WriteLine("todo remove -i 5\t - removes a todo with index 5");
            Console.WriteLine("todo remove --index 5\t - removes a todo with index 5");
        }

        public void HelpCheck()
        {
            Console.WriteLine("Showing help for command `check`");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Description:");
            Console.ResetColor();
            Console.WriteLine("sets a todo specified by index as checked or completed.");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Syntax");
            Console.ResetColor();
            Console.WriteLine("todo check [-i | --index] <index>");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Examples");
            Console.ResetColor();
            Console.WriteLine("todo check -i 5\t - sets a todo with index 5 as checked or completed");
            Console.WriteLine("todo check --index 5\t - sets a todo with index 5 as checked or completed");
        }

        public void HelpUnCheck()
        {
            Console.WriteLine("Showing help for command `uncheck`");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Description:");
            Console.ResetColor();
            Console.WriteLine("sets a todo specified by index as unchecked or pending.");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Syntax");
            Console.ResetColor();
            Console.WriteLine("todo uncheck [-i | --index] <index>");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Examples");
            Console.ResetColor();
            Console.WriteLine("todo uncheck -i 5\t - sets a todo with index 5 as unchecked or pending");
            Console.WriteLine("todo uncheck --index 5\t - sets a todo with index 5 as unchecked or pending");
        }

        public void HelpHelp()
        {
            Console.WriteLine("Showing help for command `help`");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Description:");
            Console.ResetColor();
            Console.WriteLine("displays detailed help and examples for a command specified.");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Syntax");
            Console.ResetColor();
            Console.WriteLine("todo help [-c | --command] <command>");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Examples");
            Console.ResetColor();
            Console.WriteLine("todo help -c list\t - displays help for command `list`");
            Console.WriteLine("todo help -c create\t - displays help for command `create`");
        }
    }
}

