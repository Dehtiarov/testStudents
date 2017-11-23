using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [Serializable]
    public class Student
    {
        private int _id;
        private string _name;        
        private IList<Mark> _marks;
        private string _image;

        public string Image
        {
            get { return _image; }
            set { _image = value; }
        }


        public IList<Mark> Marks
        {
            get { return _marks; }
            set { _marks = value; }
        }


        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

    }
}
