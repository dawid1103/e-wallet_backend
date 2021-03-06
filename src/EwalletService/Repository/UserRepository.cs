﻿using EwalletCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EwalletService.DataAccessLayer;
using EwalletCommon.Utils;
using System.Data.SqlClient;

namespace EwalletService.Repository
{
    public interface IUserRepository : IRepository<UserDTO>
    {
    }
    public class UserRepository : Repository, IUserRepository
    {
        public UserRepository(IDatabaseSession dbSession) : base(dbSession)
        {
        }

        public async Task<int> CreateAsync(UserDTO user)
        {
            try
            {
                IEnumerable<int> results = await base.LoadByStorageProcedureAsync<int>("dbo.UserCreate", new
                {
                    email = user.Email,
                    passwordHash = user.PasswordHash,
                    salt = user.Salt,
                    isActive = user.IsActive,
                    role = user.Role
                });

                return results.FirstOrDefault();
            }
            catch (SqlException ex)
            {
                if (ex.Message.IndexOf("Duplicate username", StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    throw new DuplicateUsernameException();
                }

                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            await base.ExecuteStorageProcedureAsync("dbo.UserDelete", new
            {
                id = id
            });
        }

        public Task EditAsync(UserDTO entity)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> GetAsync(int id)
        {
            IEnumerable<UserDTO> result = await base.LoadByStorageProcedureAsync<UserDTO>("dbo.UserGet", new
            {
                id = id
            });

            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            return await base.LoadByStorageProcedureAsync<UserDTO>("dbo.UserGetAll", null);
        }
    }
}
