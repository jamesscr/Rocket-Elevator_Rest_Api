using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;
using Rocket_Elevators_REST_API.Models;

namespace Rocket_Elevators_REST_API.Views
{
    public class QuotesQuery
    {
        public AppDb Db { get; }

        public QuotesQuery(AppDb db)
        {
            Db = db;
        }

        //public async Task<Quotes> FindOneAsync(int id)
        //{
        //    using var cmd = Db.Connection.CreateCommand();
        //    cmd.CommandText = @"SELECT `Id`, `status` FROM `quotes` WHERE `Id` = @id";
        //    cmd.Parameters.Add(new MySqlParameter
        //    {
        //        ParameterName = "@id",
        //        DbType = DbType.Int32,
        //        Value = id,
        //    });
        //    var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
        //    return result.Count > 0 ? result[0] : null;
        //}

        // public async Task<List<Quotes>> LatestPostsAsync()
        // {
        //     using var cmd = Db.Connection.CreateCommand();
        //     cmd.CommandText = @"SELECT `Id`, `Full_Name`, `Compagny_Name`, `Email`, `Phone`, `Project_Name`, `Project_Description`, `Department`, `Message`, `File_name`, `created_at`, `updated_at`  FROM `quotes` WHERE DATEDIFF(NOW(), `created_at`) <= 30  ORDER BY `created_at` ;";
        //     return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        // }

        public async Task<List<Quotes>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT quotes.Id, quotes.Full_Name, quotes.Phone_Number, quotes.Company_Name, quotes.Building_Type, quotes.Product_Quality, quotes.Nb_Business, quotes.Nb_Floor, quotes.Nb_Basement, quotes.Nb_Cage, quotes.Nb_Parking, quotes.Nb_Ele_Suggested, quotes.Subtotal, quotes.Install_Fee, quotes.Final_Price FROM `quotes` WHERE `Product_Quality` = 'Excellium' AND `Building_Type` = 'commercial' ORDER BY `Id` LIMIT 50;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
           using var txn = await Db.Connection.BeginTransactionAsync();
           using var cmd = Db.Connection.CreateCommand();
           cmd.CommandText = @"DELETE FROM `rocketApp_development`";
           await cmd.ExecuteNonQueryAsync();
           await txn.CommitAsync();
        }

        private async Task<List<Quotes>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Quotes>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    int? nc_quote_id, nc_nb_apt, nc_nb_biz, nc_nb_comp, nc_nb_floor, nc_nb_base, nc_nb_cage, nc_nb_park, nc_nb_occ;
                    string? nc_full_name, nc_company_name, nc_building_type, nc_product_quality, nc_nb_hours, nc_nb_ele, nc_subtotal, nc_install, nc_final, nc_phone;
                    DateTime? nc_created_at, nc_updated_at;

                    if(reader.IsDBNull(0)) nc_quote_id = 0; else nc_quote_id = reader.GetInt32(0);
                    if(reader.IsDBNull(1)) nc_full_name = ""; else nc_full_name = reader.GetString(1);
                    if(reader.IsDBNull(2)) nc_phone = ""; else nc_phone = reader.GetString(2);
                    if(reader.IsDBNull(3)) nc_company_name = ""; else nc_company_name = reader.GetString(3);
                    if(reader.IsDBNull(4)) nc_building_type = ""; else nc_building_type = reader.GetString(4);
                    if(reader.IsDBNull(5)) nc_product_quality = ""; else nc_product_quality = reader.GetString(5);
                    if(reader.IsDBNull(6)) nc_nb_biz = 0; else nc_nb_biz = reader.GetInt32(6);
                    // if(reader.IsDBNull(8)) nc_nb_comp = 0; else nc_nb_comp = reader.GetInt32(8);
                    if(reader.IsDBNull(7)) nc_nb_floor = 0; else nc_nb_floor = reader.GetInt32(7);
                    if(reader.IsDBNull(8)) nc_nb_base = 0; else nc_nb_base = reader.GetInt32(8);
                    if(reader.IsDBNull(9)) nc_nb_cage = 0; else nc_nb_cage = reader.GetInt32(9);
                    if(reader.IsDBNull(10)) nc_nb_park = 0; else nc_nb_park = reader.GetInt32(10);
                    // if(reader.IsDBNull(13)) nc_nb_occ = 0; else nc_nb_occ = reader.GetInt32(13);
                    // if(reader.IsDBNull(14)) nc_nb_hours = ""; else nc_nb_hours = reader.GetString(14);
                    if(reader.IsDBNull(11)) nc_nb_ele = ""; else nc_nb_ele= reader.GetString(11);
                    if(reader.IsDBNull(12)) nc_subtotal = ""; else nc_subtotal= reader.GetString(12);
                    if(reader.IsDBNull(13)) nc_install = ""; else nc_install= reader.GetString(13);
                    if(reader.IsDBNull(14)) nc_final = ""; else nc_final = reader.GetString(14);
                    // if (reader.IsDBNull(19)) nc_created_at = new DateTime(0000, 0, 0); else nc_created_at = reader.GetDateTime(19);
                    // if (reader.IsDBNull(20)) nc_updated_at = new DateTime(0000, 0, 0); else nc_updated_at = reader.GetDateTime(20);


                    var post = new Quotes(Db)
                    {
                        QuoteId = nc_quote_id,
                        Full_Name = nc_full_name,
                        Phone_Number = nc_phone,
                        Company_Name = nc_company_name,
                        Building_Type = nc_building_type,
                        Product_Quality = nc_product_quality,
                        // Nb_Appartement = nc_nb_apt,
                        Nb_Business = nc_nb_biz,
                        // Nb_Company = nc_nb_comp,
                        Nb_Floor = nc_nb_floor,
                        Nb_Basement = nc_nb_base,
                        Nb_Cage = nc_nb_cage,
                        Nb_Parking = nc_nb_park,
                        // Nb_OccupantPerFloor = nc_nb_occ,
                        // Nb_OperatingHour = nc_nb_hours,
                        Nb_Ele_Suggested = nc_nb_ele,
                        Subtotal = nc_subtotal,
                        Install_Fee = nc_install,
                        Final_Price = nc_final
                        // Created_At = nc_created_at,
                        // Updated_At = nc_updated_at
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}
