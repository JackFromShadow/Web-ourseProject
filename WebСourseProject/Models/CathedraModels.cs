using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Web.Mvc;

namespace WebСourseProject.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int number { get; set; }

        public string letter { get; set; }//Буква группы? Символ

        public string Group_Name { get; set; }
        public int Count_Of_Student { get; set; }                 
        public ICollection<Student> Students { get; set; }
        public ICollection<Timetable> TimeTables { get; set; }
        public int? TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; }
    }

    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FathersName { get; set; }
        [Required]
        public int GroupId { get; set; }
        [Required]
        public string Email { get; set; }
        [ForeignKey("GroupId")]
        public Group Group { get; set; }

        public string imgPath { get; set; }
    }

    public class Graduate
    {   //Выпускник
        [Key]
        [ForeignKey("ScienceWork")]
        public int Id { get; set; }
        [Required]
        public string FIO { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }

        public ScienceWork ScienceWork { get; set; }

    }

    public class ScienceWork
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Header { get; set; }
        [Required]
        [MaxLength(150)]
        public string ScienceDirection { get; set; }

        public Graduate Graduate { get; set; }
    }

    public class Teacher
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FathersName { get; set; }
        [Required]
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string Information { get; set; }
        [Required]
        public string Qualification { get; set; }   // типа вместо Degree. Учёная степень/ранг
        public ICollection<Group> Groups { get; set; }
        public ICollection<Graduate> Graduates { get; set; }
      
        public string imgPath { get; set; }
    }

    public class Lesson
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int number { get; set; }
        [Required]
        public int day { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int TimetableId { get; set; }

        public string TeachersName { get; set; }
        [ForeignKey("TimetableId")]
        public Timetable timetable { get; set; }
    }

    public class Topic
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Header { get; set; }
        [Required]
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string Content { get; set; }

        public DateTime published { get; set; }

        public string Type { get; set; }//Тип - новость/статья/объявление 

        public string imgPath { get; set; }//путь к изображению-заголовку.Это надо.

        public IEnumerable<string> Items { get; set; }
    }


    public class Timetable
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int GroupId { get; set; }

        [Required]
        public int PeriodId { get; set; }

        [ForeignKey("PeriodId")]
        public Period Period { get; set; }

        [ForeignKey("GroupId")]
        public Group Group { get; set; }

  //      [ForeignKey("PeriodId")]
 //       public Period Period { get; set; }

        public IEnumerable<Lesson> Lessons;
    }

    public class LessonRequest
    {
        public string name { get; set; }
        public string teacher { get; set; }
    }

    public class Period
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime begin { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime end { get; set; }
        public ICollection<Timetable> TimeTables { get; set; }
    }
    
    public class SiteContext : DbContext
    {
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
    }
}