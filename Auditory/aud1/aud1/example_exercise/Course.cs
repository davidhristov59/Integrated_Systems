using System.Text.RegularExpressions;

namespace aud1.example_exercise;

public class Course : ICourseEnrollment
{
    public string courseId { get; set; }
    public string courseName { get; set; }
    public int credits { get; set; }
    
    public List<Student> students { get; set; }
    
    public List<Grade> grades { get; set; }
    
    public Course(string courseId, string courseName, int credits)
    {
        if (!Regex.IsMatch(courseId, @"^[A-Z]{3}\d{3}$"))
            throw new ArgumentException("Course code must follow format XXX123");
        if (string.IsNullOrWhiteSpace(courseName))
            throw new ArgumentException("Course name cannot be empty");
        if (credits <= 0)
            throw new ArgumentException("Credits must be positive");

        this.courseId = courseId;
        this.courseName = courseName;
        this.credits = credits;
        students = new List<Student>();
        grades = new List<Grade>();
    }
    
    public void EnrollStudents(Student student)
    {
        if (student == null)
        {
            throw new ArgumentException(nameof(student));
        }

        if (!students.Contains(student))
        {
            students.Add(student);

            if (!student.Courses.Contains(this))
            {
                student.EnrollStudentInCourse(this);
            }
        }
    }

    public void UnenrollStudents(Student student)
    {
        if (student == null)
        {
            throw new ArgumentException(nameof(student));
        }
        
        if (!students.Contains(student))
        {
            students.Remove(student);

            if (!student.Courses.Contains(this))
            {
                student.UnEnrollStudentInCourse(this);
            }
        }
    }
    
    public double getAverageGrade()
    {
        return grades.GroupBy(x => x).Average(x => x.Count());
    }

    public Student getTopPerfomingStudent()
    {
        return grades
            .GroupBy(g => g.student)
            .OrderByDescending(x => x.Average(grade => grade.numericGrade))
            .Select(g => g.Key)
            .FirstOrDefault();
    }
    
    public IEnumerable<Student> GetFailingStudents()
    {
        return grades
            .Where(g => g.numericGrade == 5)
            .Select(g => g.student)
            .Distinct();
    }

    public override string ToString()
    {
        return $"Course {courseId}: {courseName} ({credits} credits) - Average: {getAverageGrade()}";
    }
}