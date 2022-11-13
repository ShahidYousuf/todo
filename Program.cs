﻿// See https://aka.ms/new-console-template for more information

using System;
using todo;
class Application 
{
    static void Main()
    {
        Console.WriteLine("Welcome to todos, the current time is " + DateTime.Now);
        //Persistence persistence = Persistence.FILE;
        //Console.WriteLine("The persistence is of type {0}", persistence);
        CommandParser commandParser = new CommandParser();
        Store store = new Store();
        Controller controller = new Controller(store);
        Router router = new Router(commandParser, controller);
        router.Route();
       
    }

}

