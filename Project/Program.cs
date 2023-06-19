using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
class Program
{
    public static readonly string[] MainMenuOptions = { "1 - Add Employee", "2 - Show All Employees", "3 - Find Employee By Name", "4 - Find Employee By Position", "5 - Exit" };
    public static readonly string[] EmployeeMenuOptions = { "1 - Delete Employee", "2 - Change Position", "3 - Count Salary", "4 - Change Worked Time", "5 - Exit to Main Menu" };
    public static List<Employee> Employees = new List<Employee>();
    public static readonly string dbPath = Path.GetFullPath("EmployeeDB.txt");


    static void ShowMenu(string[] options)
    {
        Console.WriteLine("Please select action: ");
        foreach (var option in options)
            Console.WriteLine(option);
    }

    static void AddEmployee()
    {
        Console.WriteLine("Please enter Name: ");
        var name = Console.ReadLine();

        Console.WriteLine("Please enter Date Of Employment in format 'DD-MM-YYYY': ");
        var dateOfEmployment = Convert.ToDateTime(Console.ReadLine());

        Console.WriteLine("Please enter Position: ");
        var position = Console.ReadLine();

        Console.WriteLine("Please enter Identification Code: ");
        var identificationCode = Console.ReadLine();

        Console.WriteLine("Please enter Work Experience in months: ");
        var workExperience = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Please enter type of Employee (1 - Hourly, 2 - Salaried): ");
        var type = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine($"Please enter {(type == 1 ? "Hourly" : "Salary")} rate: ");
        var rate = Convert.ToDouble(Console.ReadLine());

        Employee employee;
        if (type == 1)
            employee = new HourlyEmployee(name, dateOfEmployment, position, identificationCode, workExperience) { Rate = rate };
        else
            employee = new SalariedEmployee(name, dateOfEmployment, position, identificationCode, workExperience) { Rate = rate };

        if (Employees.Count > 0 && IsExistingEmployee(employee))
            Console.WriteLine("The employee alreasy exists.");
        else
        {
            Employees.Add(employee);
            SaveEmployeesToDB();
            Console.WriteLine("The employee was added successfully.");
        }
    }

    static void DeleteEmployee(Employee employee)
    {
        Employees.Remove(employee);
        SaveEmployeesToDB();
        Console.WriteLine("The employee was deleted successfully.");
    }

    static bool IsExistingEmployee(Employee employee)
    {
        if (Employees.Where(e => e.IdentificationCode == employee.IdentificationCode) != null)
            return true;
        else
            return false;
    }

    static void ReadEmployeesFromDB(string path)
    {
        using (StreamReader st = new StreamReader(path, System.Text.Encoding.Default))
        {
            string line;
            while ((line = st.ReadLine()) != null)
            {
                string[] info = line.Split('\t');
                if (info == null) return;
                if (info[0] == "HourlyEmployee")
                    Employees.Add(new HourlyEmployee(info[1].Trim(), Convert.ToDateTime(info[2].Trim()), info[3].Trim(), info[4].Trim(), Convert.ToInt32(info[5].Trim())) { Rate = Convert.ToDouble(info[6].Trim()) });
                else
                    Employees.Add(new SalariedEmployee(info[1].Trim(), Convert.ToDateTime(info[2].Trim()), info[3].Trim(), info[4].Trim(), Convert.ToInt32(info[5].Trim())) { Rate = Convert.ToDouble(info[6].Trim()) });

            }
        }
    }

    static void ShowAllEmployees()
    {
        var sortedList = Employees.OrderBy(e => e.Name).ToList();
        Console.WriteLine(String.Format("{0,5}{1,5}{2,5}{3,5}{4,5}{5,5}{5,5}", "Type", "tName", "Date Of Employment", "Identification Code", "Position", "Work Experience", "Rate"));
        foreach (var empl in sortedList)
            Console.WriteLine(empl);
    }

    static void ShowEmployees(List<Employee> employees)
    {
        var sortedList = employees.OrderBy(e => e.Name).ToList();
        Console.WriteLine(String.Format("{0,5}{1,5}{2,5}{3,5}{4,5}{5,5}{5,5}", "Type", "tName", "Date Of Employment", "Identification Code", "Position", "Work Experience", "Rate"));
        foreach (var empl in sortedList)
            Console.WriteLine(empl);
    }

    static void SaveEmployeesToDB()
    {
        using (StreamWriter st = new StreamWriter(dbPath, false, System.Text.Encoding.Default))
        {
            foreach (var empl in Employees)
            {
                st.WriteLine(empl);
            }
        }
    }

    static void FindByName()
    {
        Console.WriteLine("Enter Name: ");
        var name = Console.ReadLine();
        var employee = Employees.Find(e => e.Name == name);

        if (employee == null)
            Console.WriteLine("The employee doesn't exist.");
        else
        {
            SwithchToEmployee(employee);
        }
    }

    static void FindByPosition()
    {
        Console.WriteLine("Enter Position: ");
        var position = Console.ReadLine();
        var employees = Employees.FindAll(e => e.Position.Contains(position));

        if (employees == null)
            Console.WriteLine("The employee doesn't exist.");
        else
        {
            ShowEmployees(employees);
        }
    }

    static void SwithchToEmployee(Employee employee)
    {
        int option = 0;
        while (option != 5)
        {
            ShowMenu(EmployeeMenuOptions);
            option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    DeleteEmployee(employee);
                    break;
                case 2:
                    Employees.Remove(employee);
                    employee.ChangePosition();
                    Employees.Add(employee);
                    SaveEmployeesToDB();
                    break;
                case 3:
                    Console.WriteLine($"Accrued salary: {employee.CountSalary()}");
                    break;
                case 4:
                    Employees.Remove(employee);
                    employee.ChangeWorkedTime();
                    Employees.Add(employee);
                    SaveEmployeesToDB();
                    break;
                case 5:
                    return;
                default:
                    Console.WriteLine("Please enter an integer value between 1 and 5");
                    break;
            }

        }

    }

    static void Main(string[] args)
    {
        ReadEmployeesFromDB(dbPath);

        int option = 0;
        while (option != 5)
        {
            try
            {
                ShowMenu(MainMenuOptions);
                option = Convert.ToInt32(Console.ReadLine());
            }
            catch (System.FormatException)
            {
                Console.WriteLine("Please enter an integer value between 1 and 5");
                continue;
            }
            catch (Exception)
            {
                Console.WriteLine("An unexpected error happened. Please try again");
                continue;
            }

            switch (option)
            {
                case 1:
                    AddEmployee();
                    break;
                case 2:
                    ShowAllEmployees();
                    break;
                case 3:
                    FindByName();
                    break;
                case 4:
                    FindByPosition();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Please enter an integer value between 1 and 5");
                    break;
            }

        }

    }
}

