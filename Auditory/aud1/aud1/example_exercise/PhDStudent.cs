namespace aud1.example_exercise;

public class PhDStudent : Student
{
    public List<String> publications;

    public PhDStudent(int id, string name, string surname, int year_enrolled) : base(id, name, surname, year_enrolled)
    {
        publications = new List<string>();
    }

    public override string GetAcademicStatus()
    {
        return $"PhD Student has {publications.Count()} publications";
    }
}