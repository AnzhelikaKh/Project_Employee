using System;

public class HourlyEmployee : Employee
{
    private int workingHours;
    public int WorkingHours { get { return workingHours; } set { this.workingHours = value; } }
    public HourlyEmployee(string name, DateTime dateOfEmployment, string position, string identificationCode, int workExperience)
        : base(name, dateOfEmployment, position, identificationCode, workExperience)
    {

    }

    public override double CountSalary()
    {
        return workingHours * Rate;
    }

    public override void ChangeWorkedTime()
    {
        Console.WriteLine("Please enter new amount of working hours: ");
        var hours = Convert.ToInt16(Console.ReadLine());
        this.workingHours = hours;
        Console.WriteLine("Amount of working hours was changes successfully.");

    }
}

