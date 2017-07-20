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
            var ci = new ConsoleInterface();
            db.CheckChildTable();
            db.CheckToyTable();

            int choice;

			// Read in the user's choice
			do 
            {
                choice = ci.MainMenuScreen();
			
                switch(choice)
                {
                    case 1:
                        ci.AddChildScreen();
                        break;
                    case 2:
                        ci.AssignToyToChildScreen();
                        break;
                    case 3:
                        ci.RevokeToyFromChildScreen();
                        break;
                    case 4:
                        ci.ReviewChildToysScreen();
                        break;
                    case 5:
                        ci.AssignChildDeliveryCompleteScreen();
                        break;
                    case 6:
                        ci.YuletimeDeliveryReportScreen();
                        break;
                }
            } while (choice != 7);
        }
    }
}
