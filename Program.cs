using MinimalAPI_POC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/Hello", () =>
{
    //var forecast = Enumerable.Range(1, 5).Select(index =>
    //    new WeatherForecast
    //    (
    //        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
    //        Random.Shared.Next(-20, 55),
    //        summaries[Random.Shared.Next(summaries.Length)]
    //    ))
    //    .ToArray();
    //return forecast;
    return "Hello World!";
})
.WithName("GetWeatherForecast")
.WithOpenApi();
//------------------------------------------

List<Student> listOfStudentnts = new List<Student>();

app.MapGet("Student/GetAllStudent", () =>
{
    return listOfStudentnts; 
});

app.MapGet("Student/GetStudent/{id}", (int id) =>
{
    int index = listOfStudentnts.FindIndex(x => x.Id == id);
    if (index < 0) { return Results.NotFound(index); }
    return Results.Ok(listOfStudentnts[index]);
});
app.MapPost("Student/AddStudent", (Student student) =>
{
    listOfStudentnts.Add(student);
});

app.MapPost("Student/AddListOfStudents", (List<Student> studentList) =>
{
    listOfStudentnts.AddRange(studentList);
});

app.MapPut("Student/{Id}", (int id, Student student) =>
{
    int index = listOfStudentnts.FindIndex(x => x.Id == id);
    if (index < 0) { return Results.NotFound(index); }
    listOfStudentnts[index] = student;
    return Results.Ok();
});

app.MapDelete("Student/DeleteStudent/{id}", (int id) =>
{
    int index = listOfStudentnts.FindIndex(x => x.Id == id);
    if (index < 0) { return Results.NotFound(index); }
    int count = listOfStudentnts.RemoveAll(x => x.Id == index);
    if (count > 0) return Results.Ok();
    return Results.NotFound();
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
