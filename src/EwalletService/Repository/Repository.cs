﻿using Dapper;
using EwalletService.DataAccessLayer;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace EwalletService.Repository
{
    public abstract class Repository
    {
        protected readonly IDatabaseSession _dbSession;

        protected Repository(IDatabaseSession dbSession)
        {
            _dbSession = dbSession;
        }

        protected IEnumerable<T> LoadByStorageProcedure<T>(string spName, object param)
        {
            return _dbSession.Connection.Query<T>(spName, param, commandType: CommandType.StoredProcedure);
        }

        protected Task<IEnumerable<T>> LoadByStorageProcedureAsync<T>(string spName, object param)
        {
            return _dbSession.Connection.QueryAsync<T>(spName, param, commandType: CommandType.StoredProcedure);
        }

        protected DataTable ToSqlIntArray(IEnumerable<int> values)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID");

            if (values != null)
            {
                foreach (int id in values)
                {
                    dataTable.Rows.Add(id);
                }
            }

            return dataTable;
        }

        protected Task<GridReader> LoadMultipleByStorageProcedureAsync(string spName, object param)
        {
            return _dbSession.Connection.QueryMultipleAsync(spName, param, commandType: CommandType.StoredProcedure);
        }

        protected void ExecuteStorageProcedure(string spName, object param)
        {
            _dbSession.Connection.Execute(spName, param: param, commandType: CommandType.StoredProcedure);
        }

        protected async Task ExecuteStorageProcedureAsync(string spName, object param)
        {
            await _dbSession.Connection.ExecuteAsync(spName, param, commandType: CommandType.StoredProcedure);
        }

        protected async Task<T> ExecuteScalarStorageProcedureAsync<T>(string spName, object param)
        {
            var scalar = await _dbSession.Connection.ExecuteScalarAsync(spName, param: param, commandType: CommandType.StoredProcedure);
            return (T)scalar;
        }
    }
}