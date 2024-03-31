using System.Text;

namespace Boss.az.Models.UserInfo;
public class Cv
{
    public List<string> Skills { get; set; } = new();
    public List<Language> Languages { get; set; } = new();
    public List<Company> Companies { get; set; } = new();
    public string School
    {
        get { return school; }
        set
        {
            if (value.Length < 3)
                throw new("School name should contain min 3 character");
            school = value;
        }
    }
    public int Point
    {
        get { return point; }
        set
        {
            if (value < 150 || value > 700)
                throw new("Point should between 150 and 700");
            point = value;
        }
    }
    public string Speciality { get; set; }
    public string University { get; set; }
    public bool HonorDiplom = false;
    string school { get; set; }
    int point { get; set; } = 0;
    public Cv(string school, int point, string speciality, string univercity, bool honorDiplom)
    {
        School = school;
        Point = point;
        Speciality = speciality;
        University = univercity;
        HonorDiplom = honorDiplom;
    }
    public Cv()
    {

    }
    public void AddSkill()
    {
        Console.Write("Enter Skill: ");
        string skill = Console.ReadLine()!;
        if (skill.Length < 3)
            throw new("skill should contain min 3 character");
        if (!Skills.Exists(s => s.ToLower() == skill.ToLower()))
            Skills.Add(skill.ToLower());
        else
        {
            Console.WriteLine("This skill is already available");
            _ = Console.ReadKey(true);
        }
    }
    public void AddLanguage()
    {
        string[] levels = new string[6] { "A0", "A1", "A2", "B1", "B2", "C" };
        int select = 0;
        Language l = new Language();
        Console.Clear();
        Console.WriteLine(Main.font);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Enter Language: ");
        while (true)
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                l.Name = Console.ReadLine()!;
                Console.ForegroundColor = ConsoleColor.White;
                if (Languages.Exists(s => s.Name.ToLower().Trim() == l.Name.ToLower().Trim()))
                {
                    Console.WriteLine("This languge has already exist");
                    _ = Console.ReadKey(true);
                    return;
                }
                break;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _ = Console.ReadKey(true);
            }
            catch
            {
                Console.WriteLine("Error occupied");
                _ = Console.ReadKey(true);
            }

        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Main.Show(levels, select, false, $"Enter Language: {l.Name}\n");
            int a = Main.Control(levels.Length, ref select);
            if (a == -1)
                return;

            else if (a == 1)
            {
                l.LanguageLevel = levels[select];
                break;
            }
        }
        Languages.Add(l);
    }
    public void AddCompany()
    {
        Company c = new();
        try
        {
            Console.Clear();
            Console.WriteLine(Main.font);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Enter Company name: ");
            Console.ForegroundColor = ConsoleColor.Green;
            c.Name = Console.ReadLine()!;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Enter Time you worked for company (1 year): ");
            Console.ForegroundColor = ConsoleColor.Green;
            c.Time = Console.ReadLine()!;
            Companies.Add(c);
            Console.WriteLine("Company added successfully");
            Console.ForegroundColor = ConsoleColor.White;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        catch
        {
            Console.WriteLine("Something went wrong");
        }
        _ = Console.ReadKey(true);
    }
    public override string ToString()
    {
        StringBuilder cvContent = new StringBuilder();

        cvContent.AppendLine($"School: {School}");
        cvContent.AppendLine($"Point: {Point}");
        cvContent.AppendLine($"Speciality: {Speciality}");
        cvContent.AppendLine($"University: {University}");

        if (HonorDiplom)
            cvContent.AppendLine(" (Honor Diplom)");
        else
            cvContent.AppendLine();

        cvContent.AppendLine("Skills:");
        foreach (string skill in Skills)
        {
            cvContent.AppendLine($"* {skill}");
        }

        cvContent.AppendLine("Languages:");
        foreach (Language language in Languages)
        {
            cvContent.AppendLine($"* {language.Name} ({language.LanguageLevel})");
        }

        cvContent.AppendLine("Companies:");
        foreach (Company company in Companies)
        {
            cvContent.AppendLine($"* {company.Name} ({company.Time})");
        }

        return cvContent.ToString();

    }
}