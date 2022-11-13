using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console_Generic_Repository.Model;
using ConsoleGenericRepository.DataAccessLayer;

namespace SalaryConsoleGenericRepository
{
    class Program
    {
        private static UnitOfWork unitOfWork = new UnitOfWork();

        static void Main(string[] args)
        {
            bool programRun = true;
            while (programRun)
            {
                Console.WriteLine("Employees Information>> Press 'L' to view Employee list, 'A' to add, 'E' to edit, 'D' to delete, 'C' to clean, 'X' to exit");
                string command = Console.ReadLine();
                if (command == "L")
                {
                    Index();
                }
                else if (command == "A")
                {
                    Console.Write("name: ");
                    var name = Console.ReadLine();

                    Console.Write("designation: ");
                    var designation = Console.ReadLine();

                    var employee = new Employee();
                    employee.EmployeeName = name;
                    employee.Designation = designation;

                    Create(employee);
                }
                else if (command == "E")
                {
                    Console.Write("id: ");
                    var id = Console.ReadLine();
                    var employee = Details(Convert.ToInt32(id));
                    if (employee != null)
                    {
                        Console.WriteLine("id: " + employee.EmployeeId + "  name: " + employee.EmployeeName + "   designation: " + employee.Designation);

                        Console.Write("name: ");
                        var name = Console.ReadLine();

                        Console.Write("designation: ");
                        var designation = Console.ReadLine();

                        employee.EmployeeName = name;
                        employee.Designation = designation;

                        Edit(employee);
                    }
                }
                else if (command == "D")
                {
                    Console.Write("id: ");
                    var id = Console.ReadLine();
                    var employee = Details(Convert.ToInt32(id));
                    if (employee != null)
                    {
                        Console.WriteLine("id: " + employee.EmployeeId + "  name: " + employee.EmployeeName + "   designation: " + employee.Designation);
                        Console.WriteLine("Are you sure to delete, press 'Y' For Yes to continue, 'N' to back");
                        string confirm = Console.ReadLine();
                        if (confirm == "Y")
                        {
                            DeleteConfirmed(employee.EmployeeId);
                        }
                        else if (confirm == "N")
                        {
                            Console.Clear();
                        }
                    }
                }
                else if (command == "X")
                {
                    programRun = false;
                }
                else if (command == "C")
                {
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Wrong command");
                }
            }
        }

        // GET: Teacher
        public static void Index()
        {
            var employees = unitOfWork.EmployeeRepository.Get(includeProperties: "");
            foreach (var employee in employees.ToList())
            {
                Console.WriteLine("id: " + employee.EmployeeId + "  name: " + employee.EmployeeName + "   designation: " + employee.Designation);
            }
        }

        public static void Create(Employee employee)
        {
            try
            {
                unitOfWork.EmployeeRepository.Insert(employee);
                unitOfWork.Save();
                Console.WriteLine("New Employee added successfully.");
            }
            catch (DataException dex)
            {
                Console.WriteLine(dex);
            }
        }

        public static Employee Details(int id)
        {
            Employee employee = unitOfWork.EmployeeRepository.GetByID(id);
            return employee;
        }

        public static void Edit(Employee employee)
        {
            try
            {
                unitOfWork.EmployeeRepository.Update(employee);
                unitOfWork.Save();
                Console.WriteLine("Employee updated successfully.");
            }
            catch (DataException dex)
            {
                Console.WriteLine(dex);
            }
        }

        public static void DeleteConfirmed(int id)
        {
            Employee employee = unitOfWork.EmployeeRepository.GetByID(id);
            unitOfWork.EmployeeRepository.Delete(id);
            unitOfWork.Save();
            Console.WriteLine("Employee deleted successfully.");
        }


    }
}
