using AutoMapper;
using Todos.Dtos;
using Todos.Models;

namespace Todos.Profiles
{
    public class TodosProfile:Profile
    {
        public TodosProfile()
        {
            //Source -> Target
            CreateMap<Todo, TodoReadDto>();
            CreateMap<TodoCreateDto, Todo>();
            
            CreateMap<TodoUpdateDto, Todo>();
            CreateMap<Todo, TodoUpdateDto>();
            
            CreateMap<TodoPartialUpdateDto, Todo>();
            CreateMap<Todo, TodoPartialUpdateDto>();

            CreateMap<TodoCassandra, TodoReadDtoCassandra>();
            CreateMap<TodoReadDtoCassandra, TodoCassandra>();
        }
    }
}
