﻿namespace ToDoApp.Server.DTO
{
    public class TaskDTO
    {
        public int user_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public bool is_completed { get; set; }
    }
}
