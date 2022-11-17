using System;
using System.Text;
namespace todo
{
    public class Store
    {
        string? HomeDirPath = "";
        string TODO_DB = Path.DirectorySeparatorChar + ".todo.db";
        string TODO_INDEX = Path.DirectorySeparatorChar + ".todo_index.db";

        string DBPath;
        string DBIndexPath;
        int Index { get; set; }
        
        public Store()
        {
            HomeDirPath = Environment.GetEnvironmentVariable("HOME");
            if (HomeDirPath == null)
            {
                throw new Exception("Missing Environment variable `HOME`, have you set it?");
            }
            DBPath = HomeDirPath + TODO_DB;
            DBIndexPath = HomeDirPath + TODO_INDEX;
            InitFileDB();
        }

        private void InitFileDB()
        {
            if (HomeDirPath is not null)
            {

                FileStream db_file = File.Open(DBPath, FileMode.OpenOrCreate);
                FileStream index_file = File.Open(DBIndexPath, FileMode.OpenOrCreate, FileAccess.Read);
                StreamReader reader = new StreamReader(index_file);
                string? readIndex = reader.ReadLine();
              
                if (readIndex is not null)
                {
                    int.TryParse(readIndex, out int index);
                    Index = index;
                 
                } else
                {
                    Index = 0;
                }
                reader.Close();
                db_file.Close();
               
            }
        }


        public void WriteTodo(Todo todo)
        {
            FileStream db_file_stream = File.Open(DBPath, FileMode.Append, FileAccess.Write);
            todo.Id = Index + 1;
            StreamWriter writer = new StreamWriter(db_file_stream);
            writer.WriteLine("{0}", todo);
            writer.Close();
            db_file_stream.Close();
            SaveIndex();
        }

        public void SaveIndex()
        {
            FileStream db_index_stream = File.Open(DBIndexPath, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(db_index_stream);
            writer.WriteLine(Index + 1);
            writer.Close();
            db_index_stream.Close();
        }

        public List<Todo> GetTodos()
        {
            FileStream db_file_stream = File.Open(DBPath, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(db_file_stream);
            List<Todo> todos = new List<Todo>();
            while (!reader.EndOfStream)
            {
                string? data = reader.ReadLine();
                if (data is null)
                {
                    break;
                }
                string[] parts = data.Split(" ");
                if (parts.Length < 3)
                {
                    continue;
                }
                int.TryParse(parts[0], out int index);
                string title = parts[1];
                bool.TryParse(parts[2], out bool completed);
                Todo todo = new Todo(title);
                todo.Id = index;
                todo.Completed = completed;
                todos.Add(todo);
            }
            reader.Close();
            db_file_stream.Close();
            return todos;

        }

        public Todo? GetTodo(int id)
        {
            string index = id.ToString();
            FileStream db_file_stream = File.Open(DBPath, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(db_file_stream);
            while (!reader.EndOfStream)
            {
                string? data = reader.ReadLine();
                if (data is null)
                {
                    break;
                }
                string[] parts = data.Split(" ");
                if (parts.Length < 3)
                {
                    continue;
                }
                if (index == parts[0])
                {
                    Todo todo = new Todo(parts[1]);
                    todo.Id = id;
                    bool.TryParse(parts[2], out bool completed);
                    todo.Completed = completed;
                    return todo;
                }
            }
            reader.Close();
            db_file_stream.Close();
            return null;
        }


        public void SetTodoChecked(int id)
        {
            FileStream db_file_stream = File.Open(DBPath, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(db_file_stream);
            List<Todo> todos = new List<Todo>();
            while (!reader.EndOfStream)
            {
                string? data = reader.ReadLine();
                if (data is null)
                {
                    break;
                }
                string[] parts = data.Split(" ");
                if (parts.Length < 3)
                {
                    continue;
                }
                Todo todo = new Todo(parts[1]);
                int.TryParse(parts[0], out int index);
                todo.Id = index;
                if (index == id)
                {
                    todo.Completed = true;
                } else
                {
                    bool.TryParse(parts[2], out bool completed);
                    todo.Completed = completed;
                }
                todos.Add(todo);
            }
            reader.Close();
            db_file_stream.Close();
            Refresh_DB(todos);
        }

        public void SetTodoUnChecked(int id)
        {
            FileStream db_file_stream = File.Open(DBPath, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(db_file_stream);
            List<Todo> todos = new List<Todo>();
            while (!reader.EndOfStream)
            {
                string? data = reader.ReadLine();
                if (data is null)
                {
                    break;
                }
                string[] parts = data.Split(" ");
                if (parts.Length < 3)
                {
                    continue;
                }
                Todo todo = new Todo(parts[1]);
                int.TryParse(parts[0], out int index);
                todo.Id = index;
                if (index == id)
                {
                    todo.Completed = false;
                }
                else
                {
                    bool.TryParse(parts[2], out bool completed);
                    todo.Completed = completed;
                }
                todos.Add(todo);
            }
            reader.Close();
            db_file_stream.Close();
            Refresh_DB(todos);
        }

        public void RemoveTodo(int id)
        {
            FileStream db_file_stream = File.Open(DBPath, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(db_file_stream);
            List<Todo> todos = new List<Todo>();
            while (!reader.EndOfStream)
            {
                string? data = reader.ReadLine();
                if (data is null)
                {
                    break;
                }
                string[] parts = data.Split(" ");
                if (parts.Length < 3)
                {
                    continue;
                }
                Todo todo = new Todo(parts[1]);
                int.TryParse(parts[0], out int index);
                todo.Id = index;
                if (index == id)
                {
                    continue;
                }
                bool.TryParse(parts[2], out bool completed);
                todo.Completed = completed;
                todos.Add(todo);
            }
            reader.Close();
            db_file_stream.Close();
            Refresh_DB(todos);
        }

        public void EditTodo(int id, string title)
        {
            FileStream db_file_stream = File.Open(DBPath, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(db_file_stream);
            List<Todo> todos = new List<Todo>();
            while (!reader.EndOfStream)
            {
                string? data = reader.ReadLine();
                if (data is null)
                {
                    break;
                }
                string[] parts = data.Split(" ");
                if (parts.Length < 3)
                {
                    continue;
                }
                Todo todo = new Todo(parts[1]);
                int.TryParse(parts[0], out int index);
                todo.Id = index;
                if (index == id)
                {
                    todo.Title = title;
                }
                bool.TryParse(parts[2], out bool completed);
                todo.Completed = completed;
                todos.Add(todo);
            }
            reader.Close();
            db_file_stream.Close();
            Refresh_DB(todos);
        }

        public void Refresh_DB(List<Todo> todos)
        {
            todos = todos.OrderBy(t => t.Id).ToList();
            FileStream db_file_stream = File.Open(DBPath, FileMode.Truncate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(db_file_stream);
            foreach (var todo in todos)
            {
                writer.WriteLine("{0}", todo);
            }
            writer.Close();
            db_file_stream.Close();
        }





    }
}

