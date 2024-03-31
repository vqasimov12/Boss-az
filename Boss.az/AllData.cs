using Boss.az.Models.Human;
using Boss.az.Models.UserInfo;
using Boss.az.Models.Work;
using Newtonsoft.Json;

namespace Boss.az;

public class AllData
{
    public static Main main { get; set; } = new();
    public static void SerializeConfig()
    {
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented
        };
        string json = JsonConvert.SerializeObject(Admin.AdminNotifications, settings);
        File.WriteAllText(Main.DirectoryPath+"AdminNotifications.json", json);
        json = JsonConvert.SerializeObject(Admin.AdminVacancies, settings);
        File.WriteAllText(Main.DirectoryPath+"AdminVacancies.json", json);
        json = JsonConvert.SerializeObject(Admin.RemovedEmployers, settings);
        File.WriteAllText(Main.DirectoryPath + "RemovedEmployers.json", json);
        json = JsonConvert.SerializeObject(Admin.RemovedWorkers, settings);
        File.WriteAllText(Main.DirectoryPath + "RemovedWorkers.json", json);
        json = JsonConvert.SerializeObject(Main.workers, settings);
        File.WriteAllText(Main.DirectoryPath + "Worker.json", json);
        json = JsonConvert.SerializeObject(Main.employers, settings);
        File.WriteAllText(Main.DirectoryPath + "Employer.json", json);
        json = JsonConvert.SerializeObject(Main.Vacancies, settings);
        File.WriteAllText(Main.DirectoryPath + "Vacancy.json", json);
    }

    public static void DeserializeConfig()
    {
        try
        {
            if (!Directory.Exists("AllDatas"))
                Directory.CreateDirectory("AllDatas");
            Main.DirectoryPath = "AllDatas\\";

            if (File.Exists(Main.DirectoryPath + "Worker.json"))
            {
                string json = File.ReadAllText(Main.DirectoryPath + "Worker.json");
                Main.workers = JsonConvert.DeserializeObject<List<Worker>>(json)!;
            }

            if (File.Exists(Main.DirectoryPath + "Admin.json"))
            {
                string json = File.ReadAllText(Main.DirectoryPath + "Admin.json");
                Main.admin = JsonConvert.DeserializeObject<Admin>(json)!;
            }
            
            if (File.Exists(Main.DirectoryPath + "Employer.json"))
            {
                string json = File.ReadAllText(Main.DirectoryPath + "Employer.json");
                Main.employers = JsonConvert.DeserializeObject<List<Employer>>(json)!;
            }
            
            if (File.Exists(Main.DirectoryPath + "Vacancy.json"))
            {
                string json = File.ReadAllText(Main.DirectoryPath + "Vacancy.json");
                Main.Vacancies = JsonConvert.DeserializeObject<List<Vacancy>>(json)!;
            }
            
            if (File.Exists(Main.DirectoryPath + "AdminVacancies.json"))
            {
                string json = File.ReadAllText(Main.DirectoryPath + "AdminVacancies.json");
                Admin.AdminVacancies = JsonConvert.DeserializeObject<List<Vacancy>>(json)!;
            }
            
            if (File.Exists(Main.DirectoryPath + "AdminNotifications.json"))
            {
                string json = File.ReadAllText(Main.DirectoryPath + "AdminNotifications.json");
                Admin.AdminNotifications = JsonConvert.DeserializeObject<List<Notification>>(json)!;
            }
            
            if (File.Exists(Main.DirectoryPath + "RemovedEmployers.json"))
            {
                string json = File.ReadAllText(Main.DirectoryPath + "RemovedEmployers.json");
                Admin.RemovedEmployers = JsonConvert.DeserializeObject<List<Employer>>(json)!;
            }
            
            if (File.Exists(Main.DirectoryPath + "RemovedWorkers.json"))
            {
                string json = File.ReadAllText(Main.DirectoryPath + "RemovedWorkers.json");
                Admin.RemovedWorkers = JsonConvert.DeserializeObject<List<Worker>>(json)!;
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            _ = Console.ReadKey(true);
        }


    }
}

