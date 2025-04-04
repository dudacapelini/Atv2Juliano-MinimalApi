using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

public static class NotaEndpoints
{
    public static void MapNotaEndpoints(this WebApplication app)
    {
        app.MapPost("/notas", async ([FromBody] Nota nota, AppDbContext db) =>
        {
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(nota, new ValidationContext(nota), validationResults, true))
            {
                return Results.BadRequest(validationResults);
            }

            db.Notas.Add(nota);
            await db.SaveChangesAsync();
            return Results.Created($"/notas/{nota.Id}", nota);
        });

        app.MapGet("/notas", async (AppDbContext db) => await db.Notas.ToListAsync());

        app.MapGet("/notas/{id}", async (Guid id, AppDbContext db) =>
            await db.Notas.FindAsync(id) is Nota nota ? Results.Ok(nota) : Results.NotFound());

        app.MapPut("/notas/{id}", async (Guid id, [FromBody] Nota updatedNota, AppDbContext db) =>
        {
            var nota = await db.Notas.FindAsync(id);
            if (nota == null) return Results.NotFound();

            nota.Aluno = updatedNota.Aluno;
            nota.Disciplina = updatedNota.Disciplina;
            nota.Valor = updatedNota.Valor;

            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(nota, new ValidationContext(nota), validationResults, true))
            {
                return Results.BadRequest(validationResults);
            }

            await db.SaveChangesAsync();
            return Results.Ok(nota);
        });

        app.MapDelete("/notas/{id}", async (Guid id, AppDbContext db) =>
        {
            var nota = await db.Notas.FindAsync(id);
            if (nota == null) return Results.NotFound();

            db.Notas.Remove(nota);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}