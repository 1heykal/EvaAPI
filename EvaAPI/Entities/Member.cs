using System.ComponentModel.DataAnnotations;

namespace EvaAPI.Entities;

public class Member
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    [DataType(DataType.Date)]
    public DateOnly JoinDate { get; set; }

    [DataType(DataType.Date)]
    public DateOnly BirthDate { get; set; }

    public ICollection<Borrow> Borrows { get; set; }
}