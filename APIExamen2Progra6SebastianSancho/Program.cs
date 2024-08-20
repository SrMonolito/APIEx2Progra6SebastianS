using APIExamen2Progra6SebastianSancho.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        //se agrega codigo para para poder inyectar al cadena de conexion en appsettings.json

        //1. obtener el valor de la cadena en appsettings.json
        var CnnStrBuilder = new SqlConnectionStringBuilder(
            builder.Configuration.GetConnectionString("CnnStr"));

        //2.Como en la cadena no esta el password lo ponemos aqui
        CnnStrBuilder.Password = "123Queso";

        //3. creamos un string con la info de la cadena de conexion
        string cnnStr = CnnStrBuilder.ConnectionString;
        //4. Vamos a asignar el valor de esta cadena de conexion al
        //DB Context que esta en Models
        builder.Services.AddDbContext<AnswersDbContext>(options => options.UseSqlServer(cnnStr));


        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}