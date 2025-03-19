namespace aud1.example_exercise;

public class MasterStudent : Student
{

    private string supervisor;
    private string thesisTopic;
    private bool hasCompletedThesis;
    private string researchArea;

    public MasterStudent(int id, string name, string surname, int year_enrolled, string supervisor, string thesisTopic, bool hasCompletedThesis, string researchArea) : base(id, name, surname, year_enrolled)
    {

        if (researchArea == null)
        {
            throw new ArgumentNullException("Research Area cannot be null");
        }
        
        this.researchArea = researchArea;
    }

    public void AssignSupervisor(string supervisor)
    {
        if (string.IsNullOrWhiteSpace(supervisor))
        {
            throw new ArgumentNullException("Supervisor cannot be null");
        }
        
        this.supervisor = supervisor;
    }

    public void submitThesis(string thesisTopic)
    {
        if (string.IsNullOrWhiteSpace(thesisTopic))
        {
            throw new ArgumentNullException("Thesis Topic cannot be null");
        }

        if (string.IsNullOrWhiteSpace(supervisor))
        {
            throw new ArgumentNullException("The thesis cannot be done without a supervisor");
        }
        
        this.thesisTopic = thesisTopic;
        hasCompletedThesis = true;
    }

    public bool checkStatusThesis()
    {
        return hasCompletedThesis;
    }

    public void AddResearchPublication(string publication)
    {
        throw new NotSupportedException("Master students are not required to publish");
    }

    public override string GetAcademicStatus()
    {
        return $"Master Student - Research Area: {researchArea}, " +
               $"Thesis: {(hasCompletedThesis ? "Completed" : "In Progress")}";    
    }
}