using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public interface IAppRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete(int id);
        bool SaveAll();

        List<Texts> GetTexts();
        Texts GetTextById(int id);
        List<Question> GetQuestions(int id);
        List<Exam> GetExams();
        Exam GetExam(int id);
    }
}
