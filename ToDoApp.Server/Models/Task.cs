using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Server.Models
{
    public class Task
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int task_id { get; set; }
        public int user_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public bool is_completed { get; set; } = false;
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; } = DateTime.Now;
    }
}
