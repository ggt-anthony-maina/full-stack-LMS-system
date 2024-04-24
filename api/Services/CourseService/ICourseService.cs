public interface ICourseService
{
    Task<Course?> GetCourseByIdAsync(int id);
    Task<ServiceResponse<bool>> CreateCourseAsync(CourseDTO courseDTO);
    Task<ServiceResponse<bool>> UpdateCourseAsync(CourseDTO courseDTO);
    Task<ServiceResponse<bool>> DeleteCourseAsync(int id);
}

public class CourseService : ICourseService
{
    private readonly LibraryDbContext _dbContext;

    public CourseService(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Course?> GetCourseByIdAsync(int id)
    {
        return await _dbContext.Courses.FindAsync(id);
    }

    public async Task<ServiceResponse<bool>> CreateCourseAsync(CourseDTO courseDTO)
    {
        try
        {
            var course = new Course
            {
                Id = courseDTO.Id,
                Name = courseDTO.Name!,
                Description = courseDTO.Description
            };

            _dbContext.Courses.Add(course);
            await _dbContext.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                Data = true,
                Message = "Course created successfully.",
                StatusCode = 201
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating course: {ex.Message}");
            return new ServiceResponse<bool>
            {
                Success = false,
                Message = "Failed to create course.",
                StatusCode = 500
            };
        }
    }

    public async Task<ServiceResponse<bool>> UpdateCourseAsync(CourseDTO courseDTO)
    {
        try
        {
            var course = await _dbContext.Courses.FindAsync(courseDTO.Id);

            if (course == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Course not found.",
                    StatusCode = 404
                };
            }

            course.Name = courseDTO.Name!;
            course.Description = courseDTO.Description;

            await _dbContext.SaveChangesAsync();
            return new ServiceResponse<bool>
            {
                Data = true,
                Message = "Course updated successfully.",
                StatusCode = 200
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating course: {ex.Message}");
            return new ServiceResponse<bool>
            {
                Success = false,
                Message = "Failed to update course.",
                StatusCode = 500
            };
        }
    }

    public async Task<ServiceResponse<bool>> DeleteCourseAsync(int id)
    {
        try
        {
            var course = await _dbContext.Courses.FindAsync(id);

            if (course == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Course not found.",
                    StatusCode = 404
                };
            }

            _dbContext.Courses.Remove(course);
            await _dbContext.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                Data = true,
                Message = "Course deleted successfully.",
                StatusCode = 200
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting course: {ex.Message}");
            return new ServiceResponse<bool>
            {
                Success = false,
                Message = "Failed to delete course.",
                StatusCode = 500
            };
        }
    }
}
