using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class StudentService
    {
        private IList<Student> _students;
        private string _fileName;// = "students.bin";
        public StudentService()
        {
            _fileName = ConfigurationManager.AppSettings["StudentFileName"].ToString();
            if(File.Exists(_fileName))
            {
                using (FileStream fs = new FileStream(_fileName, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    _students = (IList<Student>)bf.Deserialize(fs);
                }
            }
            else
            {
                _students = new List<Student>();
            }
        }
        public void Add(Student student)
        {
            _students.Add(student);
        }
        public void Save()
        {
            using (FileStream fs = new FileStream(_fileName, FileMode.Create, FileAccess.ReadWrite))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, _students);
            }
        }
        public IList<Student> GetAllStudents
        {
            get { return _students; }
        }
        public int CountStudents
        {
            get { return _students.Count(); }
        }
    }
}
