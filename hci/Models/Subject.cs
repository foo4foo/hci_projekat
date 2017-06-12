using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hci.Models
{
    public class Subject
    {

        private string dbID;

        public string DbId
        {
            get { return dbID; }
            set { dbID = value; }
        }

        private string id;
        private string name;
        private string description;
        private int size;
        private int minLength;
        private int noOfClasses;
        private bool needProjector;
        private bool needBoard;
        private bool needSmartBoard;
        private string os;
        private Course smer;
        private ObservableCollection<Software> softwares = new ObservableCollection<Software>();
        private ObservableCollection<string> testS = new ObservableCollection<string>();

        private bool deleted = false;

        public bool Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }



        public ObservableCollection<string> TestS
        {
            get { return testS; }
            set { testS = value; }
        }

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

        public int Size
        {
            get
            {
                return size;
            }

            set
            {
                size = value;
            }
        }

        public int MinLength
        {
            get
            {
                return minLength;
            }

            set
            {
                minLength = value;
            }
        }

        public int NoOfClasses
        {
            get
            {
                return noOfClasses;
            }

            set
            {
                noOfClasses = value;
            }
        }

        public bool NeedProjector
        {
            get
            {
                return needProjector;
            }

            set
            {
                needProjector = value;
            }
        }

        public bool NeedBoard
        {
            get
            {
                return needBoard;
            }

            set
            {
                needBoard = value;
            }
        }

        public bool NeedSmartBoard
        {
            get
            {
                return needSmartBoard;
            }

            set
            {
                needSmartBoard = value;
            }
        }

        public string Os
        {
            get
            {
                return os;
            }

            set
            {
                os = value;
            }
        }

        public Course Smer
        {
            get
            {
                return smer;
            }

            set
            {
                smer = value;
            }
        }

        public ObservableCollection<Software> Softwares
        {
            get
            {
                return softwares;
            }

            set
            {
                softwares = value;
            }
        }

        public Subject(string id, string name, string description, int size, int minLength, int noOfClasses, bool needProjector, bool needBoard, bool needSmartBoard, string os, Course smer, ObservableCollection<Software> softwares)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Size = size;
            this.MinLength = minLength;
            this.NoOfClasses = noOfClasses;
            this.NeedProjector = needProjector;
            this.NeedBoard = needBoard;
            this.NeedSmartBoard = needSmartBoard;
            this.Os = os;
            this.Smer = smer;
            this.Softwares = softwares;
        }

        public Subject() { }
    }  
}   
