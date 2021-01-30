using System.ComponentModel.DataAnnotations;

namespace Todos.Models
{
    public class TodoCassandra
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(250)]
        public string TaskTitle { get; set; }
        
        [Required]
        public string Datetime { get; set; }
    }
}
