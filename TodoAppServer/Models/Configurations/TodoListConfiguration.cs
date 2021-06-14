using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using TodoAppServer.Models.Entities;

namespace TodoAppServer.Models.Configurations
{
    public class TodoListConfiguration: IEntityTypeConfiguration<TodoList>
    {
        public void Configure(EntityTypeBuilder<TodoList> builder)
        {
            builder.ToTable("TodoLists");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Caption)
                .IsRequired();

            builder.Property(x => x.Description)
                .IsRequired();

            builder.Property(x => x.Icon)
                .IsRequired()
                .HasMaxLength(30);
            builder.Property(x => x.Color)
               .IsRequired()
                .HasMaxLength(30);
        }
    }
}
