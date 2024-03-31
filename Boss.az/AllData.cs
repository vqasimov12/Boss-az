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
        File.WriteAllText("AdminNotifications.json", json);
        json = JsonConvert.SerializeObject(Admin.AdminVacancies, settings);
        File.WriteAllText("AdminVacancies.json", json);
        json = JsonConvert.SerializeObject(Admin.RemovedEmployers, settings);
        File.WriteAllText("RemovedEmployers.json", json);
        json = JsonConvert.SerializeObject(Admin.RemovedWorkers, settings);
        File.WriteAllText("RemovedWorkers.json", json);
        json = JsonConvert.SerializeObject(Main.workers, settings);
        File.WriteAllText("Worker.json", json);
        json = JsonConvert.SerializeObject(Main.employers, settings);
        File.WriteAllText("Employer.json", json);
        json = JsonConvert.SerializeObject(Main.Vacancies, settings);
        File.WriteAllText("Vacancy.json", json);
    }

    public static void DeserializeConfig()
    {
        try
        {
            if (!Directory.Exists("AllDatas"))
                Directory.CreateDirectory("AllDatas");
            Main.DirectoryPath = "AllDatas";

            if (File.Exists("Worker.json"))
            {
                string json = File.ReadAllText("Worker.json");
                Main.workers = JsonConvert.DeserializeObject<List<Worker>>(json)!;
            }

            if (File.Exists("Admin.json"))
            {
                string json = File.ReadAllText("Admin.json");
                Main.admin = JsonConvert.DeserializeObject<Admin>(json)!;
            }
            
            if (File.Exists("Employer.json"))
            {
                string json = File.ReadAllText("Employer.json");
                Main.employers = JsonConvert.DeserializeObject<List<Employer>>(json)!;
            }
            
            if (File.Exists("Vacancy.json"))
            {
                string json = File.ReadAllText("Vacancy.json");
                Main.Vacancies = JsonConvert.DeserializeObject<List<Vacancy>>(json)!;
            }
            
            if (File.Exists("AdminVacancies.json"))
            {
                string json = File.ReadAllText("AdminVacancies.json");
                Admin.AdminVacancies = JsonConvert.DeserializeObject<List<Vacancy>>(json)!;
            }
            
            if (File.Exists("AdminNotifications.json"))
            {
                string json = File.ReadAllText("AdminNotifications.json");
                Admin.AdminNotifications = JsonConvert.DeserializeObject<List<Notification>>(json)!;
            }
            
            if (File.Exists("RemovedEmployers.json"))
            {
                string json = File.ReadAllText("RemovedEmployers.json");
                Admin.RemovedEmployers = JsonConvert.DeserializeObject<List<Employer>>(json)!;
            }
            
            if (File.Exists("RemovedWorkers.json"))
            {
                string json = File.ReadAllText("RemovedWorkers.json");
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

