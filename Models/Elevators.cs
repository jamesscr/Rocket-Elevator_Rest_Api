using System.Data;
using System.Threading.Tasks;
using MySqlConnector;

namespace Rocket_Elevators_REST_API.Models
{
    public class Elevators
    {
        public int ElevatorId { get; set; }
        public string Status { get; set; }
        internal AppDb Db { get; set; }

        public Elevators()
        {
        }

        internal Elevators(AppDb db)
        {
            Db = db;
        }

        //public async Task InsertAsync()
        //{
        //    using var cmd = Db.Connection.CreateCommand();
        //    cmd.CommandText = @"INSERT INTO `Elevators` (`status`) VALUES (@status);";
        //    BindParams(cmd);
        //    await cmd.ExecuteNonQueryAsync();
        //    Id = (int)cmd.LastInsertedId;
        //}

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `elevators` SET `status` = @status, `updated_at` = NOW() WHERE `Id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        //public async Task DeleteAsync()
        //{
        //    using var cmd = Db.Connection.CreateCommand();
        //    cmd.CommandText = @"DELETE FROM `rocketApp_development` WHERE `Id` = @id;";
        //    BindId(cmd);
        //    await cmd.ExecuteNonQueryAsync();
        //}

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = ElevatorId,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@status",
                DbType = DbType.String,
                Value = Status,
            });
        }
    }
}
