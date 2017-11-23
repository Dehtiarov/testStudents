using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleStudentApp
{
    class Program
    {
        static void Main(string[] args)
        {

            StudentService studService = new StudentService();
            int action = 0;
            do
            {
                Console.WriteLine("1. Add student.");
                Console.WriteLine("2. Show all students");
                Console.WriteLine("3. Exit");
                Console.Write("->_");
                action = int.Parse(Console.ReadLine());
                switch (action)
                {
                    case 1:
                        {
                            //Student st = new Student();
                            Console.Write("Name: ");
                            string name = Console.ReadLine();
                            Student newStud = new Student
                            {
                                Name = name,
                                Image = Guid.NewGuid().ToString()
                            };
                            studService.Add(newStud);
                            studService.Save();
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("-------Show list users-------");
                            foreach (var item in studService.GetAllStudents)
                            {
                                Console.WriteLine("Image:\t{0}", item.Image);
                                Console.WriteLine("Name:\t{0}\n",item.Name);
                            }
                            break;
                        }
                    default:
                        break;
                }

            } while (action!=3);

            //Student
        }
    }
}
