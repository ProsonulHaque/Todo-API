using System;

namespace Todos.Dtos
{
    public class TodoReadDtoCassandra
    {
        public string TaskTitle { get; set; }

        public string DateTime { get; set; }
    }
}
