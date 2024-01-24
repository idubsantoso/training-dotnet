using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Dto
{
    public class BookDto
    {
        public int? Id { get; set; }
        [StringLength(100, MinimumLength = 5)]
        [Required(ErrorMessage ="Please enter the title of your book")]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
        public string? Category { get; set; }
        [Required(ErrorMessage = "Please enter the total pages")]
        [Display(Name ="Total pages of book")]
        public int? TotalPages { get; set; }
        public List<int>? AuthorIds { get; set; }
        public ICollection<BookAuthor>? BookAuthors { get; set; } =new HashSet<BookAuthor>();
    }

    public class BookWithAuthorsDto
    {
        public int? Id { get; set; }
        [StringLength(100, MinimumLength = 5)]
        [Required(ErrorMessage ="Please enter the title of your book")]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
        public string? Category { get; set; }
        [Required(ErrorMessage = "Please enter the total pages")]
        [Display(Name ="Total pages of book")]
        public int? TotalPages { get; set; }
        public List<string>? AuthorNames { get; set; }
    }
}