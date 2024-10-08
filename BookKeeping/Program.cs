using BookingKeeping.Common.Implement;
using BookingKeeping.Common.Interface;
using BookKeeping.Repository.Implement;
using BookKeeping.Repository.Interface;
using BookKeeping.Service.Implement;
using BookKeeping.Service.Interface;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IInvoiceRepository>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    var errorLog = sp.GetRequiredService<IErrorLog>();

    return new InvoiceRepository(connectionString, errorLog);
});

builder.Services.AddScoped<IErrorLog>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var ErrorLogPath = configuration.GetValue<string>("ErrorLogPath");

    return new ErrorLog(ErrorLogPath);
});

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
