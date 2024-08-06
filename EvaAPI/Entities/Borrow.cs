using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaAPI.Entities;

public class Borrow
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Book))]
    public int BookId { get; set; }
    
    [ForeignKey(nameof(Member))]
    public int MemberId{ get; set; }

    public DateTime BorrowDate { get; set; }
    public Book Book { get; set; }
    public Member Member { get; set; }
}