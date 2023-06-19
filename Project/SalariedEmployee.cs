using System;

public class SalariedEmployee : Employee
{
    private int workingDays;
    private int sickLeaveDays;
    public int WorkingDays { get { return workingDays; } set { this.workingDays = value; } }
    public int SickLeaveDays { get { return sickLeaveDays; } set { this.sickLeaveDays = value; } }

    public SalariedEmployee(string name, DateTime dateOfEmployment, string position, string identificationCode, int workExperience)
        : base(name, dateOfEmployment, position, identificationCode, workExperience)
    {
    }

    public override void ChangeWorkedTime()
    {
        Console.WriteLine("Please enter new amount of working days: ");
        var workingDays = Convert.ToInt16(Console.ReadLine());
        this.workingDays = workingDays;
        Console.WriteLine("Amount of working days was changes successfully.");

        Console.WriteLine("Please enter new amount of sick leaves: ");
        var sickLeaveDays = Convert.ToInt16(Console.ReadLine());
        this.SickLeaveDays = sickLeaveDays;
        Console.WriteLine("Amount of sick leave days was changes successfully.");
    }

    public override double CountSalary()
    {
        return WorkExperience * 10 + Rate;
    }
}

