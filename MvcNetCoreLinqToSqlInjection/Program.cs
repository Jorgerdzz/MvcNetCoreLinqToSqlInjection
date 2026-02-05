using MvcNetCoreLinqToSqlInjection.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

Coche coche = new Coche();
coche.Marca = "Volkswagen";
coche.Modelo = "GTI";
coche.Imagen = "gti.webp";
coche.Velocidad = 0;
coche.VelocidadMaxima = 330;
builder.Services.AddSingleton<ICoche, Coche>( x => coche);
//Resolvemos el Servicio coche para la inyeccion
//builder.Services.AddTransient<Coche>();
//builder.Services.AddSingleton<ICoche, Deportivo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
