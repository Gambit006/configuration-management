using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManagement.DatabaseConnection.SqlServerConnection
{
    public class SqlServerBaseRepository<T> : IRepository<T> where T : class, new()
    {
        private readonly SqlServerDbContext _context;
        private readonly string _tableName;
        private readonly string[] _columns;

        public SqlServerBaseRepository(SqlServerDbContext context, string tableName, string[] columns)
        {
            _context = context;
            _tableName = tableName;
            _columns = columns;
        }


        public async Task<List<T>> GetAllAsync()
        {
            using (var connection = _context.CreateConnection())
            {
                var query = $"SELECT * FROM {_tableName}";
                var command = new SqlCommand(query, (SqlConnection)connection);
                var dataTable = new DataTable();
                var adapter = new SqlDataAdapter(command);

                await Task.Run(() => adapter.Fill(dataTable));
                return DataTableToList(dataTable);
            }
        }

        public async Task<T> GetByIdAsync(string id)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = $"SELECT * FROM {_tableName} WHERE {_columns[0]} = @id";
                var command = new SqlCommand(query, (SqlConnection)connection);
                command.Parameters.AddWithValue("@id", id);
                var dataTable = new DataTable();
                var adapter = new SqlDataAdapter(command);

                await Task.Run(() => adapter.Fill(dataTable));
                return dataTable.Rows.Count == 0 ? null : DataRowToEntity(dataTable.Rows[0]);
            }
        }

        public async Task AddAsync(T entity)
        {
            using (var connection = _context.CreateConnection())
            {
                var columns = string.Join(", ", _columns.Skip(1));
                var parameters = string.Join(", ", _columns.Skip(1).Select(c => "@" + c));
                var values = _columns.Skip(1).Select(c => new SqlParameter("@" + c, GetPropertyValue(entity, c))).ToArray();
                var query = $"INSERT INTO {_tableName} ({columns}) VALUES ({parameters})";
                var command = new SqlCommand(query, (SqlConnection)connection);
                command.Parameters.AddRange(values);

                connection.Open();
                await Task.Run(() => command.ExecuteNonQuery());
            }
        }

        public async Task UpdateAsync(string id, T entity)
        {
            using (var connection = _context.CreateConnection())
            {
                var updates = string.Join(", ", _columns.Skip(1).Select(c => $"{c} = @{c}"));
                var values = _columns.Select(c => new SqlParameter("@" + c, GetPropertyValue(entity, c))).ToArray();
                var query = $"UPDATE {_tableName} SET {updates} WHERE {_columns[0]} = @{_columns[0]}";
                var command = new SqlCommand(query, (SqlConnection)connection);
                command.Parameters.AddRange(values);

                connection.Open();
                await Task.Run(() => command.ExecuteNonQuery());
            }
        }

        public async Task DeleteAsync(string id)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = $"DELETE FROM {_tableName} WHERE {_columns[0]} = @id";
                var command = new SqlCommand(query, (SqlConnection)connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                await Task.Run(() => command.ExecuteNonQuery());
            }
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        #region private functions
        private List<T> DataTableToList(DataTable dataTable)
        {
            return dataTable.AsEnumerable().Select(DataRowToEntity).ToList();
        }

        private T DataRowToEntity(DataRow row)
        {
            var entity = new T();
            foreach (var column in _columns)
            {
                typeof(T).GetProperty(column).SetValue(entity, row[column]);
            }
            return entity;
        }

        private object GetPropertyValue(T entity, string propertyName)
        {
            return typeof(T).GetProperty(propertyName).GetValue(entity);
        }
        #endregion
    }
}
