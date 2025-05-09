using TaskManager.Models;
using System.Collections.Generic;
using System.Linq;

namespace TaskManager.Services
{
    public class TaskService
    {
        private static List<Tasks> _tasks = new List<Tasks>
        {
            new Tasks { Id = 1, Title = "Learn MVC", Description = "Study ASP.NET Core MVC", IsCompleted = false, CreatedAt = DateTime.Now.AddDays(-2) },
            new Tasks { Id = 2, Title = "Set up Jenkins", Description = "Configure Jenkins CI/CD", IsCompleted = true, CreatedAt = DateTime.Now.AddDays(-1) },
            new Tasks { Id = 3, Title = "Prepare Demo", Description = "Create slides for mentors", IsCompleted = false, CreatedAt = DateTime.Now }
        };

        private static int _nextId = 4;

        public List<Tasks> GetAll() => _tasks;

        public Tasks GetById(int id) => _tasks.FirstOrDefault(t => t.Id == id);

        public void Add(Tasks task)
        {
            task.Id = _nextId++;
            task.CreatedAt = DateTime.Now;
            _tasks.Add(task);
        }

        public void Update(Tasks task)
        {
            var existing = GetById(task.Id);
            if (existing != null)
            {
                existing.Title = task.Title;
                existing.Description = task.Description;
                existing.IsCompleted = task.IsCompleted;
            }
        }

        public void Delete(int id)
        {
            var task = GetById(id);
            if (task != null)
            {
                _tasks.Remove(task);
            }
        }
    }
}