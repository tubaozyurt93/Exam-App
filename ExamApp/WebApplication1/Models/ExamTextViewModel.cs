using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ExamTextViewModel
    {
        public int SelectedTextId { get; set; }

        public Exam Exam { get; set; }
    }
}
