using System.ComponentModel.DataAnnotations;
namespace Ecommerce.Models;

public class Course : BaseModel
{
    [MaxLength(256)]
    public string Name { get; set; } = string.Empty;
    public ICollection<Student> Students { get; set; } = null!;
}