using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Dto
{
    public class CinemaDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Theaters { get; set; }
        public DateTime? Schedule { get; set; }
        public List<int>? MovieIds { get; set; }
        public List<CinemaStudioMovie>? CinemaStudioMovies { get; set; }
    }

    public class CinemaWithMoviesDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Theaters { get; set; }
        public DateTime? Schedule { get; set; }
        public List<Movie>? Movies { get; set; }
    }
}