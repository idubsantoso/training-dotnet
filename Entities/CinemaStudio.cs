using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class CinemaStudio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }

        public string? Theaters { get; set; }

        public DateTime? Schedule { get; set; }

        public List<CinemaStudioMovie>? CinemaStudioMovies { get; set; }


    }
}
