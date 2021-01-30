using Todos.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Todos.Data
{
    public class SqlTodoRepo : ITodoRepo
    {
        private readonly TodoContext _context;

        public SqlTodoRepo(TodoContext context)
        {
            _context = context;
        }

        public void CreateTodo(Todo todo)
        {
            if(todo==null)
            {
                throw new ArgumentNullException(nameof(todo));
            }
            _context.Todos.Add(todo);
        }

        public void DeleteTodo(Todo todo)
        {
            if (todo == null)
            {
                throw new ArgumentNullException(nameof(todo));
            }
            _context.Todos.Remove(todo);
        }

        public IEnumerable<Todo> GetAllTodos()
        {
            return _context.Todos.ToList();
        }

        public Todo GetTodoByID(int id)
        {
            return _context.Todos.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges()>=0);
        }

        public void UpdateTodo(Todo todo)
        {
            //Nothing
        }
    }
}