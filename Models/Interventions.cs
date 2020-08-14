using System;
using System.Data;
using System.Threading.Tasks;
using MySqlConnector;

namespace Rocket_Elevators_REST_API.Models
{
    public class Interventions
    {
        public int? InterventionsID { get; set; }
        public int? Author { get; set; }
        public int? CustomerID { get; set; }
        public int? BuildingID { get; set; }
        public int? BatteryID { get; set; }
        public int? ColumnID { get; set; }
        public int? ElevatorID { get; set; }
        public int? EmployeeID { get; set; }
        public DateTime? start_of_intervention { get; set; }
        public DateTime? end_of_intervention { get; set; }
        public string? Result { get; set; }
        public string? Report { get; set; }
        public string? Status { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }

        internal AppDb Db { get; set; }

        public Interventions()
        {
        }

        internal Interventions(AppDb db)
        {
            Db = db;
        }

        public async Task UpdateInProgressAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `interventions` SET `Status` = 'InProgress', start_of_intervention = NOW(), `updated_at` = NOW() WHERE `Id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }
        public async Task UpdateCompletedAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `interventions` SET `Status` = 'Completed', end_of_intervention = NOW(), `updated_at` = NOW() WHERE `Id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }


        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = InterventionsID,
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
