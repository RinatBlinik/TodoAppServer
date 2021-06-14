using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAppServer.Models.Entities
{
    public record TodoList(
        int Id,
        string Caption,
        string Description,
        string Icon,
        string Color);
}
