using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppCSharp
{
    public enum Type { empty = 0, coin, book, shadowamulet};
    public class Place
    {
        Type type;
        int count;
        public Place(Type type, int count)
        { this.type = type; this.count = count; }
        public Place()
        { type = Type.empty; count = 0; }
        public Place(Type type)
        { this.type = type; count = 1; }
        public string getText()
        {
            string r = "\t";
            switch (type)
            {
                case Type.empty: r="\t EMPTY\t"; break;
                case Type.coin: r = "\t COIN(" + count.ToString() +")\t"; break;
                case Type.book: r = "\t BOOK\t"; break;
                case Type.shadowamulet: r = "\t AMULET\t"; break;
                default: r = "\t error!\t"; break;
            }
            return r;           
        }
        public int getCount(){ return count; }
        public void setCount(int count) { this.count = count; }
        public Type getType() { return type; }
        public void setType(Type type) { this.type=type; }
    }
}
