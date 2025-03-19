namespace aud1.example_exercise;

public interface IStudentEnrollment
{
    void EnrollStudentInCourse(Course course);
    
    void UnEnrollStudentInCourse(Course course);
    
    List<Course> getEnrolledCourses();
    
    bool IsEnrolledIn(Course course);
    
    string GetAcademicStatus();
}