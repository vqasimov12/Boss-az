using Boss.az.Models.UserInfo;
using Boss.az.Models.Work;

namespace Boss.az.Models.Human;
public class Worker : Person
{
    public List<Vacancy> AppliedWorks { get; set; } = new();
    public Cv PersonalCv { get; set; } = new();
    public Worker()
    {
    }
    public Worker(string name, string surname, string city, string phone, int age, string username, string password, string mail)
        : base(name, surname, city, phone, age, username, password, mail)
    {

    }
    public void WorkerMenu()
    {
        string[] arr = new string[5] { "Edit Profile", "Edit CV", "Notifications", "Apply", "Exit" };
        int select = 0;
        while (true)
        {
            Console.Clear();
            Main.Show(arr, select);
            int a = Main.Control(arr.Length, ref select);

            if (a == -1) break;

            else if (a == 1)
            {
                if (select == 0)
                {
                    EditUser();
                    Main.AddLog($"{Username} edited Profile -> ");
                }

                else if (select == 1)
                {
                    EditCV();
                    Main.AddLog($"{Username} edited Cv -> ");
                }

                else if (select == 2)
                {
                    List<Notification> temp = new();
                    foreach (var not in Notifications)
                        if (not.Visiblity == 1 || not.Visiblity == 2)
                            temp.Add(not);
                    int index = Main.PrintList(temp, "Notification");
                    if (index != -1)
                    {
                        if (temp[index].Visiblity == 2)
                        {
                            if (Admin.Selectoption("Do you want to accept invitation") == 1)
                            {
                                foreach (var e in Main.employers)
                                    if (e.Username == temp[index].Sender)
                                    {
                                        e.Workers.Add(this);
                                        Main.AddLog($"{Username} accept invitation of {e.Username} -> ");

                                    }
                            }
                        }

                    }

                }

                else if (select == 3)
                {
                    int index = Main.PrintList<Vacancy>(Main.Vacancies, "Vacancies");
                    if (index != -1)
                    {
                        int option = Admin.Selectoption("Do you want to apply");
                        if (option == 1)
                        {
                            foreach (var vacancy in Main.Vacancies)
                                if (vacancy.Id == Main.Vacancies[index].Id)
                                {
                                    Notification n = new();
                                    n.Content = $@"
    {Username}
    {City} ZIP Code: AZ-057
    {Mail}
    {Phone}
    {DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year}
    
    
    Dear {vacancy.Username},
    
    I am writing to express my interest in the {Main.Vacancies[index].Position} advertised by {Main.Vacancies[index].CompanyName}. 
    With my experience, and qualifications, I am confident in my ability to contribute effectively to your team.
    
    I am particularly drawn to  specific aspects of the job  and I am excited about the opportunity to [mention what  you    hope to achieve or contribute]. Through my previous experience in [mention relevant experience or projects], I have developed a strong  foundation in    [mention relevant skills or competencies] that align well with the requirements of the position.
    
    I am eager to bring my skills or qualities to {Main.Vacancies[index].CompanyName} and to further develop my abilities.
    
    Enclosed is my CV, which provides additional details about my background and accomplishments. I would welcome the opportunity to discuss how my     skills and experiences align with the needs of your team. Please feel free to contact me at your convenience to schedule a meeting or interview.
    
    Thank you for considering my application. I look forward to the possibility of contributing to {Main.Vacancies[index].CompanyName}.
    
    Sincerely,
    
    {Username}


Please find below my CV:


{PersonalCv}";
                                    n.Sender = Username;
                                    n.HTML = $@"
               <!DOCTYPE html>
                <html lang=""en"">
                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <title>Job Application Email</title>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            background-color: #f9f9f9;
                            color: #333;
                            margin: 0;
                            padding: 0;
                        }}
                        .container {{
                            max-width: 600px;
                            margin: 0 auto;
                            padding: 20px;
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
                        strong {{
                            color: #2ecc40;
                        }}
                        footer {{
                            font-size: 14px;
                            color: #888;
                            margin-top: 30px;
                        }}
                    </style>
                </head>
                <body>
                    <div class=""container"">
                        <h1>Hello {vacancy.Username},</h1>
                        <p>I am writing to express my interest in the <strong>{vacancy.Position}</strong> advertised by <strong>{vacancy.CompanyName}</strong>. With my experience and qualifications, I am confident in my ability to contribute effectively to your team.</p>
                        <p>I am particularly drawn to specific aspects of the job, and I am excited about the opportunity to [mention what you hope to achieve or contribute]. Through my previous experience in [mention relevant experience or projects], I have developed a strong foundation in [mention relevant skills or competencies] that align well with the requirements of the position.</p>
                        <p>I am eager to bring my skills or qualities to <strong>{vacancy.CompanyName}</strong> and to further develop my abilities.</p>
                        <p>Enclosed is my CV, which provides additional details about my background and accomplishments. I would welcome the opportunity to discuss how my skills and experiences align with the needs of your team. Please feel free to contact me at your convenience to schedule a meeting or interview.</p>
                        <p>Thank you for considering my application. I look forward to the possibility of contributing to <strong>{vacancy.CompanyName}</strong>.</p>
                        <footer>
                            Sincerely,<br>
                            {Username}<br>
                            {Mail}<br>
                            {Phone}<br><br>
                   <p> You can find my CV below:</p><br>
                  <p>{PersonalCv}</p>
                        </footer>
                    </div>
                </body>
                </html>";
                                    n.Receiver = vacancy.Username;
                                    n.WorkId = vacancy.Id;
                                    Admin.AdminNotifications.Add(n);
                                    foreach (var u in Main.employers)
                                        if (u.Username == vacancy.Username)
                                        {
                                            u.AddNotification(n);
                                            Main.AddLog($"{Username} applied Id:{vacancy.Id} work -> ");
                                            break;
                                        }
                                    break;
                                }
                        }
                    }
                }

                else
                    break;

            }
        }
    }
    public override string ToString()
    {
        return $"\t\t\t\t\tId: {Id}\n\t\t\t\t\tName: {Name}\n\t\t\t\t\tSurname: {Surname}\n\t\t\t\t\tUsername: {Username}\n\t\t\t\t\tCity: {City}\n\t\t\t\t\tPhone: +994 {Phone}\n\t\t\t\t\tAge: {Age}\n\t\t\t\t\tMail: {Mail}\n\n\t\t\t\t\t";
    }
    public void EditCV()
    {
        string school = PersonalCv.School;
        string speciality = PersonalCv.Speciality;
        string university = PersonalCv.University;
        int point = PersonalCv.Point;
        int sl = 0;
        bool @break = false;
        string[] arr1 = new string[6] { $"School:       {school}", $"Point:        {(point != 0 ? point.ToString() : " ")}",
            $"University:   {university}", $"Speciality:   {speciality}",$"Honor diplom: {(PersonalCv.HonorDiplom?"Yes":"No")}","Save" };
        while (true)
        {
            Console.Clear();
            Main.Show(arr1, sl);
            int k = Main.Control(arr1.Length, ref sl);

            if (k == -1)
            {
                if (PersonalCv.School != default && PersonalCv.Point != default && PersonalCv.University != default && PersonalCv.Speciality != default)
                    return;
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please be sure to enter all fields");
                    _ = Console.ReadKey(true);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            else if (k == 1)
            {
                string newValue = "";
                if (sl != 4 && sl != 5)
                {
                    Console.SetCursorPosition(54, sl + 9);
                    Console.Write(new string(' ', arr1[sl].Length - arr1[sl].IndexOf(':') - 1));
                    Console.SetCursorPosition(54, sl + 9);
                    newValue = Console.ReadLine() ?? "";
                }
                else if (sl == 4)
                {
                    while (true)
                    {
                        Console.SetCursorPosition(0, arr1.Length + 7);
                        Console.Write(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(40, arr1.Length + 7);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("Honor diplom: < ");
                        Console.Write((PersonalCv.HonorDiplom ? "Yes" : "No") + " >");
                        Console.ResetColor();
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.RightArrow)
                            PersonalCv.HonorDiplom = !PersonalCv.HonorDiplom;
                        else if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Escape)
                        {
                            arr1[sl] = $"{"Honor diplom:",-11} {(PersonalCv.HonorDiplom ? "Yes" : "No")}";
                            break;
                        }
                    }
                }
                try
                {
                    switch (sl)
                    {
                        case 0:
                            PersonalCv.School = newValue;
                            arr1[sl] = $"{arr1[sl].Substring(0, arr1[sl].IndexOf(':') + 7)} {newValue}";
                            break;
                        case 1:
                            PersonalCv.Point = Convert.ToInt32(newValue);
                            arr1[sl] = $"{arr1[sl].Substring(0, arr1[sl].IndexOf(':') + 8)} {newValue}";
                            break;
                        case 2:
                            PersonalCv.University = newValue;
                            arr1[sl] = $"{"University:",-13} {newValue}";
                            break;
                        case 3:
                            PersonalCv.Speciality = newValue;
                            arr1[sl] = $"{"Speciality:",-13} {newValue}";
                            break;
                        case 5:
                            if (PersonalCv.School != default && PersonalCv.Point != default && PersonalCv.University != default && PersonalCv.Speciality != default)
                                @break = true;
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Please be sure to enter all fields");
                                _ = Console.ReadKey(true);
                                Console.ForegroundColor = ConsoleColor.White;
                            }
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

            if (@break) break;
        }
        string[] arr = new string[4] { "Add Company", "Add Language ", "Add Skill", "Save" };
        sl = 0;
        while (true)
        {
            Console.Clear();
            Main.Show(arr, sl);
            int k = Main.Control(arr.Length, ref sl);

            if (k == 1)
            {
                if (sl == 0)
                    PersonalCv.AddCompany();
                else if (sl == 1)
                    PersonalCv.AddLanguage();
                else if (sl == 2)
                    PersonalCv.AddSkill();
                else break;
            }
            else if (k == -1)
                break;
        }

    }
}
