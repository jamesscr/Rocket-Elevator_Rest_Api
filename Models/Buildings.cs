using System;
using System.Data;
using System.Threading.Tasks;
using MySqlConnector;

namespace Rocket_Elevators_REST_API.Models
{
    public class Buildings
    {
        public int? BuildingId { get; set; }
        public string? Admin_Full_Name { get; set; }
        public string? Admin_Phone { get; set; }
        public string? Admin_Email { get; set; }
        public string? Full_Name_STA { get; set; }
        public string? Phone_TA { get; set; }
        public string? Email_TA { get; set; }
        public int? Address_Id { get; set; }
        public int? Customer_Id { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }



        internal AppDb Db { get; set; }

        public Buildings()
        {
        }

        internal Buildings(AppDb db)
        {
            Db = db;
        }

        //public async Task InsertAsync()
        //{
        //    using var cmd = Db.Connection.CreateCommand();
        //    cmd.CommandText = @"INSERT INTO `Buildings` (`status`) VALUES (@status);";
        //    BindParams(cmd);
        //    await cmd.ExecuteNonQueryAsync();
        //    Id = (int)cmd.LastInsertedId;
        //}

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `buildings` SET `status` = @status, `updated_at` = NOW() WHERE `Id` = @id;";
            //BindParams(cmd);
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
                Value = BuildingId,
            });
        }

        //private void BindParams(MySqlCommand cmd)
        //{
        //    cmd.Parameters.Add(new MySqlParameter
        //    {
        //        ParameterName = "@status",
        //        DbType = DbType.String,
        //        Value = Status,
        //    });
        //}
    }
}
