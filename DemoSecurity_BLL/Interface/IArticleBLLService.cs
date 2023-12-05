using DemoSecurity_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSecurity_BLL.Interface
{
    public interface IArticleBLLService
    {
        Task Create(Article article);
        IEnumerable<Article> GetAll();
        Article GetById(int id);
        void Delete(int id);
        void Update(Article article);
    }
}
