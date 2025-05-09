using TaskManager.Models;

namespace TaskManager.Interfaces
{
    public interface ITaskService
    {
        List<Tasks> GetAll();
        Tasks? GetById(int id);
        void Add(Tasks task);
        void Update(Tasks task);
        void Delete(int id);
    }
}