using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hci.Models
{
    public class Software
    {
        private string dbID;

        public string DbId
        {
            get { return dbID; }
            set { dbID = value; }
        }

        private bool deleted = false;

        public bool Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }

        public string Id
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

        private string id;
        private string name;
        private string developer;
        private string site;
        private string description;
        private string os;
        private int year;
        private double price;

        public Software(string _id, string _name, string _developer, string _site, string _description,
            string _os, int _year, double _price, bool _deleted)
        {
            this.id = _id;
            this.name = _name;
            this.developer = _developer;
            this.site = _site;
            this.description = _description;
            this.os = _os;
            this.year = _year;
            this.price = _price;
            this.Deleted = _deleted;
        }

        public Software() { }

        public override string ToString()
        {
            return Id + " " + Name + " " + Description + " " + Developer + " " + Os + " " + Year 
                + " " + Site + " " + Price;
        }
    }
}
