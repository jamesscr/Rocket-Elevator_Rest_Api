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
    public class LeadsQuery
    {
        public AppDb Db { get; }

        public LeadsQuery(AppDb db)
        {
            Db = db;
        }

        //public async Task<Leads> FindOneAsync(int id)
        //{
        //    using var cmd = Db.Connection.CreateCommand();
        //    cmd.CommandText = @"SELECT `Id`, `status` FROM `leads` WHERE `Id` = @id";
        //    cmd.Parameters.Add(new MySqlParameter
        //    {
        //        ParameterName = "@id",
        //        DbType = DbType.Int32,
        //        Value = id,
        //    });
        //    var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
        //    return result.Count > 0 ? result[0] : null;
        //}

        public async Task<List<Leads>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT DISTINCT leads.Id, leads.Full_Name, leads.Compagny_Name, leads.Email, leads.Phone, leads.Project_Name, leads.Project_Description, leads.Department, leads.Message, leads.File_name, leads.created_at, leads.updated_at  FROM leads JOIN customers ON customers.company_contact_email != leads.Email WHERE DATEDIFF(NOW(), leads.created_at) <= 30  ORDER BY leads.created_at ;";
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

        private async Task<List<Leads>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Leads>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    int? iLeadId;
                    string? sFull_Name,sCompagny_Name,sEmail,sPhone,sProject_Name,sProject_Description,sDepartment,sMessage,sFile_Name;
                    DateTime? dCreated_at,dUpdated_at;
                    //check for null value and set a placeholder to avoid errors
                    if (reader.IsDBNull(0)) iLeadId = 0; else iLeadId = reader.GetInt32(0);
                    if (reader.IsDBNull(1)) sFull_Name = ""; else sFull_Name = reader.GetString(1);
                    if (reader.IsDBNull(2)) sCompagny_Name = ""; else sCompagny_Name = reader.GetString(2);
                    if (reader.IsDBNull(3)) sEmail = ""; else sEmail = reader.GetString(3);
                    if (reader.IsDBNull(4)) sPhone = ""; else sPhone = reader.GetString(4);
                    if (reader.IsDBNull(5)) sProject_Name = ""; else sProject_Name = reader.GetString(5);
                    if (reader.IsDBNull(6)) sProject_Description = ""; else sProject_Description = reader.GetString(6);
                    if (reader.IsDBNull(7)) sDepartment = ""; else sDepartment = reader.GetString(7);
                    if (reader.IsDBNull(8)) sMessage = ""; else sMessage = reader.GetString(8);
                    if (reader.IsDBNull(9)) sFile_Name = ""; else sFile_Name = reader.GetString(9);
                    if (reader.IsDBNull(10)) dCreated_at = new DateTime(0001, 1, 1); else dCreated_at = reader.GetDateTime(10);
                    if (reader.IsDBNull(11)) dUpdated_at = new DateTime(0001, 1, 1); else dUpdated_at = reader.GetDateTime(11);

                    var post = new Leads(Db)
                    {
                        LeadId = iLeadId,
                        Full_Name = sFull_Name,
                        Compagny_Name = sCompagny_Name,
                        Email = sEmail,
                        Phone = sPhone,
                        Project_Name = sProject_Name,
                        Project_Description = sProject_Description,
                        Department = sDepartment,
                        Message = sMessage,
                        File_name = sFile_Name,
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
