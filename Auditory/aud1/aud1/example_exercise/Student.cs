namespace aud1.example_exercise;

public abstract class Student : IStudentEnrollment
{
    public int id { get; set; }
    public string name { get; set; }

    public string surname { get; set; }

    public int year_enrolled { get; set; }

    public List<Course> Courses { get; set; }

    public List<Grade> Grades { get; set; }

    protected Student(int id, string name, string surname, int year_enrolled)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty");
        if (year_enrolled < 1 || year_enrolled > 4)
            throw new ArgumentException("Year must be between 1 and 4");

        this.id = id;
        this.name = name;
        this.surname = surname;
        this.year_enrolled = year_enrolled;
        Courses = new List<Course>();
        Grades = new List<Grade>();
    }

    public void EnrollStudentInCourse(Course course)
    {
        if (course == null)
        {
            throw new ArgumentException(nameof(course));
        }

        if (!Courses.Contains(course))
        {
            Courses.Add(course);

            if (!course.students.Contains(this))
            {
                Courses.Add(course);
                course.EnrollStudents(this);
            }
        }
    }

    public void UnEnrollStudentInCourse(Course course)
    {
        if (course == null)
        {
            throw new ArgumentException(nameof(course));
        }

        if (!Courses.Contains(course))
        {
            Courses.Remove(course);
            course.UnenrollStudents(this);
        }
    }

    public List<Course> getEnrolledCourses()
    {
        throw new NotImplementedException();
    }

    public bool IsEnrolledIn(Course course)
    {
        return Courses.Where(course_enrolled => course_enrolled == course).Any();
    }

    public abstract string GetAcademicStatus();


    public double calculateGPA()
    {
        if (!Grades.Any())
        {
            return 0.0;
        }

        return Grades.Average(x => x.numericGrade);
    }

    public IEnumerable<Course> getCourseWithGradeAbove(double threshold)
    {
        return Grades.Where(g => g.numericGrade > threshold)
            .Select(g => g.course)
            .Distinct();
    }

    public override string ToString()
    {
        return $"{name} {surname} {id} : {year_enrolled} - GPA: {calculateGPA()}";
    }
}