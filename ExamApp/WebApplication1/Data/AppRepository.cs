using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class AppRepository : IAppRepository
    {
        private DataContext _context;

        public AppRepository (DataContext context)
        {
            _context = context;
        }
        const string crawlUrl = @"https://www.wired.com/";

        public List<Texts> GetTexts()
        {
            string[] links = new string[5];
            string[] texts = new string[5];
            string[] titles = new string[5];
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(crawlUrl);
            titles[0] = HtmlEntity.DeEntitize(htmlDoc.DocumentNode.SelectNodes("//*[@id='app-root']/div/div[3]/div/div/div[2]/div[1]/div/div[1]/div[1]/div[1]/div/ul/li[2]/a[2]/h2")[0].InnerText);
            titles[1] = HtmlEntity.DeEntitize(htmlDoc.DocumentNode.SelectNodes("//*[@id='app-root']/div/div[3]/div/div/div[2]/div[1]/div/div[1]/div[1]/div[2]/div/ul/li[2]/a[2]/h2")[0].InnerText);
            titles[2] = HtmlEntity.DeEntitize(htmlDoc.DocumentNode.SelectNodes("//*[@id='app-root']/div/div[3]/div/div/div[2]/div[1]/div/div[1]/div[2]/div[1]/div[1]/div/ul/li[2]/a[2]/h2")[0].InnerText);
            titles[3] = HtmlEntity.DeEntitize(htmlDoc.DocumentNode.SelectNodes("//*[@id='app-root']/div/div[3]/div/div/div[2]/div[1]/div/div[1]/div[2]/div[2]/div/ul/li[2]/a[2]/h2")[0].InnerText);
            titles[4] = HtmlEntity.DeEntitize(htmlDoc.DocumentNode.SelectNodes("//*[@id='app-root']/div/div[3]/div/div/div[2]/div[1]/div/div[1]/div[2]/div[1]/div[2]/div/ul/li[2]/a[2]/h2")[0].InnerText);
            links[0] = htmlDoc.DocumentNode.SelectNodes("//*[@id='app-root']/div/div[3]/div/div/div[2]/div[1]/div/div[1]/div[1]/div[1]/div/ul/li[2]/a[2][@href]")[0].GetAttributeValue("href", string.Empty);
            links[1] = htmlDoc.DocumentNode.SelectNodes("//*[@id='app-root']/div/div[3]/div/div/div[2]/div[1]/div/div[1]/div[1]/div[2]/div/ul/li[2]/a[2][@href]")[0].GetAttributeValue("href", string.Empty);
            links[2] = htmlDoc.DocumentNode.SelectNodes("//*[@id='app-root']/div/div[3]/div/div/div[2]/div[1]/div/div[1]/div[2]/div[1]/div[1]/div/ul/li[2]/a[2][@href]")[0].GetAttributeValue("href", string.Empty);
            links[3] = htmlDoc.DocumentNode.SelectNodes("//*[@id='app-root']/div/div[3]/div/div/div[2]/div[1]/div/div[1]/div[2]/div[2]/div/ul/li[2]/a[2][@href]")[0].GetAttributeValue("href", string.Empty);
            links[4] = htmlDoc.DocumentNode.SelectNodes("//*[@id='app-root']/div/div[3]/div/div/div[2]/div[1]/div/div[1]/div[2]/div[1]/div[2]/div/ul/li[2]/a[2][@href]")[0].GetAttributeValue("href", string.Empty);

            for (int j = 0; j <= 4; j++)
            {
                string titleUrl = @"https://www.wired.com" + links[j];

                HtmlWeb web2 = new HtmlWeb();
                var htmlDoc2 = web2.Load(titleUrl);

                var root = htmlDoc2.DocumentNode.SelectNodes("//*[@id='main-content']/article/div[2]/div/div[1]")[0];
                var childNodes = root.ChildNodes;
                int p;
                for (int i = 1; i <= childNodes.Count; i += 2)
                {
                    var node = htmlDoc2.DocumentNode.SelectNodes("//*[@id='main-content']/article/div[2]/div/div[1]/div[" + i + "]/div[1]")[0];
                    var childNodess = node.ChildNodes;
                    p = 0;
                    foreach (var n in childNodess)
                    {
                        if (n.Name == "p")
                            p++;
                    }
                    for (int y = 1; y <= p; y++)
                    {
                        texts[j] += HtmlEntity.DeEntitize(htmlDoc2.DocumentNode.SelectNodes("//*[@id='main-content']/article/div[2]/div/div[1]/div[" + i + "]/div[1]/p[" + y + "]")[0].InnerText) + "\n";
                    }
                }
            }

            List<Texts> text = new List<Texts>
            {
                new Texts{ Id=1, Link=links[0], Text=texts[0], Title=titles[0] },
                new Texts{ Id=2, Link=links[1], Text=texts[1], Title=titles[1] },
                new Texts{ Id=3, Link=links[2], Text=texts[2], Title=titles[2] },
                new Texts{ Id=4, Link=links[3], Text=texts[3], Title=titles[3] },
                new Texts{ Id=5, Link=links[4], Text=texts[4], Title=titles[4] },
            };
            return text;
        }

        public Texts GetTextById(int id)
        {
            List<Texts> texts = GetTexts();
            Texts text = texts[id - 1];
            return text;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete(int id)
        {
            var exam = _context.Exams.FirstOrDefault(e => e.Id == id);
            _context.Remove(exam);
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public List<Question> GetQuestions(int id)
        {
            var questions = _context.Questions.Where(q => q.ExamId == id).ToList();
            return questions;
        }

        public List<Exam> GetExams()
        {
            var exams = _context.Exams.OrderByDescending(e => e.DateAdded).ToList();
            return exams;
        }

        public Exam GetExam(int id)
        {
            var exams = _context.Exams.Include(e => e.Questions).FirstOrDefault(e => e.Id == id);
            return exams;
        }
    }
}
