// See https://aka.ms/new-console-template for more information

using System;
using todo;
class Application 
{
    static void Main()
    {
     
        CommandParser commandParser = new CommandParser();
        Store store = new Store();
        Controller controller = new Controller(store);
        Router router = new Router(commandParser, controller);
        router.Route();
       
    }

}

