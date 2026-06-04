using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assignment.Repository.Data.Configurations
{
    public class TaskModelConfiguration : IEntityTypeConfiguration<TaskModel>
    {
        public void Configure(EntityTypeBuilder<TaskModel> builder)
        {
            builder.HasOne(t => t.User)
                   .WithMany(u => u.TasksModel)
                   .HasForeignKey(t => t.UserId);

        }
    }
}
