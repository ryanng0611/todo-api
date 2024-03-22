using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models;

public class TodoItem
{
    [Key]
    public long Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }

    [ForeignKey("User")]
    public Guid UserId { get; set; }
    public virtual User? User { get; set; }
}