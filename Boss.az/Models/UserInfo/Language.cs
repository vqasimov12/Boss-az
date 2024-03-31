namespace Boss.az.Models.UserInfo;
public class Language
{
    string name { get; set; }
    public string Name { get { return name; } 
        set {
            if (value.Length < 3)
                throw new("Name is not in correct form ");
            name = value;
        }
    }
    public string LanguageLevel { get; set; }
    public override string ToString()
    {
        return $"Language {Name} -> {LanguageLevel} ";
    }
}

