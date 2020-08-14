using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;
using Rocket_Elevators_REST_API.Models;

namespace Rocket_Elevators_REST_API.Views
{
    public class ListElevatorsQuery
    {
        public AppDb Db { get; }

        public ListElevatorsQuery(AppDb db)
        {
            Db = db;
        }

        //public async Task<ListElevators> FindOneAsync(int id)
        //{
        //    using var cmd = Db.Connection.CreateCommand();
        //    cmd.CommandText = @"SELECT `Id`, `status` FROM `elevators` WHERE `Id` = @id";
        //    cmd.Parameters.Add(new MySqlParameter
        //    {
        //        ParameterName = "@id",
        //        DbType = DbType.Int32,
        //        Value = id,
        //    });
        //    var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
        //    return result.Count > 0 ? result[0] : null;
        //}

        //public async Task<List<ListElevators>> LatestPostsAsync()
        //{
        //    using var cmd = Db.Connection.CreateCommand();
        //    cmd.CommandText = @"SELECT `Id`, `status` FROM `elevators` ORDER BY `Id` DESC LIMIT 10;";
        //    return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        //}
        public async Task<List<ListElevators>> InactiveElevatorsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `elevators` WHERE `status` != 'active' ;";
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

        private async Task<List<ListElevators>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<ListElevators>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    int? iElevatorId, iColumn_Id;
                    Int64? iSerial_Number;
                    string? sModel, sElevator_Type, sStatus, sCertificat_Of_Inspection, sInformations, sNotes;
                    DateTime? dCommission_Date, dDate_Of_Last_Inspection, dCreated_at, dUpdated_at;
                    //check for null value and set a placeholder to avoid errors
                    if (reader.IsDBNull(0)) iElevatorId = 0; else iElevatorId = reader.GetInt32(0);
                    if (reader.IsDBNull(1)) iSerial_Number = 0; else iSerial_Number = reader.GetInt64(1);
                    if (reader.IsDBNull(2)) sModel = ""; else sModel = reader.GetString(2);
                    if (reader.IsDBNull(3)) sElevator_Type = ""; else sElevator_Type = reader.GetString(3);
                    if (reader.IsDBNull(4)) sStatus = ""; else sStatus = reader.GetString(4);
                    if (reader.IsDBNull(5)) dCommission_Date = new DateTime(0001, 1, 1); else dCommission_Date = reader.GetDateTime(5);
                    if (reader.IsDBNull(6)) dDate_Of_Last_Inspection = new DateTime(0001, 1, 1); else dDate_Of_Last_Inspection = reader.GetDateTime(6);
                    if (reader.IsDBNull(7)) sCertificat_Of_Inspection = ""; else sCertificat_Of_Inspection = reader.GetString(7);
                    if (reader.IsDBNull(8)) sInformations = ""; else sInformations = reader.GetString(8);
                    if (reader.IsDBNull(9)) sNotes = ""; else sNotes = reader.GetString(9);
                    if (reader.IsDBNull(10)) iColumn_Id = 0; else iColumn_Id = reader.GetInt32(10);
                    if (reader.IsDBNull(11)) dCreated_at = new DateTime(0001, 1, 1); else dCreated_at = reader.GetDateTime(11);
                    if (reader.IsDBNull(12)) dUpdated_at = new DateTime(0001, 1, 1); else dUpdated_at = reader.GetDateTime(12);

                    var post = new ListElevators(Db)
                    {
                        ElevatorId = iElevatorId,
                        Serial_Number = iSerial_Number,
                        Model = sModel,
                        Elevator_Type = sElevator_Type,
                        Status = sStatus,
                        Commission_Date = dCommission_Date,
                        Date_Of_Last_Inspection = dDate_Of_Last_Inspection,
                        Certificat_Of_Inspection = sCertificat_Of_Inspection,
                        Informations = sInformations,
                        Notes = sNotes,
                        Column_Id = iColumn_Id,
                        Created_at = dCreated_at,
                        Updated_at = dUpdated_at,
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}
