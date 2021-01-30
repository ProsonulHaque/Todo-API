using System;
using System.ComponentModel.DataAnnotations;



namespace Todos.Dtos
{
    public class TodoPartialUpdateDto
    {
        [Required]
        public DateTime dateTime { get; set; }
    }
}
