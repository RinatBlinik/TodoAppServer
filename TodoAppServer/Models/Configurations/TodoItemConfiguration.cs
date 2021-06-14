using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using TodoAppServer.Models.Entities;

namespace TodoAppServer.Models.Configurations
{
    public class TodoItemConfiguration: IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder.ToTable("TodoItems");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Caption)
                .IsRequired();

            builder.Property(x => x.ListId)
                .IsRequired();

            builder.Property(x => x.IsCompleted)
                .IsRequired();
        }
    }
}
