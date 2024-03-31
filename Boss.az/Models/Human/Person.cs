using Boss.az.Models.UserInfo;
using System.Text.RegularExpressions;

namespace Boss.az.Models.Human;
public class Person
{
    public List<Notification> Notifications = new();
    string password { get; set; }
    string username { get; set; }
    int age { get; set; }
    string phone { get; set; }
    string mail { get; set; }
    string city { get; set; }
    string name { get; set; }
    string surname { get; set; }
    public string Id { get; set; }
    public string Name
    {
        get { return name; }
        set
        {
            string pattern = @"^[a-zA-Z]{3,15}$";
            if (!Regex.IsMatch(value, pattern))
                throw new("Name should only contain letters and 3-15 characters");
            name = value.Substring(0, 1).ToUpper() + value.Substring(1);
        }
    }
    public string Surname
    {
        get { return surname; }
        set
        {
            string pattern = @"^[a-zA-Z]{3,15}$";
            if (!Regex.IsMatch(value, pattern))
                throw new("Surname should only contain letters and 3-15 characters");
            surname = value.Substring(0, 1).ToUpper() + value.Substring(1);
        }
    }
    public string City
    {

        get { return city; }
        set
        {
            string pattern = @"^[a-zA-Z]{3,15}$";
            if (!Regex.IsMatch(value, pattern))
                throw new("City name should only contain letters and 3-15 characters");
            city = value.Substring(0, 1).ToUpper() + value.Substring(1);
        }
    }
    public string Mail
    {
        get { return mail; }
        set
        {
            if (value.Length < 14)
                throw new Exception("Email should contain min 14 character");
            if (!value.EndsWith("@gmail.com"))
                throw new Exception("Mail should end with \"@gmail.com\"");
            mail = value;
        }
    }
    public string Phone
    {
        get { return phone; }
        set
        {
            string pattern = @"(?:50|51|70|77|10|99|55|57) \d{3} \d{2} \d{2}";
            if (!Regex.IsMatch(value, pattern))
                throw new("Phone format or prefix is not correct");
            phone = value;
        }
    }
    public int Age
    {
        get { return age; }

        set
        {
            if (value >= 18 && value <= 70)
                age = value;
            else throw new("Age should between 18 and 70");
        }

    }
    public string Password
    {
        get { return password; }
        set
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$";
            if (!Regex.IsMatch(value, pattern))
                throw new("Password should have min 8 character at least 1 UpperCase 1 Lowercase and 1 digit ");
            password = value;
        }
    }
    public string Username
    {
        get { return username; }

        set
        {
            string pattern = @"^[a-zA-Z][a-zA-Z0-9_-]{2,19}$";
            if (!Regex.IsMatch(value, pattern))
                throw new(@"Username should between 3 and 16 character and 
can contain '_' '-' digits or letters and should start with letter ");
            username = value;
        }


    }
    public Person()
    {
        Id = Guid.NewGuid().ToString("N").Substring(0, 8);
    }
    public Person(string name, string surname, string city, string phone, int age, string username, string password, string mail) : this()
    {
        Name = name;
        Surname = surname;
        City = city;
        Phone = phone;
        Mail = mail;
        Username = username;
        Password = password;
        Age = age;
    }
    public void AddNotification(Notification n)
    {
        try
        {
            string mail = "";
            bool found = false;
            foreach (var a in Main.employers)
                if (a.username == n.Receiver)
                {
                    mail = a.Mail;
                    found = true;
                    a.Notifications.Add(n);
                    break;
                }
            if (!found)
            {
                foreach (var a in Main.workers)
                    if (a.username == n.Receiver)
                    {
                        a.Notifications.Add(n);
                        mail = a.Mail;
                        break;
                    }
            }
            Main.SendMail<string>(mail, n.HTML!,true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            _ = Console.ReadKey(true);
        }
    }
    public void EditUser()
    {
        string user = Username;
        string password = Password;
        string phone = Phone;
        string name = Name;
        string surname = Surname;
        string city = City;
        string mail = Mail;
        int age = Age;
        int sl = 0;
        string[] arr1 = new string[8] { $"Name:     {name}", $"Surname:  {surname}",$"City:     {city}", $"Phone:" +
        $"    {phone}", $"Email:    {mail}",$"Age:      {age}", $"Username: {username}",$"Password: {password}" };
        while (true)
        {
            Console.Clear();
            Main.Show(arr1, sl);
            int a = Main.Control(arr1.Length, ref sl);
            if (a == -1)
                return;

            else if (a == 1)
            {
                Console.SetCursorPosition(50, sl+9);
                Console.Write(new string(' ', arr1[sl].Length - arr1[sl].IndexOf(':') - 1));
                Console.SetCursorPosition(50, sl+9);
                string newValue = Console.ReadLine() ?? "";
                try
                {
                    switch (sl)
                    {
                        case 0:
                            Name = newValue;
                            arr1[sl] = $"{arr1[sl].Substring(0, arr1[sl].IndexOf(':') + 5)} {newValue}";
                            break;
                        case 1:
                            Surname = newValue;
                            arr1[sl] = $"{arr1[sl].Substring(0, arr1[sl].IndexOf(':') + 2)} {newValue}";
                            break;
                        case 2:
                            City = newValue;
                            arr1[sl] = $"{arr1[sl].Substring(0, arr1[sl].IndexOf(':') + 5)} {newValue}";
                            break;
                        case 3:
                            Phone = newValue;
                            arr1[sl] = $"{arr1[sl].Substring(0, arr1[sl].IndexOf(':') + 4)} {newValue}";
                            break;
                        case 4:
                            Mail = newValue;
                            arr1[sl] = $"{arr1[sl].Substring(0, arr1[sl].IndexOf(':') + 4)} {newValue}";
                            break;
                        case 5:
                            Age = Convert.ToInt32(newValue);
                            arr1[sl] = $"{arr1[sl].Substring(0, arr1[sl].IndexOf(':') + 6)} {newValue}";
                            break;
                        case 6:
                            Username = newValue;
                            arr1[sl] = $"{arr1[sl].Substring(0, arr1[sl].IndexOf(':') + 1)} {newValue}";
                            break;
                        case 7:
                            Password = newValue;
                            arr1[sl] = $"{arr1[sl].Substring(0, arr1[sl].IndexOf(':') + 1)} {newValue}";
                            break;
                    }

                }
                catch (Exception e)
                {
                    Console.SetCursorPosition(0, arr1.Length + 11);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: {e.Message}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

    }
}

