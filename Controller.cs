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

        public string ListTodos(string? output)
        {
            if (output == "completed") return "Listing todos -- completed";
            if (output == "pending") return "Listing todos -- pending";
            return "Listing todos -- all";
        }

        public string GetTodo(int id)
        {
            return "Getting Todo with id " + id.ToString();
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

        public string EditTodo(int id, string? newTitle)
        {
            return "Editing Todo with id " + id.ToString() + " new title " + newTitle;
        }

        public string DeleteTodo(int id)
        {
            return "Deleting Todo with id " + id.ToString();
        }

        public string CheckTodo(int id)
        {
            return "Setting the Todo with id " + id.ToString() + " completed";
        }

        public string UncheckTodo(int id)
        {
            return "Setting the Todo with id " + id.ToString() + " pending";
        }

        public string Help()
        {
            return "General Help Menu";
        }

        public string HelpCommand(string? command)
        {
            if (command == null || command.Trim().Length == 0) return Help();
            return "Help for " + command;
        }
    }
}

