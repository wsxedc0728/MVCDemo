#nullable disable
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace MVCDemo.Models;

public partial class CourseCreate
{
    [Required]
    [BindRequired]
    public string Title { get; set; }

    [Required]
    [BindRequired]
    [Range(0, 5, ErrorMessage = "值限定為 0 ~ 5")]
    [UIHint(nameof(Credits))]
    public int Credits { get; set; }
}