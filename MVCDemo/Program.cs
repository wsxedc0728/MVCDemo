using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

 //�ϥ� AddAuthentication �M AddCookie ��k�s�W���Ҥ����n��A��
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        // �]�w Cookie ���L���ɶ��� 20 ����
//        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
//        // �ҥηưʹL���A�o�˨C���ШD���|���m�L���ɶ�
//        options.SlidingExpiration = true;
//        // �]�w��s���Q�ڮɪ����ɦV���|
//        options.AccessDeniedPath = "/Home/";
//    });

// �ϥ� AddAuthentication �M AddCookie ��k�s�W���Ҥ����n��A��
builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        // �]�w Cookie ���L���ɶ��� 20 ����
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        // �ҥηưʹL���A�o�˨C���ШD���|���m�L���ɶ�
        options.SlidingExpiration = true;
        // �]�w��s���Q�ڮɪ����ɦV���|
        options.AccessDeniedPath = "/Home/AccessDenied";
    });

// ���U�� Attribute �N�ɭP power tool ���X�ɳ��� InvalidOperationException
//builder.Services.AddScoped<�P�_�O�_�����ƪ��ҵ{�W��Attribute>();

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

// UseAuthentication �M UseAuthorization �����b MapRazorPages �M MapDefaultControllerRoute �� Map ��k���e�i��I�s
app.UseAuthentication();
// app.UseAuthentication ���Ӧb app.UseAuthorization ���e�I�s�A�o�ˤ~��T�O�C�ӽШD���|���i�樭�����ҡC
app.UseAuthorization();

//app.MapRazorPages();
app.MapDefaultControllerRoute();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
