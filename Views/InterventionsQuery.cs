using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Threading.Tasks;
using MySqlConnector;
using Rocket_Elevators_REST_API.Models;

namespace Rocket_Elevators_REST_API.Views
{
    public class InterventionsQuery
    {
        public AppDb Db { get; }

        public InterventionsQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<Interventions> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `interventions` WHERE `id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<Interventions>> GetPending()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM interventions WHERE start_of_intervention IS NULL AND Status = 'Pending' ;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        

        //public async Task DeleteAllAsync()
        //{
        //    using var txn = await Db.Connection.BeginTransactionAsync();
        //    using var cmd = Db.Connection.CreateCommand();
        //    cmd.CommandText = @"DELETE FROM `rocketApp_development`";
        //    await cmd.ExecuteNonQueryAsync();
        //    await txn.CommitAsync();
        //}

        private async Task<List<Interventions>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Interventions>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    int? iInterventionID, iAuthor, iCustomerID, iBuildingID, iBatteryID, iColumnID, iElevatorID, iEmployeeID; 
                    string? sResult, sReport, sStatus;
                    DateTime? dstart_of_intervention, dend_of_intervention, dcreated_at, dupdated_at;
                    //check for null value and set a placeholder to avoid errors
                    if (reader.IsDBNull(0)) iInterventionID = null; else iInterventionID = reader.GetInt32(0);
                    if (reader.IsDBNull(1)) iAuthor = null; else iAuthor = reader.GetInt32(1);
                    if (reader.IsDBNull(2)) iCustomerID = null; else iCustomerID = reader.GetInt32(2);
                    if (reader.IsDBNull(3)) iBuildingID = null; else iBuildingID = reader.GetInt32(3);
                    if (reader.IsDBNull(4)) iBatteryID = null; else iBatteryID = reader.GetInt32(4);
                    if (reader.IsDBNull(5)) iColumnID = null; else iColumnID = reader.GetInt32(5);
                    if (reader.IsDBNull(6)) iElevatorID = null; else iElevatorID = reader.GetInt32(6);
                    if (reader.IsDBNull(7)) iEmployeeID = null; else iEmployeeID = reader.GetInt32(7);
                    if (reader.IsDBNull(8)) dstart_of_intervention = null; else dstart_of_intervention = reader.GetDateTime(8);
                    if (reader.IsDBNull(9)) dend_of_intervention = null; else dend_of_intervention = reader.GetDateTime(9);
                    if (reader.IsDBNull(10)) sResult = null; else sResult = reader.GetString(10);
                    if (reader.IsDBNull(11)) sReport = null; else sReport = reader.GetString(11);
                    if (reader.IsDBNull(12)) sStatus = null; else sStatus = reader.GetString(12);
                    if (reader.IsDBNull(13)) dcreated_at = new DateTime(0001, 1, 1); else dcreated_at = reader.GetDateTime(13);
                    if (reader.IsDBNull(14)) dupdated_at = new DateTime(0001, 1, 1); else dupdated_at = reader.GetDateTime(14);

                    var post = new Interventions(Db)
                    {
                        InterventionsID = iInterventionID,
                        Author = iAuthor,
                        CustomerID = iCustomerID,
                        BuildingID = iBuildingID,
                        BatteryID = iBatteryID,
                        ColumnID = iColumnID,
                        ElevatorID = iElevatorID,
                        EmployeeID = iEmployeeID,
                        start_of_intervention = dstart_of_intervention,
                        end_of_intervention = dend_of_intervention,
                        Result = sResult,
                        Report = sReport,
                        Status = sStatus,
                        created_at = dcreated_at,
                        updated_at = dupdated_at,


                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}
