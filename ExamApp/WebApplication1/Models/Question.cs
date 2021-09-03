using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        [Required(ErrorMessage = "Soru gerekli.")]
        public string QuestionText { get; set; }
        [Required(ErrorMessage = "Seçenek gerekli.")]
        public string Option1 { get; set; }
        [Required(ErrorMessage = "Seçenek gerekli.")]
        public string Option2 { get; set; }
        [Required(ErrorMessage = "Seçenek gerekli.")]
        public string Option3 { get; set; }
        [Required(ErrorMessage = "Seçenek gerekli.")]
        public string Option4 { get; set; }
        public string Answer { get; set; }
    }
}
