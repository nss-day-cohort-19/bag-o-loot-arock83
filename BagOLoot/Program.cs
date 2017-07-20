using System;
using System.Collections.Generic;
using System.Linq;

namespace BagOLoot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var db = new DatabaseInterface();
            var tr = new ToyRegister();
            db.CheckChildTable();
            db.CheckToyTable();

            Console.WriteLine ("WELCOME TO THE BAG O' LOOT SYSTEM");
            Console.WriteLine ("*********************************");
            Console.WriteLine ("1. Add a child");
            Console.WriteLine ("2. Assign Toy to a child");
            Console.WriteLine ("3. Revoke Toy from a child");
            Console.WriteLine ("4. Review Child's Toy List");
            Console.WriteLine ("5. Child Toy Delivery Complete");
            Console.WriteLine ("6. Yuletide Delivery Report");
			Console.Write ("> ");

			// Read in the user's choice
			int choice;
			Int32.TryParse (Console.ReadLine(), out choice);

            
            if (choice == 1)
            {
                Console.WriteLine ("Enter child name");
                Console.Write ("> ");
                string childName = Console.ReadLine();
                ChildRegister registry = new ChildRegister();
                bool childId = registry.AddChild(childName);
                Console.WriteLine(childId);
            }
            if (choice ==2)
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
                int childID = Int32.Parse(Console.ReadLine());
                Console.WriteLine($"What toy does {childList[childID]} get?");
                Console.WriteLine("> ");
                string toyName = Console.ReadLine();
                tr.AddToyToChild(toyName, childID);
 
            }
            if(choice == 3)
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
            if(choice == 4)
            {
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
                int counter2 = 1;
                foreach(var toy in toyList)
                {
                    Console.WriteLine($"{counter2}. {toy.Value}");
                }
            }

            if(choice == 5)
            {
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

            }
            if(choice == 6)
            {
                Console.WriteLine("Yuletide Delivery Report");
                Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%");
                ChildRegister childRegistry = new ChildRegister();
                ToyRegister toyRegistry = new ToyRegister();
                Dictionary<int, string> childList = childRegistry.GetChildren();
                
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

            }
        }
    }
}
