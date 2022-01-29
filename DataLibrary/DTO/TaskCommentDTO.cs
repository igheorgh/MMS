using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class TaskCommentDTO
    {
        public int Id { get; set; }
        public int Comment_id { get; set; }
        public int Task_id  { get; set; }
        public int User_id  { get; set; }
    }
}
