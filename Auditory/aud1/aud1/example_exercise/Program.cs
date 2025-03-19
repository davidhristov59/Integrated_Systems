namespace aud1.example_exercise;

public class Program
{
    static void Main(string[] args)
    {
        try
        {
            var student1 = new UndergraduateStudent(1, "David", "Hristov", 2022);
            var student2 = new MasterStudent(2, "David", "Smith", 2026, "Stefan Andonov",
                "Artificial Intelligence something", true, "AI");
            var student3 = new PhDStudent(3, "David", "Hristov", 2028);

            var math = new Course("MAT101", "Calculus", 6);
            var programming = new Course("CSE102", "Programming", 6);

            student1.EnrollStudentInCourse(math);
            student1.EnrollStudentInCourse(programming);
            student2.EnrollStudentInCourse(math);

            var grade1 = new Grade(student1, math, 6);
            var grade2 = new Grade(student1, programming, 10);
            var grade3 = new Grade(student2, math, 8);

            Console.WriteLine("Student Information:");
            Console.WriteLine(student1);
            Console.WriteLine(student2);

            Console.WriteLine("\nCourse Information:");
            Console.WriteLine(math);
            Console.WriteLine(programming);

            Console.WriteLine("\nTop Performing Student in Math:");
            Console.WriteLine(math.getTopPerfomingStudent());

            Console.WriteLine("\nFailing Students in Math:");
            math.GetFailingStudents().ToList().ForEach(x => Console.WriteLine(x));

            Console.WriteLine("\nJohn's Courses with Grade Above 90:");
            foreach (var course in student1.getCourseWithGradeAbove(90))
            {
                Console.WriteLine(course);
            }

            var test = new
            {
                Name = student1.name,
                Major = student1.id
            };

            Console.WriteLine(test);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}