// See https://aka.ms/new-console-template for more information

using System;
class Application 
{
    static void Main()
    {
        Console.WriteLine("Welcome to todos, the current time is " + DateTime.Now);
        string[] arguments = Environment.GetCommandLineArgs();
        Console.WriteLine("Command Line arguments: {0}", string.Join(", ", arguments));
    }

}

