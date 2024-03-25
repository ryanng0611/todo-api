using System.ComponentModel.DataAnnotations;
namespace TodoApi.Models;

public class User
{
    [Key]
    public Guid UserId { get; set; }
    public string? DisplayName { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    [StringLength(10)]
    public UserRole Role { get; set; }
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; }
    public DateTime EditedAt {get; set; }
    public bool isDeleted { get; set; }
    public virtual ICollection<TodoItem>? TodoItems { get; set; }
}