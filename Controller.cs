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

        public string EditTodo(int id, string? newTitle)
        {
            return "Editing Todo with id " + id.ToString() + " new title " + newTitle;
        }

        public string DeleteTodo(int id)
        {
            return "Deleting Todo with id " + id.ToString();
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

