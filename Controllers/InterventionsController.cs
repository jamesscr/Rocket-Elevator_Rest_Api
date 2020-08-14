using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rocket_Elevators_REST_API.Models;
using Rocket_Elevators_REST_API.Views;

namespace Rocket_Elevators_REST_API.Controllers
{
    [Route("api/[controller]")]
    public class InterventionsController : ControllerBase
    {
        public InterventionsController(AppDb db)
        {
            Db = db;
        }

        // GET api/leads
        [HttpGet("pending")]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new InterventionsQuery(Db);
            var result = await query.GetPending();
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

        

        // PUT api/interventions/inprogress/1
        [HttpPut("inprogress/{id}")]
        public async Task<IActionResult> changeInProgress(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new InterventionsQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.UpdateInProgressAsync();
            result = await query.FindOneAsync(id);
            return new OkObjectResult(result);
        }

        // PUT api/interventions/completed/1
        [HttpPut("completed/{id}")]
        public async Task<IActionResult> changeCompleted(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new InterventionsQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.UpdateCompletedAsync();
            result = await query.FindOneAsync(id);
            return new OkObjectResult(result);
        }





        public AppDb Db { get; }
    }
}