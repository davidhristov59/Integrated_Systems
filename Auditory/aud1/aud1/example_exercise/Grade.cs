namespace aud1.example_exercise;

public class Grade
{
    public Student student { get; set; }
    public Course course {get; set;}
    public double numericGrade { get; set; }
    public DateTime dateAdded {get; set;}

    public Grade(Student student, Course course, double numericGrade)
    {
        if (student == null)
        {
            throw new ArgumentNullException(nameof(student));
        }
        
        if (course == null)
        {
            throw new ArgumentNullException(nameof(course));
        }
        
        if (numericGrade < 0 || numericGrade > 100)
        {
            throw new ArgumentException("Grade must be between 0 and 100");
        }
        
        this.student = student;
        this.course = course;
        this.numericGrade = numericGrade;
        dateAdded = DateTime.Now;
    }

    public String getLetterGrade()
    {
        if (numericGrade >= 90 && numericGrade <= 100)
        {
            return "A";
        }
        else if (numericGrade >= 80 && numericGrade <= 90)
        {
            return "B";
        }
        else if (numericGrade >= 70 && numericGrade <= 80)
        {
            return "C";
        }
        else
        {
            return "D";
        }
    }

    public override string ToString()
    {
        return $"{student.name} - {course.courseId}: {numericGrade} ({getLetterGrade()})";
    }
}