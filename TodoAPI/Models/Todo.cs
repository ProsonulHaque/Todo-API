using System;
using System.ComponentModel.DataAnnotations;
using Cassandra;

namespace Todos.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(250)]
        public string TaskTitle { get; set; }
        
        [Required]
        public DateTime datetime { get; set; }
    }
}
