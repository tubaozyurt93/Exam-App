using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IAppRepository _appRepository;

        public HomeController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        public IActionResult Index()
        {
            ExamTextViewModel model = new ExamTextViewModel();
            var texts = _appRepository.GetTexts();
            ViewBag.TextData = new SelectList(texts, "Id", "Title");
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(ExamTextViewModel model)
        {
            Exam exam = new Exam();
            int i = 0;
            foreach (var m in model.Exam.Questions)
            {
                exam.Questions.Add(m);
                switch (m.Answer)
                {
                    case "1":
                        exam.Questions[i].Answer = m.Option1;
                        break;
                    case "2":
                        exam.Questions[i].Answer = m.Option2;
                        break;
                    case "3":
                        exam.Questions[i].Answer = m.Option3;
                        break;
                    case "4":
                        exam.Questions[i].Answer = m.Option4;
                        break;
                }
                i++;
            }
            if (ModelState.IsValid)
            {
                if (model.SelectedTextId != 0)
                {
                    Texts text = _appRepository.GetTextById(model.SelectedTextId);
                    exam.Title = text.Title;
                    exam.Text = text.Text;
                }
                _appRepository.Add(exam);
                _appRepository.SaveAll();
                return RedirectToAction("Index", "Exam");
            }
            var texts = _appRepository.GetTexts();
            ViewBag.TextData = new SelectList(texts, "Id", "Title");
            return View();
        }

        public JsonResult GetTextById(int id)
        {
            return Json(_appRepository.GetTextById(id));
        }
    }
}
