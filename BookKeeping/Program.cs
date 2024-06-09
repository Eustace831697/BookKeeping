using BookKeeping.Repository.Implement;
using BookKeeping.Repository.Interface;
using BookKeeping.Service.Implement;
using BookKeeping.Service.Interface;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//SqlConnection
//builder.Services.AddScoped<SqlConnection>(sp =>
//{
//    var configuration = sp.GetRequiredService<IConfiguration>();
//    var connectionString = configuration.GetConnectionString("DefaultConnection");
//    return new SqlConnection(connectionString);
//});
builder.Services.AddScoped<IInvoiceRepository>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    return new InvoiceRepository(connectionString);
});

//builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
