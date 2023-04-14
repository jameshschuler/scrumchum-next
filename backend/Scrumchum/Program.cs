using Scrumchum.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();

builder.Services.AddControllers();
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

app.UseCors(appBuilder =>
    appBuilder
        .WithOrigins("http://127.0.0.1:5173")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<PlanningHub>("/planning");

app.Run();