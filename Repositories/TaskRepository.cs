using System.Collections.Generic;
using System.Threading.Tasks;
using AgileTaskMaster.Models;
using MongoDB.Driver;

namespace AgileTaskMaster.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IMongoCollection<Models.Task> _taskCollection;

        public TaskRepository(IMongoDatabase database)
        {
            _taskCollection = database.GetCollection<Models.Task>("tasks");
        }

        public async Task<IEnumerable<Models.Task>> GetAllTasks()
        {
            return await _taskCollection.Find(task => true).ToListAsync();
        }

        public async Task<Models.Task> GetTaskById(string taskId)
        {
            return await _taskCollection.Find(task => task.Id == taskId).FirstOrDefaultAsync();
        }

        public async Task<Models.Task> CreateTask(Models.Task task)
        {
            await _taskCollection.InsertOneAsync(task);
            return task;
        }

        public async Task<Models.Task> UpdateTask(Models.Task task)
        {
            var updateResult = await _taskCollection.ReplaceOneAsync(t => t.Id == task.Id, task);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0 ? task : null;
        }

        public async Task<bool> DeleteTask(string taskId)
        {
            var deleteResult = await _taskCollection.DeleteOneAsync(task => task.Id == taskId);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
        public async Task<Models.Task> UpdateTaskPriority(string taskId, TaskPriority priority)
        {
            var filter = Builders<Models.Task>.Filter.Eq(t => t.Id, taskId);
            var update = Builders<Models.Task>.Update.Set(t => t.Priority, priority);
            var options = new FindOneAndUpdateOptions<Models.Task> { ReturnDocument = ReturnDocument.After };

            return await _taskCollection.FindOneAndUpdateAsync(filter, update, options);
        }

    }
}
