using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Ecommerce.Models;
using System.Text.Json.Serialization;

public class Student : BaseModel
{
    public string FirstName { get; set; } = null!;
    public Course? Course { get; set; } = null!;

    public int? CourseId { get; set; }
}