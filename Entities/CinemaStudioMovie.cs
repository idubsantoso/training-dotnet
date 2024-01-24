using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class CinemaStudioMovie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int MovieId { get; set; }
        public int CinemaId { get; set; }
        public DateTime? Schedule { get; set; }
        public CinemaStudio? Cinema { get; set; }
        public Movie? Movie { get; set; }


    }
}
