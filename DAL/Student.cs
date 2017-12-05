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
        private string _imageSmall;

        public int Id {
            get { return _id; }
            set { _id = value; }
        }
        public string Image
        {
            get { return _image; }
            set { _image = value; }
        }
        public string ImageSmall
        {
            get { return _imageSmall; }
            set { _imageSmall = value; }
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


        public void AddMark(Mark m)
        {
            try
            {
                if (_marks.Count() == 0)
                    _marks = new List<Mark>();
            }
            catch
            { _marks = new List<Mark>(); }
            _marks.Add(m);
        }
        public void DeleteMark(int index)
        {
            _marks.RemoveAt(index);
        }

        public int CountMarks
        {
            get { return _marks.Count(); }
        }

    }
}
