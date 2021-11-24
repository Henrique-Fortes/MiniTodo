using Microsoft.EntityFrameworkCore;

namespace MiniTodo.Data
{
    public class AppDbContext : DbContext //Representacao do Banco de dados em memoria
    {
        public DbSet<Todo> Todos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("DataSource=app.db;Cache=Shared");
    }
}
