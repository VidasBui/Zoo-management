using Zoo_management.Data;
using Zoo_management.Data.Repositories;
using Zoo_management.Services;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ZooDbContext>();

builder.Services.AddTransient<IAnimalsRepository, AnimalsRepository>();
builder.Services.AddTransient<IEnclosuresRepository, EnclosuresRepository>();
builder.Services.AddTransient<IEnclosureObjectsRepository, EnclosureObjectsRepository>();

builder.Services.AddScoped<IDataUploadService, DataUploadService>();
builder.Services.AddScoped<IAnimalMigrationService, AnimalMigrationService>();


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

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.UseAuthorization();

app.MapControllers();
app.Run();
