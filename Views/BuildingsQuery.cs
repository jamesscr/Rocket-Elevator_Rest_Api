using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;
using Rocket_Elevators_REST_API.Models;

namespace Rocket_Elevators_REST_API.Views
{
    public class BuildingsQuery
    {
        public AppDb Db { get; }

        public BuildingsQuery(AppDb db)
        {
            Db = db;
        }

        /*public async Task<Buildings> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`,  FROM `buildings` WHERE  `Id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        } */

        public async Task<List<Buildings>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT DISTINCT buildings.Id, buildings.admin_full_name, buildings.admin_phone, buildings.admin_email, buildings.full_name_STA, buildings.phone_TA, buildings.email_TA, buildings.address_id, buildings.customer_id, buildings.created_at, buildings.updated_at FROM `buildings` 
                JOIN `batteries`
                ON buildings.id = batteries.building_id
                JOIN `columns`
                ON batteries.id = columns.battery_id
                JOIN `elevators`
                ON columns.id = elevators.column_id
                WHERE batteries.status = 'intervention' OR columns.status = 'intervention' OR elevators.status = 'intervention';";
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

        private async Task<List<Buildings>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Buildings>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    int? iBuildingId, iAddress_Id, iCustomer_Id;
                    string? sAdmin_Full_Name, sAdmin_Phone, sAdmin_Email, sFull_Name_STA, sPhone_TA, sEmail_TA;
                    DateTime? dCreated_at, dUpdated_at;
                    //check for null value and set a placeholder to avoid errors
                    if (reader.IsDBNull(0)) iBuildingId = 0; else iBuildingId = reader.GetInt32(0);
                    if (reader.IsDBNull(1)) sAdmin_Full_Name = ""; else sAdmin_Full_Name = reader.GetString(1);
                    if (reader.IsDBNull(2)) sAdmin_Phone = ""; else sAdmin_Phone = reader.GetString(2);
                    if (reader.IsDBNull(3)) sAdmin_Email = ""; else sAdmin_Email = reader.GetString(3);
                    if (reader.IsDBNull(4)) sFull_Name_STA = ""; else sFull_Name_STA = reader.GetString(4);
                    if (reader.IsDBNull(5)) sPhone_TA = ""; else sPhone_TA = reader.GetString(5);
                    if (reader.IsDBNull(6)) sEmail_TA = ""; else sEmail_TA = reader.GetString(6);
                    if (reader.IsDBNull(7)) iAddress_Id = 0; else iAddress_Id = reader.GetInt32(7);
                    if (reader.IsDBNull(8)) iCustomer_Id = 0; else iCustomer_Id = reader.GetInt32(8);
                    if (reader.IsDBNull(9)) dCreated_at = new DateTime(0001, 1, 1); else dCreated_at = reader.GetDateTime(9);
                    if (reader.IsDBNull(10)) dUpdated_at = new DateTime(0001, 1, 1); else dUpdated_at = reader.GetDateTime(10);
                    var post = new Buildings(Db)
                    {
                        BuildingId = iBuildingId,
                        Admin_Full_Name = sAdmin_Full_Name,
                        Admin_Phone = sAdmin_Phone,
                        Admin_Email = sAdmin_Email,
                        Full_Name_STA = sFull_Name_STA,
                        Phone_TA = sPhone_TA,
                        Email_TA = sEmail_TA,
                        Address_Id = iAddress_Id,
                        Customer_Id = iCustomer_Id,
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
