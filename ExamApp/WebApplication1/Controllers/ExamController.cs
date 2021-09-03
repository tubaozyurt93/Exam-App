using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    public class ExamController : Controller
    {
        private IAppRepository _appRepository;

        public ExamController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        public IActionResult Index()
        {
            var exams = _appRepository.GetExams();
            return View(exams);
        }

        public IActionResult DoExam(int id)
        {
            var exam = _appRepository.GetExam(id);
            return View(exam);
        }

        public IActionResult DeleteExam(int id)
        {
            _appRepository.Delete(id);
            _appRepository.SaveAll();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult IsCorrect(int id, string question1, string question2, string question3, string question4)
        {
            List<string> cevaplar = new List<string>();
            List<bool> result = new List<bool>();
            var exam = _appRepository.GetExam(id);
            cevaplar.Add(question1);
            cevaplar.Add(question2);
            cevaplar.Add(question3);
            cevaplar.Add(question4);
            for (int i = 0; i < exam.Questions.Count; i++)
            {
                if (exam.Questions[i].Answer == cevaplar[i])
                {
                    result.Add(true);
                }
                else
                {
                    result.Add(false);
                }
            }

            return Json(result);
        }
    }
}
