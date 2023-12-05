using Dapper;
using DemoSecurity_DAL.Entities;
using DemoSecurity_DAL.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSecurity_DAL.Services
{
    public class ArticleService : IArticleRepository
    {
        private readonly SqlConnection _connection;
        

        public ArticleService(SqlConnection connection)
        {
            _connection = connection;
       
        }

        public IEnumerable<Article> GetAll()
        {
            string sql = "SELECT * FROM Article";
            return _connection.Query<Article>(sql);
        }

        public Article GetById(int id)
        {
            string sql = "SELECT * FROM Article WHERE Id = @id";
            return _connection.QueryFirst<Article>(sql, new { id });
        }

        public async Task Create(Article article)
        {
            string sql = "INSERT INTO Article (Nom, Prix, Description, Categorie) " +
                "VALUES (@Nom, @Prix, @Description, @Categorie)";
            _connection.Execute(sql, article);
           
        }

        public void Delete(int id)
        {
            _connection.Execute("DELETE FROM Article WHERE Id = @id", new { id });
        }

        public void Update(Article article)
        {
            string sql = "UPDATE Article SET Nom = @Nom, Categorie = @Categorie, " +
                "Prix = @Prix, Description = @Description " +
                "WHERE Id = @Id";

            _connection.Execute(sql, article);
        }
    }
}
