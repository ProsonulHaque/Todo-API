using Todos.Models;
using System.Collections.Generic;

namespace Todos.Data
{
    public interface ITodoRepo
    {
        bool SaveChanges();
        IEnumerable<Todo> GetAllTodos();
        Todo GetTodoByID(int id);
        void CreateTodo(Todo todo);
        void UpdateTodo(Todo todo);
        void DeleteTodo(Todo todo);
    }
}