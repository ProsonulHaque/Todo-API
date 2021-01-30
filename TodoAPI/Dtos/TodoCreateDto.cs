using System;
using System.ComponentModel.DataAnnotations;

namespace Todos.Dtos
{
    public class TodoCreateDto
    {
        [Required]
        [MaxLength(250)]
        public string TaskTitle { get; set; }

        [Required]
        public DateTime dateTime { get; set; }
    }
}
