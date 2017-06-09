using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hci.Models
{
    public class Classroom
    {
    
        private string id;
        private string description;
        private int size;
        private bool haveProjector;
        private bool haveBoard;
        private bool haveSmartBoard;
        private string operatingSys;
        private ObservableCollection<Software> softwares = new ObservableCollection<Software>();

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

        public bool HaveProjector
        {
            get
            {
                return haveProjector;
            }

            set
            {
                haveProjector = value;
            }
        }

        public bool HaveBoard
        {
            get
            {
                return haveBoard;
            }

            set
            {
                haveBoard = value;
            }
        }

        public bool HaveSmartBoard
        {
            get
            {
                return haveSmartBoard;
            }

            set
            {
                haveSmartBoard = value;
            }
        }

        public string OperatingSys
        {
            get
            {
                return operatingSys;
            }

            set
            {
                operatingSys = value;
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

        public void Load(Software data)
        {
            softwares.Add(data);
        }

        public Classroom(string id, string description, int size, bool haveProjector, bool haveBoard, bool haveSmartBoard, string operatingSys, ObservableCollection<Software> softwares)
        {
            this.Id = id;
            this.Description = description;
            this.Size = size;
            this.HaveProjector = haveProjector;
            this.HaveBoard = haveBoard;
            this.HaveSmartBoard = haveSmartBoard;
            this.OperatingSys = operatingSys;
            this.Softwares = softwares;
        }
           
        public Classroom() { }
   
    }
}
