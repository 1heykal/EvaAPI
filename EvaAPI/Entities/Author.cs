using System.ComponentModel.DataAnnotations;

namespace EvaAPI.Entities;

public class Author
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [DataType(DataType.Date)]
    public DateOnly BirthDate { get; set; }
    
    public virtual ICollection<Book> Books { get; set; }
    
}