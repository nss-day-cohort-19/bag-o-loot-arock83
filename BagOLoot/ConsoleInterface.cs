using System;
using System.Collections.Generic;

using Microsoft.Data.Sqlite;

namespace BagOLoot
{
    public class ConsoleInterface
    {
        public int MainMenuScreen()
        {
            Console.WriteLine ("WELCOME TO THE BAG O' LOOT SYSTEM");
            Console.WriteLine ("*********************************");
            Console.WriteLine ("1. Add a child");
            Console.WriteLine ("2. Assign Toy to a child");
            Console.WriteLine ("3. Revoke Toy from a child");
            Console.WriteLine ("4. Review Child's Toy List");
			Console.Write ("> ");
            int choice;
			return Int32.Parse (Console.ReadLine());
        }

        public void AddChildScreen()
        {
            Console.WriteLine ("Enter child name");
            Console.Write ("> ");
        }

        public void AssignToyToChildScreen()
        {
            ChildRegister registry = new ChildRegister();
            Dictionary<int, string> childList = registry.GetChildren();
            int counter = 1;
            foreach(var child in childList)
            {
                Console.WriteLine($"{counter}. {child.Value}");
                counter ++;
            }
            Console.WriteLine("> ");
        }

        public void RevokeToyFromChildScreen()
        {
            ChildRegister childRegistry = new ChildRegister();
            ToyRegister toyRegistry = new ToyRegister();
            Dictionary<int, string> childList = childRegistry.GetChildren();
            int counter = 1;
            foreach(var child in childList)
            {
                Console.WriteLine($"{counter}. {child.Value}");
                counter ++;
            }
            Console.Write ("> ");
        }


    }
}