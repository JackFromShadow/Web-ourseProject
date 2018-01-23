using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebСourseProject.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {   /*
        public DbSet<Group> Groups { get; set; }//группы
        public DbSet<Student> Students { get; set; }//студенты
        public DbSet<Topic> Topics { get; set; }//статьи
        public DbSet<Period> Periods { get; set; }//периоды
        public DbSet<Timetable> Timetables { get; set; }//Расписания
        public DbSet<Teacher> Teachers { get; set; }//Преподаватели
        public DbSet<Lesson> Lessons { get; set; }//Занятия

        public SiteContext()
        : base("name=SiteContext")
        {
            Database.SetInitializer<SiteContext>(new CreateDatabaseIfNotExists<SiteContext>());
        }
        */
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}