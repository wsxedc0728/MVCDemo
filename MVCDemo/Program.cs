using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

 //使用 AddAuthentication 和 AddCookie 方法新增驗證中介軟體服務
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        // 設定 Cookie 的過期時間為 20 分鐘
//        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
//        // 啟用滑動過期，這樣每次請求都會重置過期時間
//        options.SlidingExpiration = true;
//        // 設定當存取被拒時的重導向路徑
//        options.AccessDeniedPath = "/Home/";
//    });

// 使用 AddAuthentication 和 AddCookie 方法新增驗證中介軟體服務
builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        // 設定 Cookie 的過期時間為 20 分鐘
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        // 啟用滑動過期，這樣每次請求都會重置過期時間
        options.SlidingExpiration = true;
        // 設定當存取被拒時的重導向路徑
        options.AccessDeniedPath = "/Home/AccessDenied";
    });

// 註冊此 Attribute 將導致 power tool 產出時報錯 InvalidOperationException
//builder.Services.AddScoped<判斷是否有重複的課程名稱Attribute>();

builder.Services.AddScoped<IUnitOfWork, EFUnitOfWork>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();

builder.Services.AddDbContext<ContosoUniversityContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// UseAuthentication 和 UseAuthorization 必須在 MapRazorPages 和 MapDefaultControllerRoute 等 Map 方法之前進行呼叫
app.UseAuthentication();
// app.UseAuthentication 應該在 app.UseAuthorization 之前呼叫，這樣才能確保每個請求都會先進行身份驗證。
app.UseAuthorization();

//app.MapRazorPages();
app.MapDefaultControllerRoute();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
