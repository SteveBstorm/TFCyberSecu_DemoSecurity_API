using DemoSecurity_BLL.Interface;
using DemoSecurity_DAL.Entities;
using DemoSecurity_DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSecurity_BLL.Services
{
    public class ArticleBLLService : IArticleBLLService
    {
        private readonly IArticleRepository _articleRepo;

        public ArticleBLLService(IArticleRepository articleRepo)
        {
            _articleRepo = articleRepo;
        }

        public async Task Create(Article article)
        {
            await _articleRepo.Create(article);
        }

        public void Delete(int id)
        {
            _articleRepo.Delete(id);
        }

        public IEnumerable<Article> GetAll()
        {
            return _articleRepo.GetAll();
        }

        public Article GetById(int id)
        {
            return _articleRepo.GetById(id);
        }

        public void Update(Article article)
        {
            _articleRepo.Update(article);
        }
    }
}
