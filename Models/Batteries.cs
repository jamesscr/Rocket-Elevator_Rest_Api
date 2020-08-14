using System.Data;
using System.Threading.Tasks;
using MySqlConnector;

namespace Rocket_Elevators_REST_API.Models
{
    public class Batteries
    {
        public int BatteryId { get; set; }
        public string Status { get; set; }

        internal AppDb Db { get; set; }

        public Batteries()
        {
        }

        internal Batteries(AppDb db)
        {
            Db = db;
        }

        //public async Task InsertAsync()
        //{
        //    using var cmd = Db.Connection.CreateCommand();
        //    cmd.CommandText = @"INSERT INTO `batteries` (`status`) VALUES (@status);";
        //    BindParams(cmd);
        //    await cmd.ExecuteNonQueryAsync();
        //    Id = (int)cmd.LastInsertedId;
        //}

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `batteries` SET `status` = @status, `updated_at` = NOW() WHERE `Id` = @id;";
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
                Value = BatteryId,
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
