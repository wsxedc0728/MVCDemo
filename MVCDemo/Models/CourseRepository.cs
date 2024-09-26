using MVCDemo.Models;

public class CourseRepository : EFRepository<Course>, ICourseRepository
{
    public Course? FindOne(int id)
    {
        return base.All().FirstOrDefault(d => d.CourseId == id);
    }
    public Course? FindOne(string title)
    {
        return base.All().FirstOrDefault(d => d.Title == title);
    }

    public IQueryable<Course> FindAll()
    {
        return base.All();
    }

    public async Task<Course?> FindOneJoinDepartment(int id)
    {
        return await base.All().FirstOrDefaultAsync(d => d.CourseId == id);
    }
}

public interface ICourseRepository : IRepository<Course>
{
    Course? FindOne(int id);
    Course? FindOne(string title);
    Task<Course?> FindOneJoinDepartment(int id);

    IQueryable<Course> FindAll();
}