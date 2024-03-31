using Boss.az.Models.UserInfo;
using Boss.az.Models.Work;

namespace Boss.az.Models.Human;
public class Employer : Person
{
    string company { get; set; }
    public string Company
    {
        get { return company; }
        set
        {
            if (value.Length < 2)
                throw new("Company name is very short");
            company = value;
        }
    }
    public List<Vacancy> vacancies { get; set; } = new();
    public List<Worker> Workers { get; set; } = new();
    public Employer()
    {

    }
    public Employer(string name, string surname, string city, string phone, int age, string username, string password, string mail)
        : base(name, surname, city, phone, age, username, password, mail)
    {

    }
    public void EmployerMenu()
    {
        string[] arr = new string[8] { "Edit Profile", "All Workers", "All Vacancies", "My Vacancies", "My Workers", "Notifications", "Add Vacancy", "Exit" };
        int select = 0;
        while (true)
        {
            Console.Clear();
            Main.Show(arr, select);
            int f = Main.Control(arr.Length, ref select);
            if (f == -1)
                return;

            else if (f == 1)
            {
                if (select == 0)
                {
                    EditUser();
                    Main.AddLog($"{Username} updated his profile -> ");
                }

                else if (select == 1)
                {
                    int option = Main.PrintList(Main.workers, "Workers", false);
                    if (option != -1)
                    {
                        if (Admin.Selectoption("Do you want to invite this Worker") == 1)
                        {
                            Notification n = new();
                            n.Sender = Username;
                            n.Receiver = Main.workers[option].Username;
                            n.Content = $@"

    {n.Receiver}
    {Main.workers[option].City} ZIP: AZ-057
    
    Dear {Main.workers[option].Username},
    We are pleased to extend this invitation to you to join our {Company}. 
    
    Your skills and experience align perfectly with what we are looking for, and we believe that you would be a valuable addition to our team.
    {Company} is dedicated to connecting talented individuals like yourself with meaningful job opportunities. 
    As a member of our program, you will have access to a wide range of job listings and resources designed to help you succeed in your career.
    
    Here are some key benefits of joining {Company}:
    
    Job Opportunities: Gain access to a diverse range of job listings tailored to your skills and preferences.
    Networking: Connect with industry professionals and expand your professional network.
    Career Development: Access resources and training materials to enhance your skills and advance your career.
    Flexibility: Enjoy the flexibility to choose the jobs that best fit your schedule and career goals.
    
    We are excited about the opportunity to work with you and help you achieve your career aspirations. 
    If you have any questions or need further information, please don't hesitate to contact us.
    We look forward to welcoming you to {Company}!
    
    Sincerely,
    {Username}                                  {DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year} 
    
    {Mail}
    {Phone}
    {City}";
                            n.HTML = $@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Job Invitation Email</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f9f9f9;
            color: #333;
        }}
        h1 {{
            color: #0074d9;
            margin-bottom: 20px;
        }}
        p {{
            font-size: 16px;
            line-height: 1.6;
            margin-bottom: 15px;
        }}
        ul {{
            list-style-type: disc;
            margin-left: 20px;
        }}
        footer {{
            font-size: 14px;
            color: #888;
            margin-top: 30px;
        }}
    </style>
</head>
<body>
    <h1>Dear {Main.workers[option].Username},</h1>
    <p>We are pleased to extend this invitation to you to join our <strong> {Company}</strong>.</p>
    <p>Your skills and experience align perfectly with what we are looking for, and we believe that you would be a valuable addition to our team. <strong>{Company}</strong> is dedicated to connecting talented individuals like yourself with meaningful job opportunities.</p>
    <p>As a member of our program, you will have access to a wide range of job listings and resources designed to help you succeed in your career.</p>
    <p>Here are some key benefits of joining <strong>{Company}</strong>:</p>
    <ul>
        <li>Job Opportunities: Gain access to a diverse range of job listings tailored to your skills and preferences.</li>
        <li>Networking: Connect with industry professionals and expand your professional network.</li>
        <li>Career Development: Access resources and training materials to enhance your skills and advance your career.</li>
        <li>Flexibility: Enjoy the flexibility to choose the jobs that best fit your schedule and career goals.</li>
    </ul>
    <p>We are excited about the opportunity to work with you and help you achieve your career aspirations. If you have any questions or need further information, please don't hesitate to contact us. We look forward to welcoming you to <strong>{Company}</strong>!</p>
    <footer>
        Sincerely,<br>
                   {Username}<br>
        {DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year}<br>
        Contact Information:<br>
        Email: {Mail}<br>
        Phone: {Phone}<br>
        City: {City}
    </footer>
</body>
</html>
";
                            Admin.AdminNotifications.Add(n);
                            Main.AddLog($"{Username} invited {n.Receiver} -> ");
                        }
                    }
                }

                else if (select == 2)
                {
                    List<Vacancy> temp = new();
                    foreach (var a in Main.Vacancies)
                        if (a.Visiblity == 1)
                            temp.Add(a);
                    _=Main.PrintList(temp, "Vacancies");
                }

                else if (select == 3)
                {
                    List<Vacancy> temp = new();
                    foreach (var a in vacancies)
                        if (a.Visiblity == 1)
                            temp.Add(a);
                    int k = Main.PrintList(temp, "My Vacancies", false);
                    if (k != -1)
                        if (Admin.Selectoption("Do you want to delete this Vacancy?") == 1)
                            temp[k].Visiblity = -1;
                }

                else if (select == 4)
                {
                    int index = Main.PrintList<Worker>(Workers, "My Workers");
                    if (index != -1)
                        if (Admin.Selectoption("Do you want to fire this employee?") == 1)
                        {
                            Main.AddLog($"{Username} fired {Workers[index].Username} -> ");
                            Workers.Remove(Workers[index]);
                        }
                }

                else if (select == 5)
                {
                    List<Notification> temp = new();
                    foreach (var not in Notifications)
                        if (not.Visiblity == 1)
                            temp.Add(not);
                    int option = Main.PrintList(temp, "Notifications");
                    if (option != -1)
                    {
                        while (true)
                        {
                            Console.Clear();
                            int a = Admin.Selectoption($"{temp[option].Sender} applied your job. Do you want to accept Worker");
                            if (a == 1)
                            {
                                Console.Clear();
                                Console.WriteLine(Main.font);
                                Worker worker = new();
                                foreach (var w in Main.workers)
                                    if (w.Username == temp[option].Sender)
                                    {
                                        worker = w;
                                        break;
                                    }
                                Vacancy v = new();
                                foreach (var i in Admin.AdminVacancies)
                                    if (i.Id == temp[option].WorkId)
                                    {
                                        v = i;
                                        break;
                                    }
                                Notification n = new();
                                n.Sender = Username;
                                n.Receiver = worker.Username;
                                n.HTML = $@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Job Acceptance Email</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f9f9f9;
            color: #333;
        }}
        h1 {{
            color: #0074d9;
        }}
        p {{
            font-size: 16px;
            line-height: 1.6;
        }}
        ul {{
            list-style-type: disc;
            margin-left: 20px;
        }}
        strong {{
            color: #2ecc40;
        }}
        footer {{
            font-size: 14px;
            color: #888;
        }}
    </style>
</head>
<body>
    <h1>Hello {worker.Username},</h1>
    <p>We are thrilled to inform you that your application to join our team at <strong>{Company}</strong> has been accepted!</p>
    <p>Your skills and experience are highly valued, and we believe that you will make a significant contribution to our organization.</p>
    <p>As a member of our team, you will have the opportunity to work on exciting projects, collaborate with talented individuals, and grow both personally and professionally.</p>
    <p>Here are some key details regarding your acceptance:</p>
    <ul>
        <li><strong>Position:</strong> {v.Position}</li>
        <li><strong>Start Date:</strong> {DateTime.Now.AddDays(7).Day}.{DateTime.Now.AddDays(7).Month}.{DateTime.Now.AddDays(7).Year}</li>
        <li><strong>Salary:</strong> {v.Salary}</li>
    </ul>
    <p>We are confident that you will excel in your new role and become an integral part of our team.</p>
    <p>If you have any questions or need further information, please feel free to contact us at any time.</p>
    <p>We look forward to welcoming you aboard and embarking on this exciting journey together!</p>
    <footer>
        Sincerely,<br>
        {Username}<br>
        {Company}<br>
        {DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year}<br>
        Contact Information:<br>
        Email: {Mail}<br>
        Phone: {Phone}<br>
        City: {City}
    </footer>
</body>
</html>

";
                                n.Content = $@"

    {n.Receiver}
    {Main.workers[option].City} ZIP: AZ-057
    
    Dear {worker.Username}, 
    
    We are thrilled to inform you that your application to join our team at {Company} has been accepted!
    
    Your skills and experience are highly valued, and we believe that you will make a significant contribution to our organization. 
    
    As a member of our team, you will have the opportunity to work on exciting projects, collaborate with talented individuals
    and grow both personally and professionally.
    
    Here are some key details regarding your acceptance:
    
    Position: {v.Position}
    Start Date: {DateTime.Now.AddDays(7).Day}.{DateTime.Now.AddDays(7).Month}.{DateTime.Now.AddDays(7).Year}
    Salary: {v.Salary}
    
    We are confident that you will excel in your new role and become an integral part of our team. 
    
    If you have any questions or need further information, please feel free to contact us at any time.
    
    We look forward to welcoming you aboard and embarking on this exciting journey together!
    
    Sincerely,
    {Username}
    {Company}
    {DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year} 
    
    {Mail}
    {Phone}
    {City}";
                                Admin.AdminNotifications.Add(n);
                                Main.AddLog($"{Username} accepted {n.Sender} application -> ");
                                Workers.Add(worker);
                                try
                                {
                                    Main.Vacancies.Remove(v);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                    _ = Console.ReadKey(true);
                                }
                                finally
                                {
                                    Console.ResetColor();
                                }
                                break;
                            }
                        }
                        temp[option].Visiblity = 2;
                    }
                }

                else if (select == 6)
                    AddVacancy();

                else if (select == 7) return;
            }
        }
    }
    public void AddVacancy()
    {
        Vacancy v = new();
        string requirements = v.Requirements;
        string company = v.CompanyName;
        string position = v.Position;
        double salary = v.Salary;
        int sl = 0;
        bool @break = false;
        string[] arr1 = new string[5] { $"Company:       {company}", $"Position:     {position}",$"Requirements: {requirements}"
            ,$"Salary:       {(salary!=0?salary:"")}","Save" };
        while (true)
        {
            Console.Clear();
            Main.Show(arr1, sl);
            int a = Main.Control(arr1.Length, ref sl);
            if (a == -1)
                return;

            else if (a == 1)
            {
                Console.SetCursorPosition(54, sl + 9);
                Console.Write(new string(' ', arr1[sl].Length - arr1[sl].IndexOf(':') - 1));
                Console.SetCursorPosition(54, sl + 9);
                string newValue = "";
                if (sl == 4)
                {
                    if (v.Salary != default && v.Requirements != default && v.CompanyName != default && v.Position != default)
                    {
                        @break = true;
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nBe sure for filling all lines");
                        Console.ForegroundColor = ConsoleColor.White;
                        _ = Console.ReadKey(true);
                    }
                }
                else
                    newValue = Console.ReadLine() ?? "";
                try
                {
                    switch (sl)
                    {
                        case 0:
                            v.CompanyName = newValue;
                            arr1[sl] = $"{arr1[sl].Substring(0, arr1[sl].IndexOf(':') + 6)} {newValue}";
                            break;
                        case 1:
                            v.Position = newValue;
                            arr1[sl] = $"{arr1[sl].Substring(0, arr1[sl].IndexOf(':') + 5)} {newValue}";
                            break;
                        case 2:
                            v.Requirements = newValue;
                            arr1[sl] = $"{arr1[sl].Substring(0, arr1[sl].IndexOf(':') + 1)} {newValue}";
                            break;
                        case 3:
                            v.Salary = Convert.ToDouble(newValue);
                            arr1[sl] = $"{arr1[sl].Substring(0, arr1[sl].IndexOf(':') + 7)} {newValue}";
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.SetCursorPosition(0, arr1.Length + 11);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: {e.Message}");
                    Console.ForegroundColor = ConsoleColor.White;
                    _ = Console.ReadKey(true);
                }
            }
        }
        if (@break)
        {
            v.Username = Username;
            v.City = City;
            v.Phone = Phone;
            v.Mail = Mail;
            vacancies.Add(v);
            Admin.AdminVacancies.Add(v);
            Main.Vacancies.Add(v);
        }
    }
    public override string ToString()
    {
        return $"\t\t\t\t\tId: {Id}\n\t\t\t\t\tName: {Name}\n\t\t\t\t\tSurname: {Surname}\n\t\t\t\t\tUsername: {Username}\n\t\t\t\t\tCity: {City}\n\t\t\t\t\tPhone: +994 {Phone}\n\t\t\t\t\tAge: {Age}\n\t\t\t\t\tMail: {Mail}\n\n\t\t\t\t\t";
    }
}
