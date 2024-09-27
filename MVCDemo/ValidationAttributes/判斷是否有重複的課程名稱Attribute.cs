using System.ComponentModel.DataAnnotations;

namespace MVCDemo.ValidationAttributes
{
    public class 判斷是否有重複的課程名稱Attribute : ValidationAttribute
    {


        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var repo = (ICourseRepository)validationContext.GetService(typeof(ICourseRepository))!;

            //var repo = validationContext.GetService(typeof(ICourseRepository)) as ICourseRepository;

            if (repo == null)
            {
                throw new InvalidOperationException("ICourseRepository 服務未註冊");
            }

            // 檢查是否有重複的課程名稱
            string? title = value as string;
            if (string.IsNullOrEmpty(title))
            {
                return ValidationResult.Success;
            }
            // 這裡可以寫檢查邏輯
            bool exists = repo.FindAll().Any(c => c.Title == title);

            if(exists)
            {
                var fieldname = new[] { validationContext.MemberName ?? "" };
                return new ValidationResult("課程名稱重複", fieldname);
            }

            return ValidationResult.Success;
        }
    }
}
