namespace aud1.example_exercise;

public class UndergraduateStudent : Student
{

    private string major;
    private bool isHonorsStudent;

    public UndergraduateStudent(int id, string name, string surname, int year_enrolled) : base(id, name, surname, year_enrolled)
    {
        this.major = major;
        updateHonorsStatusStudent();
    }

    public void updateHonorsStatusStudent()
    {
        isHonorsStudent = calculateGPA() >= 9.5;
    }

    public override string GetAcademicStatus()
    {
        return $"Undergraduate Student - Major: {major}" +
               (isHonorsStudent ? " (Honors)" : "");
    }
}