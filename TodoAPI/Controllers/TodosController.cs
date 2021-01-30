using AutoMapper;
using Todos.Data;
using Todos.Dtos;
using Todos.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

/*--------------------------------------------------------------------------------*/
/* USE DATETIME FORMAT LIKE THIS: "15-Jan-21 2:00:00 PM" OR "2021-02-28T19:00:00" */
/*--------------------------------------------------------------------------------*/

namespace Todos.Controllers
{
    [Route("api/todos")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        //SQL
        private readonly ITodoRepo _repository;
        private readonly IMapper _mapper;

        public TodosController(ITodoRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //GET api/todos
        [HttpGet]
        public ActionResult<IEnumerable<TodoReadDto>> GetAllTodos()
        {
            var todoItems = _repository.GetAllTodos();
            return Ok(_mapper.Map<IEnumerable<TodoReadDto>>(todoItems));
        }

        //GET api/todos/{id}
        [HttpGet("{id}", Name = "GetTodoById")]
        public ActionResult<TodoReadDto> GetTodoById(int id)
        {
            var todoItem = _repository.GetTodoByID(id);
            if (todoItem == null) return NotFound();
            return Ok(_mapper.Map<TodoReadDto>(todoItem));
        }

        //Post api/todos
        [HttpPost]
        public ActionResult<TodoReadDto> CreateTodo(TodoCreateDto todoCreateDto)
        {
            var todoModel = _mapper.Map<Todo>(todoCreateDto);
            _repository.CreateTodo(todoModel);
            _repository.SaveChanges();

            CassandraTodoContext _cassandraContext = new CassandraTodoContext();
            _cassandraContext.PostTodo(todoModel);

            var todoReadDto = _mapper.Map<TodoReadDto>(todoModel);

            return CreatedAtRoute(nameof(GetTodoById), new {Id = todoReadDto.Id }, todoReadDto);
        }

        //Put api/todos/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateTodo(int id, TodoUpdateDto todoUpdateDto)
        {
            var todoModelFromRepo = _repository.GetTodoByID(id);
            if (todoModelFromRepo == null)
            {
                return NotFound();
            }

            CassandraTodoContext _cassandraContext = new CassandraTodoContext();
            _cassandraContext.DeleteTodo(todoModelFromRepo);

            _mapper.Map(todoUpdateDto, todoModelFromRepo);
            _repository.UpdateTodo(todoModelFromRepo);
            _repository.SaveChanges();

            _cassandraContext = new CassandraTodoContext();
            _cassandraContext.PostTodo(todoModelFromRepo);

            return NoContent();
        }

        //Patch api/todos/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialTodoUpdate(int id, JsonPatchDocument<TodoPartialUpdateDto> patchDoc)
        {
            var todoModelFromRepo = _repository.GetTodoByID(id);
            if (todoModelFromRepo == null)
            {
                return NotFound();
            }

            CassandraTodoContext _cassandraContext = new CassandraTodoContext();
            _cassandraContext.DeleteTodo(todoModelFromRepo);

            var todoToPatch = _mapper.Map<TodoPartialUpdateDto>(todoModelFromRepo);
            patchDoc.ApplyTo(todoToPatch, ModelState);

            if (!TryValidateModel(todoToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(todoToPatch, todoModelFromRepo);
            _repository.UpdateTodo(todoModelFromRepo);
            _repository.SaveChanges();
            
            _cassandraContext = new CassandraTodoContext();
            _cassandraContext.UpdateTodo(todoToPatch.dateTime, todoModelFromRepo);

            return NoContent();
        }

        //Delete api/todos/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteTodo(int id)
        {
            var todoModelFromRepo = _repository.GetTodoByID(id);
            if (todoModelFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteTodo(todoModelFromRepo);
            _repository.SaveChanges();

            CassandraTodoContext _cassandraContext = new CassandraTodoContext();
            _cassandraContext.DeleteTodo(todoModelFromRepo);

            return NoContent();
        }

        //GET api/todos/datetime=searchValue
        [HttpGet("datetime={dateTime}")]
        public ActionResult<IEnumerable<TodoReadDtoCassandra>> GetTodoByDatetime(string dateTime)
        {
            CassandraTodoContext _cassandraContext = new CassandraTodoContext();
            var todoItems = _cassandraContext.GetElementByDatetime(dateTime);
            var response = _mapper.Map<IEnumerable<TodoReadDtoCassandra>>(todoItems);

            if (!response.Any()) return NotFound();
            return Ok(response);
        }   
    }
}
