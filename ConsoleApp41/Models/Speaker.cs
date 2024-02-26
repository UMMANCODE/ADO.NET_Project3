using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp41.Models {
    internal class Speaker {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Position { get; set; }
        public string? Company { get; set; }
        public string? ImageUrl { get; set; }

        override public string ToString() {
            return $"{Id} - {FullName} - {Company}";
        }
    }
}
