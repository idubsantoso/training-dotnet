using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Dto
{
    public class AuthorDto
    {
        public int? Id { get; set; }
        [StringLength(100, MinimumLength = 5)]
        [Required(ErrorMessage ="Please enter name")]
        public string? Name { get; set; }
        [StringLength(500)]
        public string? Address { get; set; }
        public string? NumberId { get; set; }
        public List<int>? BookIds { get; set; }

        public List<BookAuthor>? BookAuthors { get; set; }
    }

    public class AuthorWithBooksDto
    {
        public int? Id { get; set; }
        [StringLength(100, MinimumLength = 5)]
        [Required(ErrorMessage ="Please enter name")]
        public string? Name { get; set; }
        [StringLength(500)]
        public string? Address { get; set; }
        public string? NumberId { get; set; }
        public List<string>? BookNames { get; set; }
    }
}