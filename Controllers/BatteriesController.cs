using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rocket_Elevators_REST_API.Models;
using Rocket_Elevators_REST_API.Views;

namespace Rocket_Elevators_REST_API.Controllers
{
    [Route("api/[controller]")]
    public class BatteriesController : ControllerBase
    {
        public BatteriesController(AppDb db)
        {
            Db = db;
        }

        // GET api/batteries
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new BatteriesQuery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        // GET api/batteries/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new BatteriesQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        //// POST api/batteries
        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] Batteries body)
        //{
        //    await Db.Connection.OpenAsync();
        //    body.Db = Db;
        //    await body.InsertAsync();
        //    return new OkObjectResult(body);
        //}

        // PUT api/batteries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody] Batteries body)
        {
            await Db.Connection.OpenAsync();
            var query = new BatteriesQuery(Db);
            var result = await query.FindOneAsync(id);
            //check the request sent if it valid
            if (body is null)
                return new NotFoundObjectResult("Please enter something in the status");
            if (body.Status.ToLower() != "intervention" && body.Status.ToLower() != "active" && body.Status.ToLower() != "inactive")
                return new NotFoundObjectResult("The status you entered is invalide!");
            if (result is null)
                return new NotFoundResult();
            result.Status = body.Status;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        //// DELETE api/batteries/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteOne(int id)
        //{
        //    await Db.Connection.OpenAsync();
        //    var query = new BatteriesQuery(Db);
        //    var result = await query.FindOneAsync(id);
        //    if (result is null)
        //        return new NotFoundResult();
        //    await result.DeleteAsync();
        //    return new OkResult();
        //}

        //// DELETE api/batteries
        //[HttpDelete]
        //public async Task<IActionResult> DeleteAll()
        //{
        //    await Db.Connection.OpenAsync();
        //    var query = new BatteriesQuery(Db);
        //    await query.DeleteAllAsync();
        //    return new OkResult();
        //}

        public AppDb Db { get; }
    }
}