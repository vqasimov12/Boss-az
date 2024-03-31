namespace Boss.az.Models.UserInfo;
public class Notification
{
    public string Id { get; set; }
    string? content { get; set; }
    public int Visiblity { get; set; }
    public string Receiver { get; set; }
    public string? Sender { get; set; }
    public DateTime Time { get; set; }
    public string WorkId { get; set; }
    public string HTML { get; set; }
    public string? Content
    {
        get { return content; }
        set
        {
            if (value.Length < 5)
                throw new("Notification content should contain min 5 character");
            content = value;
        }
    }
    public Notification()
    {
        Id = Guid.NewGuid().ToString("N").Substring(0, 8);
        Visiblity = 0;
        Time = DateTime.Now;
    }
    public override string ToString()
    {
        return $"\t\t\t\t\t{content}";
    }
}
