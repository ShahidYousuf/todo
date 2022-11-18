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

     
    }
}

