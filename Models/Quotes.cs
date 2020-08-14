using System;
using System.Data;
using System.Threading.Tasks;
using MySqlConnector;

namespace Rocket_Elevators_REST_API.Models
{
    public class Quotes
    {
        public int? QuoteId { get; set; }
        public string? Full_Name { get; set; }
        public string? Phone_Number { get; set; }
        public string? Company_Name { get; set; }
        public string? Building_Type { get; set; }
        public string? Product_Quality { get; set; }
        public int? Nb_Appartement { get; set; }
        public int? Nb_Business { get; set; }
        public int? Nb_Company { get; set; }
        public int? Nb_Floor { get; set; }
        public int? Nb_Basement { get; set; }
        public int? Nb_Cage { get; set; }
        public int? Nb_Parking { get; set; }
        public int? Nb_OccupantPerFloor { get; set; }
        public string? Nb_OperatingHour { get; set; }
        public string? Nb_Ele_Suggested { get; set; }
        public string? Subtotal { get; set; }
        public string? Install_Fee { get; set; }
        public string? Final_Price { get; set; }
        
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }


        internal AppDb Db { get; set; }

        public Quotes()
        {
        }

        internal Quotes(AppDb db)
        {
            Db = db;
        }

        //public async Task InsertAsync()
        //{
        //    using var cmd = Db.Connection.CreateCommand();
        //    cmd.CommandText = @"INSERT INTO `Quotes` (`status`) VALUES (@status);";
        //    BindParams(cmd);
        //    await cmd.ExecuteNonQueryAsync();
        //    Id = (int)cmd.LastInsertedId;
        //}

        // public async Task UpdateAsync()
        // {
        //     using var cmd = Db.Connection.CreateCommand();
        //     cmd.CommandText = @"UPDATE `quotes` SET `status` = @status WHERE `Id` = @id;";
        //     BindParams(cmd);
        //     BindId(cmd);
        //     await cmd.ExecuteNonQueryAsync();
        // }

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
                Value = QuoteId,
            });
        }

        // private void BindParams(MySqlCommand cmd)
        // {
        //     cmd.Parameters.Add(new MySqlParameter
        //     {
        //         ParameterName = "@status",
        //         DbType = DbType.String,
        //         // Value = Status,
        //     });
        // }
    }
}
