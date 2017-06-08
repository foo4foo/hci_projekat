using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hci.Models
{
    public class Course
    {

        private string id;
        private string name;
        private string date;
        private string description;

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        public Course(string id, string name, string date, string description)
        {
            this.Id = id;
            this.Name = name;
            this.Date = date;
            this.Description = description;
        }
        public Course() { }
    }
}
