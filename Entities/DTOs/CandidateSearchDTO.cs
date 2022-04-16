﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CandidateSearchDTO
    {
        [Required(ErrorMessage="Candidate name is required")]
        public string? CandidateName { get; set; }
    }
}