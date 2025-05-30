var builder = WebApplication.CreateBuilder(args);

// Thêm các dịch vụ
builder.Services.AddControllers();            
builder.Services.AddControllersWithViews();   

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets(); 

// Route cho giao diện
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllers();

app.Run();
