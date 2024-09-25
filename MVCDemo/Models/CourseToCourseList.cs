#nullable disable

namespace MVCDemo.Models;

// 因為不要直接使用 model，所以建立一個 CourseToCourseList 類別擴充 Course 類別
public partial class CourseList
{
    // explicit 是明確轉型 implicit 是隱含轉型
    public static implicit operator CourseList(Course course)

    {
        return new CourseList
        {
            Title = course.Title,
            Credits = course.Credits,
            Description = course.Description,
            Slug = course.Slug
            // 欄位不一樣的話，可以在這裡做轉換邏輯
            // 例如 AAA = course.Credits.ToString()
        };
    }
}