using System;

namespace Todos.Dtos
{
    public class TodoReadDto
    {
        public int Id { get; set; }

        public string TaskTitle { get; set; }

        public DateTime dateTime { get; set; }
    }
}
