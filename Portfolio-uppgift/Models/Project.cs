using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;


namespace Portfolio_uppgift.Models
{
       public class Project
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "You must have a Titel to your project!")]
        public string Title { get; set; }
        public string About { get; set; }
        public string Image { get; set; }

        [Url(ErrorMessage = "Please enter a valid url")]
        public string Link { get; set; }
    }

       public class ProjectDBContext : DbContext
       {
           public DbSet<Project> Projects { get; set; }
       }
}