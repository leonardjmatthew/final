using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Models
{
    public class MovieCreate
    {
        [MaxLength(5000)]
        public string Description { get; set; }
    }
}
