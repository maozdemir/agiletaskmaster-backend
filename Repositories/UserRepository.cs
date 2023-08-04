using System.Collections.Generic;
using System.Threading.Tasks;
using AgileTaskMaster.Models;
using MongoDB.Driver;

namespace AgileTaskMaster.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserRepository(IMongoDatabase database)
        {
            _userCollection = database.GetCollection<User>("users");
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userCollection.Find(user => true).ToListAsync();
        }

        public async Task<User> GetUserById(string userId)
        {
            return await _userCollection.Find(user => user.Id == userId).FirstOrDefaultAsync();
        }

        public async Task<User> CreateUser(User user)
        {
            await _userCollection.InsertOneAsync(user);
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            var updateResult = await _userCollection.ReplaceOneAsync(u => u.Id == user.Id, user);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0 ? user : null;
        }

        public async Task<bool> DeleteUser(string userId)
        {
            var deleteResult = await _userCollection.DeleteOneAsync(user => user.Id == userId);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userCollection.Find(user => user.Email == email).FirstOrDefaultAsync();
        }
    }
}
