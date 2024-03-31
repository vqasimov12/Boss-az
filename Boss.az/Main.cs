using Boss.az.Models.Human;
using Boss.az.Models.Work;
using System.Net.Mail;
using System.Net;

namespace Boss.az;
public class Main
{
    //public Main()
    //{
    //    workers = new();
    //    employers = new();
    //    Vacancies = new();
    //    admin = new();
    //}

    public static long OTPCode { get; set; }
    public static List<Worker> workers { get; set; } = new();
    public static List<Employer> employers { get; set; } = new();
    public static List<Vacancy> Vacancies { get; set; } = new();
    public static Admin admin { get; set; } = new();
    public static string path { get; set; } = "MY LOGs.json";
    public static string font { get; set; } = @"
                              ____   ____   _____ _____            ______
                             |  _ \ / __ \ / ____/ ____|     /\   |___  /
                             | |_) | |  | | (___| (___      /  \     / / 
                             |  _ <| |  | |\___ \\___ \    / /\ \   / /  
                             | |_) | |__| |____) ____) |  / ____ \ / /__ 
                             |____/ \____/|_____|_____/  /_/    \_/_____|
                                                                         
                                             ";
    public static string DirectoryPath { get; set; }
    public static string Path
    {
        get { return path; }
        set
        {
            path = value;
        }
    }
    static bool Exists(string username)
    {
        foreach (var a in employers)
            if (a.Username == username)
                return true;
        foreach (var a in workers)
            if (a.Username == username)
                return true;
        return false;
    }
    public static void AddLog(string info)
    {
        using (StreamWriter writer = File.AppendText(DirectoryPath + path))
        {
            writer.WriteLine(info + DateTime.Now);
            writer.WriteLine();
        }
    }
    public static void LogThis(string logMessage)
    {

        if (!File.Exists(path))
            using (StreamWriter writer = new StreamWriter(DirectoryPath + path, true))
            {
                writer.WriteLine($"{logMessage}{DateTime.Now}");
                writer.WriteLine();
            }
        else
            AddLog(logMessage);
    }
    public void Start()
    {
        LogThis("\n\nProgram started  ->  ");
        int select = 0;
        string[] arr = new string[5] { "Workers", "Vacancies", "Sign In", "Sign Up", "Exit" };
        while (true)
        {
            Console.Clear();
            Show(arr, select);
            int a = Control(arr.Length, ref select);

            if (a == -1)
            {
                AddLog("Program closed -> ");
                break;
            }


            else if (a == 1)
            {
                if (select == 0)
                    PrintList(workers, "Workers", false);

                else if (select == 1)
                {
                    List<Vacancy> temp = new();
                    foreach (var v in Admin.AdminVacancies)
                        if (v.Visiblity == 1 && v.AviableDate < DateTime.Now)
                            temp.Add(v);
                    PrintList(temp, "Vacancies", false);
                }

                else if (select == 2)
                    SignIn();

                else if (select == 3)
                    SingUp();

                else if (select == 4)
                {
                    AddLog("Program closed -> ");
                    return;
                }
            }
        }
    }
    public static void Show(string[] arr, int select, bool color = true, string a = "")
    {
        Console.WriteLine(font);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write(a);
        Console.ForegroundColor = ConsoleColor.White;
        for (int i = 0; i < arr.Length; i++)
        {
            if (i == select)
            {
                Console.ForegroundColor = (color == true) ? ConsoleColor.Cyan : ConsoleColor.Green;
                Console.WriteLine("\t\t\t\t\t" + arr[i]);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
                Console.WriteLine("\t\t\t\t\t" + arr[i]);
        }
    }
    public static void SingUp()
    {
        int select = 0;
        string[] arr = { "Employer", "Worker" };
        bool asWorker = false;
        while (true)
        {
            Console.Clear();
            Show(arr, select, false);
            int b = Control(arr.Length, ref select);
            if (b == -1)
                return;
            else if (b == 1)
            {
                if (select == 1)
                    asWorker = true;
                break;
            }
        }

        Person a;
        if (asWorker)
            a = new Worker();
        else
            a = new Employer();

        while (true)
            try
            {
                Console.Clear();
                Console.WriteLine(font);
                Console.Write("Name: ");
                a.Name = Console.ReadLine()!;
                break;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Try again");
                _ = Console.ReadKey(true);
            }

        while (true)
            try
            {
                Console.Clear();
                Console.WriteLine(font);
                Console.WriteLine($"Name: {a.Name}");
                Console.Write("Surname: ");
                a.Surname = Console.ReadLine()!;
                break;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Try again");
                _ = Console.ReadKey(true);
            }

        while (true)
            try
            {
                Console.Clear();
                Console.WriteLine(font);
                Console.WriteLine($"Name: {a.Name}\nSurname: {a.Surname}");
                Console.Write("City: ");
                a.City = Console.ReadLine()!;
                break;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Try again");
                _ = Console.ReadKey(true);

            }

        while (true)
            try
            {
                Console.Clear();
                Console.WriteLine(font);
                Console.WriteLine($"Name: {a.Name}\nSurname: {a.Surname}\nCity: {a.City}");
                Console.Write("Phone (55 555 55 55): ");
                a.Phone = Console.ReadLine()!;
                break;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _ = Console.ReadKey(true);
            }

        while (true)
            try
            {
                Console.Clear();
                Console.WriteLine(font);
                Console.WriteLine($"Name: {a.Name}\nSurname: {a.Surname}\nCity: {a.City}\nPhone: {a.Phone}");
                Console.Write("Age: ");
                a.Age = Convert.ToInt32(Console.ReadLine());
                break;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _ = Console.ReadKey(true);
            }

        while (true)
            try
            {
                Console.Clear();
                Console.WriteLine(font);
                Console.WriteLine($"Name: {a.Name}\nSurname: {a.Surname}\nCity: {a.City}\nPhone: {a.Phone}\nAge: {a.Age}");
                Console.Write("Username: ");
                a.Username = Console.ReadLine()!;
                if (Exists(a.Username))
                    throw new("This Username has already been used change Username");
                break;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _ = Console.ReadKey(true);
            }

        while (true)
            try
            {
                Console.Clear();
                Console.WriteLine(font);
                Console.WriteLine($"Name: {a.Name}\nSurname: {a.Surname}\nCity: {a.City}\nPhone: {a.Phone}\nAge: {a.Age}\nUsername: {a.Username}");
                Console.Write("Password: ");
                a.Password = Console.ReadLine()!;
                break;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _ = Console.ReadKey(true);
            }

        while (true)
            try
            {
                Console.Clear();
                Console.WriteLine(font);
                Console.WriteLine($"Name: {a.Name}\nSurname: {a.Surname}\nCity: {a.City}\nPhone: {a.Phone}\nAge: {a.Age}\nUsername: {a.Username}\nPassword: {a.Password}");
                Console.Write("Mail: ");
                a.Mail = Console.ReadLine()!;
                break;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _ = Console.ReadKey(true);
            }
        Random rand = new Random();
        OTPCode = rand.Next(1000, 10000);
        string body = $@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>OTP Email</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f2f2f2;
            margin: 0;
            padding: 0;
        }}
        .container {{
            max-width: 600px;
            margin: 20px auto;
            padding: 20px;
            background-color: #fff;
            border-radius: 10px;
            box-shadow: 0px 0px 20px 0px rgba(0,0,0,0.1);
        }}
        .header {{
            background-color: #007bff;
            color: #fff;
            text-align: center;
            padding: 20px 0;
            border-radius: 10px 10px 0 0;
        }}
        h1 {{
            margin: 0;
            font-size: 24px;
        }}
        .content {{
            padding: 20px 0;
        }}
        p {{
            font-size: 16px;
            line-height: 1.6;
            margin-bottom: 15px;
            text-align: center;
        }}
        .otp {{
            font-size: 28px;
            font-weight: bold;
            color: #007bff;
            margin-bottom: 20px;
            text-align: center;
        }}
        .footer {{
            font-size: 14px;
            color: #888;
            text-align: center;
            padding-top: 20px;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <h1>OTP Code</h1>
        </div>
        <div class=""content"">
            <p>Your One-Time Password (OTP) is:</p>
            <p class=""otp"">{OTPCode}</p>
            <p>Please use this OTP to verify your identity.</p>
        </div>
        <div class=""footer"">
            This is an automated message. Please do not reply to this email.
        </div>
    </div>
</body>
</html>
";

        SendMail<string>(a.Mail, body, true);
        int k = 0;
        while (k < 3)
        {
            Console.Write(k == 0 ? "Enter your OTP Code that send to your mail: " : $"Try again, {3 - k} chance left: ");
            int code = Convert.ToInt32(Console.ReadLine());
            if (OTPCode == code)
            {
                if (asWorker)
                {
                    Worker? w = a as Worker;
                    w.EditCV();
                    if (w is not null)
                    {
                        workers.Add(w);
                        AddLog("New Worker sign up -> ");
                    }
                }
                else
                {
                    Employer? e = a as Employer;
                    while (true)
                        try
                        {
                            Console.Clear();
                            Console.WriteLine(font);
                            Console.WriteLine($"Name: {a.Name}\nSurname: {a.Surname}\nCity: {a.City}\nPhone: {a.Phone}\nAge: {a.Age}\nUsername: {a.Username}\nPassword: {a.Password}\nMail: {a.Mail}");
                            Console.Write("Company Name: ");
                            e!.Company = Console.ReadLine()!;
                            break;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            _ = Console.ReadKey(true);
                        }
                    if (e is not null)
                    {
                        employers.Add(e);
                        AddLog("New Employer sign up -> ");
                    }
                }
                break;
            }
            Console.WriteLine("Otp code is not correct");
            k++;
            if (k == 3)
            {
                Console.WriteLine("Your session has expired You are blocked!!");
                AddLog($"{a.Username}'s session expired -> ");
                return;
            }
        }
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Login Complated Successfully");
        Console.ForegroundColor = ConsoleColor.White;
        _ = Console.ReadKey(true);
    }
    public static void SignIn()
    {
        Console.Clear();
        Console.WriteLine(font);
        Console.Write("\t\t\t\tEnter Username: ");
        Console.ForegroundColor = ConsoleColor.Green;
        string username = Console.ReadLine()!;
        Console.ResetColor();
        Console.Write("\t\t\t\tEnter Password: ");
        Console.ForegroundColor = ConsoleColor.Green;
        string password = Console.ReadLine()!;
        Console.ResetColor();
        if (username == "Admin")

            if (password == "Admin123")
            {
                AddLog("Admin signed in -> ");
                admin.AdminMenu();
            }
            else
            {
                Console.WriteLine("Incorrect password");
                AddLog("Invalid password entered -> ");
                _ = Console.ReadKey(true);
            }

        else if (Exists(username))
        {
            bool found = false;
            foreach (var a in employers)
                if (a.Username == username && a.Password == password)
                {
                    found = true;
                    AddLog($"{a.Username} signed in -> ");
                    a.EmployerMenu();
                }
            if (!found)
                foreach (var a in workers)
                    if (a.Username == username && a.Password == password)
                    {
                        AddLog($"{a.Username} signed in -> ");
                        found = true;
                        a.WorkerMenu();
                    }
            if (!found)
            {
                Console.WriteLine("Incorrect Password");
                AddLog("Invalid password entered -> ");
                _ = Console.ReadKey(true);
            }
        }
        else
        {

            AddLog("Invalid Username entered -> ");
            Console.WriteLine("Invalid Username");
            _ = Console.ReadKey(true);
        }
    }
    public static void SendMail<T>(string _destinationMail, T content, bool HtmlBody = false)
    {
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("qasimov.vaqif512@gmail.com");
            mail.To.Add(_destinationMail);
            mail.Subject = "**** Boss.az ****";
            mail.Body = content?.ToString() ?? "No content provided.";
            if (HtmlBody)
                mail.IsBodyHtml = true;
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("qasimov.vaqif512@gmail.com", "mnnc lpwi nzua ocjg");
            smtp.EnableSsl = true;
            smtp.Send(mail);
            AddLog($"Mail sent to {_destinationMail} -> ");
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public static int PrintList<T>(List<T> list, string listType, bool color = true)
    {
        int i = 0;
        while (true)
        {
            Console.Clear();
            Console.WriteLine(font);
            if (list.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{listType} can not be found");
                Console.ForegroundColor = ConsoleColor.White;
                _ = Console.ReadKey(true);
                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\t\t\t\t\t↑");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(list[i]);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\t\t\t\t\t↓");
            Console.ForegroundColor = ConsoleColor.White;

            int a = Control(list.Count, ref i);

            if (a == 1)
                return i;

            else if (a == -1)
                return -1;
        }
    }
    public static int Control(int length, ref int select)
    {
        ConsoleKeyInfo key = Console.ReadKey(true);
        if (key.Key == ConsoleKey.DownArrow)
            select = select != length - 1 ? select + 1 : 0;
        else if (key.Key == ConsoleKey.UpArrow)
            select = select != 0 ? select - 1 : length - 1;
        else if (key.Key == ConsoleKey.Escape)
            return -1;
        else if (key.Key == ConsoleKey.Enter)
            return 1;
        return 0;
    }
}