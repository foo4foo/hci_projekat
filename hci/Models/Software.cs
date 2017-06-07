using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hci.Models
{
    public class Software
    {
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Developer
        {
            get { return developer; }
            set { developer = value; }
        }

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        public string Site
        {
            get { return site; }
            set { site = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Os
        {
            get { return os; }
            set { os = value; }
        }

        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        private int id;
        private string name;
        private string developer;
        private string site;
        private string description;
        private string os;
        private int year;
        private int size;
        private double price;

        public Software(int _id, string _name, string _developer, string _site, string _description,
            string _os, int _year, int _size, double _price)
        {
            this.id = _id;
            this.name = _name;
            this.developer = _developer;
            this.site = _site;
            this.description = _description;
            this.os = _os;
            this.year = _year;
            this.size = _size;
            this.price = _price;
        }

        public Software() { }

        public override string ToString()
        {
            return Id + " " + Name + " " + Description + " " + Developer + " " + Os + " " + Year 
                + " " + Site + " " + Size + " " + Price;
        }
    }
}
