using System;
using System.Data;
using System.Threading.Tasks;
using MySqlConnector;

namespace Rocket_Elevators_REST_API.Models
{
    public class Leads
    {
        public int? LeadId { get; set; }
        public string? Full_Name { get; set; }
        public string? Compagny_Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Project_Name { get; set; }
        public string? Project_Description { get; set; }
        public string? Department { get; set; }
        public string? Message { get; set; }
        public string? File_name { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }

        internal AppDb Db { get; set; }

        public Leads()
        {
        }

        internal Leads(AppDb db)
        {
            Db = db;
        }

        //public async Task InsertAsync()
        //{
        //    using var cmd = Db.Connection.CreateCommand();
        //    cmd.CommandText = @"INSERT INTO `Leads` (`status`) VALUES (@status);";
        //    BindParams(cmd);
        //    await cmd.ExecuteNonQueryAsync();
        //    Id = (int)cmd.LastInsertedId;
        //}

        //public async Task UpdateAsync()
        //{
        //    using var cmd = Db.Connection.CreateCommand();
        //    cmd.CommandText = @"UPDATE `leads` SET `status` = @status WHERE `Id` = @id;";
        //    BindId(cmd);
        //    await cmd.ExecuteNonQueryAsync();
        //}

        //public async Task DeleteAsync()
        //{
        //    using var cmd = Db.Connection.CreateCommand();
        //    cmd.CommandText = @"DELETE FROM `rocketApp_development` WHERE `Id` = @id;";
        //    BindId(cmd);
        //    await cmd.ExecuteNonQueryAsync();
        //}

        //private void BindId(MySqlCommand cmd)
        //{
        //    cmd.Parameters.Add(new MySqlParameter
        //    {
        //        ParameterName = "@id",
        //        DbType = DbType.Int32,
        //        Value = LeadId,
        //    });
        //}
    }
}
