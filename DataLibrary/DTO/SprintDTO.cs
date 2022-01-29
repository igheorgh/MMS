using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public  class SprintDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Start_date  { get; set; }
        public DateTime End_date { get; set; } 
        public string Goal { get;set; }
    }
}
