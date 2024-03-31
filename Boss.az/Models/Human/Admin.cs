using Boss.az.Models.UserInfo;
using Boss.az.Models.Work;

namespace Boss.az.Models.Human;
public class Admin : Person
{
    public static List<Notification> AdminNotifications { get; set; } = new();
    public static List<Vacancy> AdminVacancies { get; set; } = new();
    public static List<Employer> RemovedEmployers { get; set; } = new();
    public static List<Worker> RemovedWorkers { get; set; } = new();
    public Admin() : base("Admin", "Admin", "Shamkir", "55 555 55 55", 20, "Admin", "Admin123", "Admin@gmail.com")
    {

    }
    public void AdminMenu()
    {
        string[] arr = new string[8] { "All Workers", "All Employers", "All Vacancies", "All Notifacations", "Removed Employers", "Removed Workers", "All logs", "Exit" };
        int select = 0;
        while (true)
        {
            Console.Clear();
            Main.Show(arr, select, false);
            int a = Main.Control(arr.Length, ref select);
            if (a == -1)
                return;

            else if (a == 1)
                if (select == 0)
                {
                    int option = Main.PrintList<Worker>(Main.workers, "Workers", false);
                    if (option != -1)
                    {
                        int m = Selectoption();
                        if (m == 1)
                        {
                            try
                            {
                                RemovedWorkers.Add(Main.workers[option]);
                                Main.AddLog($"Admin deleted Worker: {Main.employers[option].Username} -> ");
                                for (int i = 0; i < RemovedWorkers.Count; i++)
                                    if (Main.workers[i].Id == Main.workers[option].Id)
                                    {
                                        Main.workers.RemoveAt(i);
                                        break;
                                    }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.ReadKey(true);
                            }
                        }
                    }
                }

                else if (select == 1)
                {
                    int option = Main.PrintList(Main.employers, "Employes", false);
                    if (option != -1)
                    {
                        int m = Selectoption();
                        if (m == 1)
                        {
                            try
                            {
                                RemovedEmployers.Add(Main.employers[option]);
                                Main.AddLog($"Admin deleted Employer: {Main.employers[option].Username} -> ");
                                Main.employers.RemoveAt(option);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.ReadKey(true);
                            }
                        }

                    }
                }

                else if (select == 2)
                    AllVacancies();

                else if (select == 3)
                    AllNotifications();

                else if (select == 6)
                {
                    Console.Clear();
                    string jsonContent = File.ReadAllText(Main.DirectoryPath+Main.path);
                    Console.WriteLine(jsonContent);
                    _ = Console.ReadKey(true);
                }

                else if (select == 5)
                {
                    int k = Main.PrintList(RemovedWorkers, "Removed Workers");
                    if (k != -1)
                    {
                        if (Selectoption("Do you want to restore this worker? ") == 1)
                        {
                            try
                            {
                                Main.workers.Add(RemovedWorkers[k]);
                                Main.AddLog($"Admin restored Worker: {RemovedWorkers[k].Username} -> ");
                                for (int i = 0; i < RemovedWorkers.Count; i++)
                                    if (RemovedWorkers[i].Id == RemovedWorkers[k].Id)
                                    {
                                        RemovedWorkers.RemoveAt(i);
                                        break;
                                    }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.ReadKey(true);
                            }
                        }
                    }
                }

                else if (select == 4)
                {
                    int k = Main.PrintList(RemovedEmployers, "Removed Employers");
                    if (k != -1)
                    {
                        if (Selectoption("Do you want to restore this employer? ") == 1)
                        {
                            try
                            {
                                Main.employers.Add(RemovedEmployers[k]);
                                Main.AddLog($"Admin restored employer: {RemovedEmployers[k].Username} -> ");
                                for (int i = 0; i < RemovedEmployers.Count; i++)
                                    if (RemovedEmployers[i].Id == RemovedEmployers[k].Id)
                                    {
                                        RemovedEmployers.RemoveAt(i);
                                        break;
                                    }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.ReadKey(true);
                            }
                        }
                    }
                }

                else
                    return;
        }
    }
    public static int Selectoption(string content = "Do you want to delete?")
    {
        string[] arr1 = new string[3] { "Yes", "No", "Exit" };
        int sl = 0;
        while (true)
        {
            Console.Clear();
            Main.Show(arr1, sl, true, $"\t\t\t\t{content}\n\n");
            int a = Main.Control(arr1.Length, ref sl);
            if (a == -1)
                return -1;

            else if (a == 1)
                if (sl == 0)
                    return 1;
                else if (sl == 1)
                    return 2;
                else
                    return -1;
        }
    }
    public static void AllNotifications()
    {
        string[] arr = new string[3] { "Checked", "Unchecked", "Deleted" };
        int sl = 0;
        while (true)
        {
            Console.Clear();
            Main.Show(arr, sl);
            int k = Main.Control(arr.Length, ref sl);
            if (k == -1)
                return;

            else if (k == 1)
            {
                List<Notification> temp = new();
                if (sl == 0)
                {
                    foreach (var v in AdminNotifications)
                        if (v.Visiblity == 1)
                            temp.Add(v);
                    int option = Main.PrintList<Notification>(temp, "Checked Notifications");
                    if (option != -1)
                        if (Selectoption() == 1)
                        {
                            temp[option].Visiblity = -1;
                            Main.AddLog($"Admin deleted notification Id: {temp[option].Id} -> ");

                        }
                }

                else if (sl == 1)
                {
                    foreach (var a in AdminNotifications)
                        if (a.Visiblity == 0 || a.Visiblity == 2)
                            temp.Add(a);
                    int option = Main.PrintList(temp, "Unchecked Notifications", false);
                    if (option != -1)
                    {
                        int a = 0;
                        string[] str = new string[2] { "Allow", "Deny" };
                        while (true)
                        {
                            Console.Clear();
                            Main.Show(str, a);
                            int control = Main.Control(str.Length, ref a);
                            if (control == -1)
                                break;

                            else if (control == 1)
                            {
                                if (a == 0)
                                {
                                    Main.AddLog($"Admin allowed notification Id: {temp[option].Id} -> ");

                                    //if (temp[option].Visiblity == 0)
                                        temp[option].Visiblity = 1;
                                    foreach (var user in Main.workers)
                                        if (user.Username == temp[option].Receiver)
                                            user.AddNotification(temp[option]);
                                }
                                else if (a == 1)
                                {
                                    Main.AddLog($"Admin denied notification Id: {temp[option].Id} -> ");

                                    temp[option].Visiblity = -1;
                                }
                                break;
                            }
                        }
                    }
                }

                else
                {
                    foreach (var a in AdminNotifications)
                        if (a.Visiblity == -1)
                            temp.Add(a);
                    int option = Main.PrintList<Notification>(temp, "Deleted Notifications", false);
                    if (option != -1)
                        if (Selectoption("Do you want to restore") == 1)
                        {
                            temp[option].Visiblity = 1;
                            Main.AddLog($"Admin restored notification Id: {temp[option].Id} -> ");
                        }
                }

            }
        }
    }
    public static void AllVacancies()
    {
        string[] arr = new string[3] { "Checked", "Unchecked", "Deleted" };
        int sl = 0;
        while (true)
        {
            Console.Clear();
            Main.Show(arr, sl);
            int k = Main.Control(arr.Length, ref sl);
            if (k == -1)
                return;

            else if (k == 1)
            {
                List<Vacancy> temp = new();
                if (sl == 0)
                {
                    foreach (var v in AdminVacancies)
                        if (v.Visiblity == 1)
                            temp.Add(v);
                    int option = Main.PrintList<Vacancy>(temp, "Checked Vacancies");
                    if (option != -1)
                        if (Selectoption() == 1)
                        {
                            temp[option].Visiblity = -1;
                            Main.AddLog($"Admin deleted vacancy Id: {temp[option].Id} -> ");
                        }
                }

                else if (sl == 1)
                {
                    foreach (var a in AdminVacancies)
                        if (a.Visiblity == 0)
                            temp.Add(a);
                    int option = Main.PrintList<Vacancy>(temp, "Unchecked Vacancies", false);
                    if (option != -1)
                    {
                        int a = 0;
                        string[] str = new string[2] { "Allow", "Deny" };
                        while (true)
                        {
                            Console.Clear();
                            Main.Show(str, a);
                            int control = Main.Control(str.Length, ref a);
                            if (control == -1)
                                break;

                            else if (control == 1)
                            {
                                if (a == 0)
                                {
                                    temp[option].Visiblity = 1;
                                    Main.AddLog($"Admin allowed vacancy Id: {temp[option].Id} -> ");

                                }
                                else if (a == 1)
                                {
                                    temp[option].Visiblity = -1;
                                    Main.AddLog($"Admin denied vacancy Id: {temp[option].Id} -> ");
                                }
                                break;
                            }
                        }
                    }
                }

                else if (sl == 2)
                {
                    foreach (var v in AdminVacancies)
                        if (v.Visiblity == -1)
                            temp.Add(v);
                    int option = Main.PrintList<Vacancy>(temp, "Deleted Vacancies");
                    if (option != -1)
                        if (Selectoption("Do you want to restore") == 1)
                        {
                            temp[option].Visiblity = 1;
                            Main.AddLog($"Admin restored vacancy Id: {temp[option].Id} -> ");
                        }
                }
            }
        }
    }
}

