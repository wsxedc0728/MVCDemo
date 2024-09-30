var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ���U�� Attribute �N�ɭP InvalidOperationException
builder.Services.AddScoped<�P�_�O�_�����ƪ��ҵ{�W��Attribute>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
