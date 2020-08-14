using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rocket_Elevators_REST_API.Models;
using Rocket_Elevators_REST_API.Views;

namespace Rocket_Elevators_REST_API.Controllers
{
    [Route("api/[controller]")]
    public class LeadsController : ControllerBase
    {
        public LeadsController(AppDb db)
        {
            Db = db;
        }

        // GET api/leads
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new LeadsQuery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        //// GET api/leads/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetOne(int id)
        //{
        //    await Db.Connection.OpenAsync();
        //    var query = new LeadsQuery(Db);
        //    var result = await query.FindOneAsync(id);
        //    if (result is null)
        //        return new NotFoundResult();
        //    return new OkObjectResult(result);
        //}

        //// POST api/leads
        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] Leads body)
        //{
        //    await Db.Connection.OpenAsync();
        //    body.Db = Db;
        //    await body.InsertAsync();
        //    return new OkObjectResult(body);
        //}

        //// PUT api/leads/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutOne(int id, [FromBody] Leads body)
        //{
        //    await Db.Connection.OpenAsync();
        //    var query = new LeadsQuery(Db);
        //    var result = await query.FindOneAsync(id);
        //    if (result is null)
        //        return new NotFoundResult();
        //    result.Status = body.Status;
        //    await result.UpdateAsync();
        //    return new OkObjectResult(result);
        //}

        //// DELETE api/leads/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteOne(int id)
        //{
        //    await Db.Connection.OpenAsync();
        //    var query = new LeadsQuery(Db);
        //    var result = await query.FindOneAsync(id);
        //    if (result is null)
        //        return new NotFoundResult();
        //    await result.DeleteAsync();
        //    return new OkResult();
        //}

        //// DELETE api/leads
        //[HttpDelete]
        //public async Task<IActionResult> DeleteAll()
        //{
        //    await Db.Connection.OpenAsync();
        //    var query = new LeadsQuery(Db);
        //    await query.DeleteAllAsync();
        //    return new OkResult();
        //}

        public AppDb Db { get; }
    }
}