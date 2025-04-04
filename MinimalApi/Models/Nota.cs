using System;
using System.ComponentModel.DataAnnotations;

public class Nota
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required, MaxLength(100)]
    public string Aluno { get; set; }

    [Required, MaxLength(100)]
    public string Disciplina { get; set; }

    [Required, Range(0, 10)]
    public decimal Valor { get; set; } = 0;

    public DateTime DataLancamento { get; set; } = DateTime.UtcNow;
}