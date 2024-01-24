using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Dto
{
    public class MovieDto
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Category { get; set; }
        public string? Image { get; set; }
    }

    public class MovieWithCinemasDto
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Category { get; set; }
        public string? Image { get; set; }
        public List<CinemaStudio>? CinemaStudios { get; set; }
    }
}