namespace Boss.az.Models.UserInfo;
public class Company
{
    public string Name
    {
        get { return name; }
        set
        {
            if (value.Length < 3)
                throw new("Company name should contain min 3 character");
            name = value;
        }
    }
    public string Time
    {
        get
        {
            return time;
        }
        set
        {
            double a = 0;
            if (double.TryParse(value, out a))
            {

                if (a < 0)
                    throw new Exception("Time period should bigger than 0");
            }
            else if(value==null)
                    throw new Exception("Time is not in correct format");
            time = value;
        }
    }
    string time { get; set; }
    string name { get; set; }
    public override string ToString()
    {
        return $"Company: {Name} -> {Time}";
    }
}
