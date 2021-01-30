using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todos.Models;

namespace Todos.Data
{
    interface ITodoRepoCassandra
    {
        bool SaveChanges();
        IEnumerable<Todo> GetAllTodosByDatetime(DateTime dateTime);
        void CreateTodo(Todo todo);
        void UpdateTodo(Todo todo);
        void DeleteTodo(Todo todo);
    }
}
