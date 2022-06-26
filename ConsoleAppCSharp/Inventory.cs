using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppCSharp
{
    public class Inventory
    {
        public Place[] places = { new Place(), new Place(), new Place(), new Place(), new Place() };
        public string getPlace(int num) { return places[num].getText(); }
        public Inventory() { }
    }
}
