using System;
using System.Collections.Generic;

#nullable disable

namespace Benchmarks.Models
{
    public partial class Player
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int TeamId { get; set; }

        public virtual Team Team { get; set; }
    }
}
