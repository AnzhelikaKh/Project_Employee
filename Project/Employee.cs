using System;

public abstract class Employee
{
    private string name;
    private DateTime dateOfEmployment;
    private string position;
    private string identificationCode;
    private int workExperience; // amount of months
    private double rate;

    protected Employee(string name, DateTime dateOfEmployment, string position, string identificationCode, int workExperience)
    {
        this.name = name;
        this.dateOfEmployment = dateOfEmployment;
        this.position = position;
        this.identificationCode = identificationCode;
        this.workExperience = workExperience;
    }

    public string Name { get { return name; } set { this.name = value; } }
    public DateTime DateOfEmployment { get { return dateOfEmployment; } set { this.dateOfEmployment = value; } }
    public string Position { get { return position; } set { this.position = value; } }
    public string IdentificationCode { get { return identificationCode; } set { this.identificationCode = value; } }
    public int WorkExperience { get { return workExperience; } set { this.workExperience = value; } }
    public double Rate { get { return rate; } set { this.rate = value; } }

    public abstract double CountSalary();
    public abstract void ChangeWorkedTime();

    public void ChangePosition()
    {
        Console.WriteLine("Please enter new position: ");
        var position = Console.ReadLine();
        this.position = position;
        Console.WriteLine("Position was changes successfully.");
    }

    public override string ToString()
    {
        return string.Format(String.Format("{0,5}{1,5}{2,5}{3,5}{4,5}{5,5}{5,5}", this.GetType(), name, dateOfEmployment, position, identificationCode, workExperience, rate));
    }
}

