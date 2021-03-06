﻿using System;
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
        private static int counter = 1000;
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
            if (_students.Count != 0)
                student.Id = this.GetAllStudents[this.CountStudents - 1].Id + 1;
            else
                student.Id = counter;
            _students.Add(student);
        }
        public void Edit(Student student, int index)
        {
            this.Delete(index);
            this.Add(student);
        }

        public void Delete(int index)
        {
            _students.RemoveAt(index);
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

        //public Student this[int index]
        //{
        //    get { return _students[index]; }
        //    set { this[index] = value; }
        //}
    }
}
