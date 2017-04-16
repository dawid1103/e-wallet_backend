using EwalletCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EwalletServices.DataAccessLayer;

namespace EwalletServices.Repository
{
    public interface IUserRepository : IRepository<UserDTO>
    {

    }
    public class UserRepository : Repository, IUserRepository
    {
        public UserRepository(IDatabaseSession dbSession) : base(dbSession)
        {
        }

        public async Task<int> AddAsync(UserDTO user)
        {
            IEnumerable<int> results = await base.LoadByStorageProcedureAsync<int>("dbo.UserCreate", new
            {
                email = user.Email,
                passwordHash = user.PasswordHash,
                passwordSalt = user.PasswordSalt,
                isActive = user.IsActive,
                role = user.Role
            });

            return results.FirstOrDefault();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditAsync(UserDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDTO>> ListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
