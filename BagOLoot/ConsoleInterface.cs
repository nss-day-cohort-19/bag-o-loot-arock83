using System;
using System.Collections.Generic;
using System.Linq;

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
            Console.WriteLine ("5. Child Toy Delivery Complete");
            Console.WriteLine ("6. Yuletime Delivery Report");
            Console.WriteLine ("7. Exit");
			Console.Write ("> ");
			return Int32.Parse (Console.ReadLine());
        }

        public void AddChildScreen()
        {
            Console.WriteLine ("Enter child name");
            Console.Write ("> ");
            string childName = Console.ReadLine();
            ChildRegister registry = new ChildRegister();
            bool success = registry.AddChild(childName);
            Console.WriteLine("");
            Console.WriteLine("");
        }

        public void AssignToyToChildScreen()
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("What child should get a toy?");
            ChildRegister childRegistry = new ChildRegister();
            ToyRegister toyRegistry = new ToyRegister();
            Dictionary<int, string> childList = childRegistry.GetChildren();
            int counter = 1;
            Dictionary<int, string> referenceList = new Dictionary<int, string>();
            foreach(var child in childList)
            {
                Console.WriteLine($"{counter}. {child.Value}");
                referenceList.Add(counter, child.Value);
                counter ++;
            }
            Console.WriteLine("> ");
            int childChoice = Int32.Parse(Console.ReadLine());
            int childID = childList.FirstOrDefault(x => x.Value == referenceList[childChoice]).Key;
            Console.WriteLine($"What toy does {childList[childID]} get?");
            Console.WriteLine("> ");
            string toyName = Console.ReadLine();
            toyRegistry.AddToyToChild(toyName, childID);
            Console.WriteLine("");
            Console.WriteLine("");
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
            int childID = Int32.Parse(Console.ReadLine());
            Dictionary<int, string> toyList = toyRegistry.GetAllToysForChild(childID);
            if(toyList.Count > 0)
            {
                //Had to create a reference list to capture the value of user input
                Dictionary<int, string> referenceList = new Dictionary<int, string>();
                int counter2 = 1;
                foreach(var toy in toyList)
                {
                    Console.WriteLine($"{counter2}. {toy.Value}");
                    referenceList.Add(counter2, toy.Value);
                    counter2++;
                }
                Console.Write ("> ");
                int toyChoice = Int32.Parse(Console.ReadLine());

                //Crossreferences the 2 Dictionaries to extract the toyID from the using input (int)
                int toyID = toyList.FirstOrDefault(x => x.Value == referenceList[toyChoice]).Key;
                toyRegistry.RemoveToy(toyID);
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("This child doesn't have any toys :(");
                Console.WriteLine("");
                Console.WriteLine("");
            }
        }

        public void ReviewChildToysScreen()
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Whose toys would you like to see?");
            ChildRegister childRegistry = new ChildRegister();
            Dictionary<int, string> childList = childRegistry.GetChildren();
            int counter = 1;
            //Had to create a reference list to capture the value of user input
            Dictionary<int, string> referenceList = new Dictionary<int, string>();
            foreach(var child in childList)
            {
                Console.WriteLine($"{counter}. {child.Value}");
                referenceList.Add(counter, child.Value);
                counter ++;
            }
            Console.Write ("> ");
            int childChoice = Int32.Parse(Console.ReadLine());
            int childID = childList.FirstOrDefault(x => x.Value == referenceList[childChoice]).Key;
            ToyRegister toyRegistry = new ToyRegister();
            Dictionary<int, string> toyList= toyRegistry.GetAllToysForChild(childID);
            if(toyList.Count >0)
            {
                int counter2 = 1;
                foreach(var toy in toyList)
                {
                    Console.WriteLine($"{counter2}. {toy.Value}");
                }
                Console.WriteLine("");
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("This child doesn't have any toys :(");
                Console.WriteLine("");
                Console.WriteLine("");
            }
        }

        public void AssignChildDeliveryCompleteScreen()
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Which child had their toys delivered?");
            ChildRegister childRegistry = new ChildRegister();
            Dictionary<int, string> childList = childRegistry.GetChildren();
            int counter = 1;

            Dictionary<int, string> referenceList = new Dictionary<int, string>();
            foreach(var child in childList)
            {
                Console.WriteLine($"{counter}. {child.Value}");
                referenceList.Add(counter, child.Value);
                counter ++;
            }
            Console.Write ("> ");
            int childChoice = Int32.Parse(Console.ReadLine());
            childRegistry.IsDelivered(childChoice);
            Console.WriteLine("");
            Console.WriteLine("");
        }

        public void YuletimeDeliveryReportScreen()
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Yuletide Delivery Report");
            Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%");
            ChildRegister childRegistry = new ChildRegister();
            ToyRegister toyRegistry = new ToyRegister();
            Dictionary<int, string> childList = childRegistry.GetAllChildrenWithToys();
                
            foreach(var child in childList)
            {
                Console.WriteLine($"{child.Value}");
                Dictionary<int, string> childToys = toyRegistry.GetAllToysForChild(child.Key);
                int counter = 1;
                foreach(var toy in childToys)
                {
                    Console.WriteLine($"{counter}. {toy.Value}");
                    counter++;
                }
            }
            Console.WriteLine("");
            Console.WriteLine("");    
        }
    }
}