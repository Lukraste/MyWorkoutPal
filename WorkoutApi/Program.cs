using Microsoft.EntityFrameworkCore;
using WorkoutApi;
using WorkoutApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"))
);

var app = builder.Build();

//app.UseHttpsRedirection();

// Méthode d'affichage de tous les exercices via une requête GET

app.MapGet("api/exercices", async (AppDbContext context) =>
{
    var list = await context.Exercices.ToListAsync(); 
    return Results.Ok(list);
});

app.MapGet("api/exercices/{id}", async (AppDbContext context, int id) =>
{
    var exercice = await context.Exercices.FirstOrDefaultAsync(t => t.Id == id);
    return Results.Ok(exercice);
});

// Méthode d'ajout de d'exercice via une requête POST

app.MapPost("api/exercices/add", async (AppDbContext context, Exercice exercice) =>
{
    await context.Exercices.AddAsync(exercice);
    await context.SaveChangesAsync();
    return Results.Created($"api/exercices/{exercice.Id}", exercice);
});

// Méthode de mise à jour d'un exercice via une requête PUT (UPDATE)

app.MapPut("api/exercices/update/{id}", async (AppDbContext context, int id, Exercice exercice) =>
{
    var exerciceModel = await context.Exercices.FirstOrDefaultAsync(t => t.Id == id);

    if (exerciceModel == null)
    {
        return Results.NotFound();
    }
    exerciceModel.Name = exercice.Name;
    exerciceModel.Description = exercice.Description;
    exerciceModel.Muscle = exercice.Muscle;
    exerciceModel.Difficulty = exercice.Difficulty;

    await context.SaveChangesAsync();
    return Results.Created($"api/exercices/{exercice.Id}", exercice);
});

// Méthode de suppression d'un exercice via une requête DELETE

app.MapDelete("api/exercices/delete/{id}", async (AppDbContext context, int id) => 
{
    var exerciceModel = await context.Exercices.FirstOrDefaultAsync(t => t.Id == id);

    if (exerciceModel == null)
    {
        return Results.NotFound();
    }
    context.Exercices.Remove(exerciceModel);
    await context.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();