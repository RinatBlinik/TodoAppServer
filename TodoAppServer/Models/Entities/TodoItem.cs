using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAppServer.Models.Entities
{
    public record TodoItem(
        int Id,
            string Caption,
            int ListId,
            bool IsCompleted);
}