# todo
A console based todo application :pushpin: written in C# 11 and .NET 7 with file persistence.

Sample help response

![Sample todo help output](https://github.com/ShahidYousuf/todo/blob/master/todo_ss.png)

##### Display information and general help
```bash
todo
```

##### Display help about any command, say `list`
```bash
todo help -c list
```

##### Create a new todo
```bash
todo create -t "This is a sample todo title"
```

##### List all todos, includes both completed and pending todos
```bash
todo list
```

##### List filtered todos
```bash
todo list -o completed
```
```bash
todo list -o pending
```

##### Get a particular todo item by its index
```bash
todo get -i 5
```

##### Edit a particular todo item given its index, and set a new title
```bash
todo edit -i 5 -t "This is the new title for this todo"
```

##### Mark a todo completed/checked
```bash
todo check -i 5
```

##### Mark a todo pending/unchecked
```bash
todo uncheck -i 5
```

##### Remove a todo from listing
```bash
todo remove -i 5
```
