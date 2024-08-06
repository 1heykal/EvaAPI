using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaAPI.Entities;

public class Book
{
    [Key]
    [DisplayName("BookId")]
    public int Id { get; set; }

    public string Title { get; set; }

    public string? Genre { get; set; }

    public DateTime PublicationDate { get; set; }

    [ForeignKey(nameof(Author))]
    public int AuthorId { get; set; }
    public Author Author { get; set; }
    
}