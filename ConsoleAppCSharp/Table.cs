using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppCSharp
{
    public class Table
    {
        public Place[] places = { new Place(Type.shadowamulet), new Place(Type.coin, 33), new Place(Type.book) };
        public string getPlace(int num) { return places[num].getText(); }
        public Table() { }
    }
}
