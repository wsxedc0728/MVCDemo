#nullable disable
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MVCDemo.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace MVCDemo.Models;

public partial class CourseCreate : IValidatableObject
{
    [Required]
    [BindRequired]
    [判斷是否有重複的課程名稱]
    public string Title { get; set; }

    [Required]
    [BindRequired]
    [Range(0, 5, ErrorMessage = "值限定為 0 ~ 5")]
    [UIHint(nameof(Credits))]
    public int Credits { get; set; }

    public string Description { get; set; }

    public DateTime StartDate { get; set; }

    // 避免執行順序導致提前回傳訊息，實務上會再寫一個 middleware，將所有檢核都跑完後一次回傳錯誤訊息
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (this.Title.Contains("Git") && this.Credits < 3)
        {
            yield return new ValidationResult("Git 課程學分不能小於 3", new[] { nameof(Title) });
        }

        throw new NotImplementedException();
    }
}