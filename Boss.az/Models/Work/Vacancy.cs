namespace Boss.az.Models.Work;
public class Vacancy
{
    string company { get; set; }
    string position { get; set; }
    double salary { get; set; }
    string requirements { get; set; }
    public DateTime AviableDate { get; set; }
    public DateTime FinishDate { get; set; }
    public int Visiblity { get; set; }
    public string CompanyName
    {
        get { return company!; }
        set
        {
            if (value.Length < 2)
                throw new("Company name can not be less than 2 character");
            company = value;
        }


    }
    public string Position
    {
        get
        {
            return position;
        }
        set
        {
            if (value.Length < 2)
                throw new("Enter correct position");
            position = value;
        }
    }
    public double Salary
    {
        get { return salary; }
        set
        {
            if (value < 300)
                throw new("Salary can not be less than min Salary(300)");
            salary = value;
        }
    }
    public string Requirements
    {
        get { return requirements; }
        set
        {
            if (value == null)
                throw new("requirements can not be null");
            requirements = value;
        }

    }
    public string Username { get; set; }
    public string Id { get; set; }
    public string City { get; set; }
    public string Mail { get; set; }
    public string Phone { get; set; }
    public Vacancy()
    {
        Id = Guid.NewGuid().ToString("N").Substring(0, 8);
        AviableDate = DateTime.Now;
        FinishDate = AviableDate.AddMonths(1);
        Visiblity = 0;
    }
    public Vacancy(string position, string companyName, double salary, string requirements) : this()
    {
        CompanyName = companyName;
        Position = position;
        Salary = salary;
        Requirements = requirements;
    }
    public override string ToString()
    {
        return $@"
                        Company: {CompanyName}
Id: {Id}
Position: {Position}

Requirements: {requirements}
Salary:       {Salary}

Aviable Date: {AviableDate.Day} {AviableDate.Month} {AviableDate.Year}
Finish  Date: {FinishDate.Day} {FinishDate.Month} {FinishDate.Year}

Employer: {Username}
City :    {City}
Email:    {Mail}
Phone:    {Phone}


";
    }
}
