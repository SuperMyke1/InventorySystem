using System;

namespace ConsoleAppCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Table table = new Table();
            Inventory inventory = new Inventory();

            bool selectionTable = true;
            bool readyToReplace = false;
            int selection = 0;
            int targetFrom = -1;
            int targetTo = -1;
            bool ftst = false;
            bool stst = false;
            void replace(int from, int to) 
            {
                Place bufer;
                if (ftst && stst) { bufer = table.places[from]; table.places[from] = table.places[to]; table.places[to] = bufer; }
                else if (!ftst && stst) { bufer = inventory.places[from]; inventory.places[from] = table.places[to]; table.places[to] = bufer; }
                else if (ftst && !stst) { bufer = table.places[from]; table.places[from] = inventory.places[to]; inventory.places[to] = bufer; }
                else if (!ftst && !stst) { bufer = inventory.places[from]; inventory.places[from] = inventory.places[to]; inventory.places[to] = bufer; }
                targetFrom = -1;
                targetTo = -1;
                ftst = false;
                stst = false;
                readyToReplace = false;
            }
            void replaceOne(int from, int to)
            {
                if ((stst && table.places[to].getType() != Type.empty) || (!stst && inventory.places[to].getType() != Type.empty)) return;
                if (ftst && table.places[from].getCount() < 2)
                {
                    if (stst) { table.places[to] = new Place(table.places[from].getType(), table.places[to].getCount() + 1); table.places[from] = new Place(); }
                    else if (!stst) { inventory.places[to] = new Place(table.places[from].getType(), inventory.places[to].getCount() + 1); table.places[from] = new Place(); }
                    targetFrom = -1;
                    targetTo = -1;
                    ftst = false;
                    stst = false;
                    readyToReplace = false;
                    return;
                }
                else if (!ftst && inventory.places[from].getCount() < 2)
                {
                    if (stst) { table.places[to] = new Place(inventory.places[from].getType(), table.places[to].getCount() + 1); inventory.places[from] = new Place(); }
                    else if (!stst) { inventory.places[to] = new Place(inventory.places[from].getType(), inventory.places[to].getCount() + 1); inventory.places[from] = new Place(); }
                    targetFrom = -1;
                    targetTo = -1;
                    ftst = false;
                    stst = false;
                    readyToReplace = false;
                    return;
                }
                    
                if (ftst && stst) { inventory.places[to] = new Place(table.places[from].getType(), table.places[to].getCount() + 1); table.places[from].setCount(table.places[from].getCount()-1); }
                else if (!ftst && stst) { table.places[to] = new Place(inventory.places[from].getType(), table.places[to].getCount() + 1); inventory.places[from].setCount(inventory.places[from].getCount() - 1); }
                else if (ftst && !stst) { inventory.places[to] = new Place(table.places[from].getType(), inventory.places[to].getCount() + 1); table.places[from].setCount(table.places[from].getCount() - 1); }
                else if (!ftst && !stst) { table.places[to] = new Place(inventory.places[from].getType(), inventory.places[to].getCount() + 1); inventory.places[from].setCount(inventory.places[from].getCount() - 1); }
            }
            void resetTargets()
            {
                targetFrom = -1;
                targetTo = -1;
                readyToReplace = false;
                ftst = false;
                stst = false;
            }
            void write()
            {
                Console.Clear();
                Console.WriteLine("\t\t\t TABLE ITEMS:");
                for (int i = 0; i < 3; i++)
                {
                    Console.Write(table.getPlace(i));
                    if (selectionTable && selection == i)
                        Console.Write("\t\t\t\t [SELECTED]");
                    if (targetFrom == i && ftst)
                        Console.Write("\t [TARGET FROM]");
                    if (targetTo == i && stst)
                        Console.Write("\t [TARGET TO]");
                    Console.Write("\n");
                    
                }
                Console.WriteLine("\n\n\n\n\n\t\t\t INVENTORY ITEMS");
                for (int i = 0; i < 5; i++)
                {
                    Console.Write(inventory.getPlace(i));
                    if (!selectionTable && selection == i)
                        Console.Write("\t\t\t\t [SELECTED]");
                    if (targetFrom == i && !ftst)
                        Console.Write("\t [TARGET FROM]");
                    if (targetTo == i && !stst)
                        Console.Write("\t [TARGET TO]");
                    Console.Write("\n");
                }
                if(readyToReplace)
                    Console.Write("\n\n\n PRESS Z TO REPLACE TARGETS \n\n\n PRESS X TO REPLACE ONE ITEM FROM STACK \n\n\n PRESS C TO RESET TARGETS");
            }

            ConsoleKeyInfo cki;
            Console.TreatControlCAsInput = true;
            Console.WriteLine("use WS to select places and E to pick place for change items");
            Console.WriteLine("use Q to switch selection from table to inventory or from inventory to table");
            Console.WriteLine("ENTER to continue ESC to exit");
                do { cki = Console.ReadKey(); } 
                while (cki.Key != ConsoleKey.Escape && cki.Key != ConsoleKey.Enter);
            Console.Clear();
            write();
            do
            {
                cki = Console.ReadKey();

                switch (cki.Key)
                {
                    case ConsoleKey.W: if (selection != 0) selection -= 1; ; break;
                    case ConsoleKey.S: if ( (selection !=4 && !selectionTable) || (selection != 2&& selectionTable) ) selection += 1; break;
                    case ConsoleKey.Q: selectionTable = !selectionTable; selection = 0; break;
                    case ConsoleKey.E:
                        if (targetFrom == -1)
                        {
                            targetFrom = selection;
                            if (targetFrom != -1 && selectionTable)
                                ftst = true;
                            else ftst = false;
                        }
                        else if (targetTo == -1)
                        {
                            targetTo = selection;
                            if (targetTo != -1 && selectionTable)
                                stst = true;
                            else stst = false;
                        }
                        break;
                    case ConsoleKey.Z: if (targetFrom != -1 && targetTo != -1) replace(targetFrom, targetTo); break;
                    case ConsoleKey.X: if (targetFrom != -1 && targetTo != -1) replaceOne(targetFrom, targetTo); break;
                    case ConsoleKey.C: resetTargets(); break;
                    default: break;
                }

                if (targetFrom > -1 && targetTo > -1)
                    readyToReplace = true;
                write();
            }
            while (cki.Key != ConsoleKey.Escape);
        }
    }
}
