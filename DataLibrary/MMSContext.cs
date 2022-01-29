using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaskDTO = DataLibrary.Models.TaskDTO;

namespace DataLibrary
{
    public partial class MMSContext : IdentityDbContext<User>
    {
        public MMSContext()
        {
        }

        public MMSContext(DbContextOptions<MMSContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Sprint> Sprints { get; set; }
        public virtual DbSet<Models.Task> Tasks { get; set; }
        public virtual DbSet<TaskComment> TaskComments { get; set; }
        public virtual DbSet<TaskSprint> TaskSprints { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserTask> UserTasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                "server=127.0.0.1;uid=root;pwd=;database=test",
                new MySqlServerVersion(new Version(8, 0, 27)));
            }
        }
    }
}
