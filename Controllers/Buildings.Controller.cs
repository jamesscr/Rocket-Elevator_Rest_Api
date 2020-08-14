using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rocket_Elevators_REST_API.Models;
using Rocket_Elevators_REST_API.Views;

namespace Rocket_Elevators_REST_API.Controllers
{
    [Route("api/[controller]")]
    public class BuildingsController : ControllerBase
    {
        public BuildingsController(AppDb db)
        {
            Db = db;
        }

        // GET api/buildings
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new BuildingsQuery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        // GET api/buildings/5
       /* [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new BuildingsQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody] Buildings body)
        {
            await Db.Connection.OpenAsync();
            var query = new BuildingsQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.Status = body.Status;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }*/

        public AppDb Db { get; }
    }
}