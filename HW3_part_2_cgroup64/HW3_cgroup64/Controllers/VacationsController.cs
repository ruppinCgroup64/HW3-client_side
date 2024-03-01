using HW2_cgroup64.BL;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HW2_cgroup64.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationsController : ControllerBase
    {
        // GET: api/<VacationsController>
        [HttpGet]
        public IEnumerable<Vacation> Get()
        {
            return Vacation.Read();
        }

        //HW1
        //[HttpGet("getByDates/{startDate}/{endDate}")]//return the vac between start and end date
        //public IEnumerable<Vacation> GetByDates(DateTime startDate, DateTime endDate)
        //{
        //    return Vacation.ReadByDates(startDate, endDate);
        //}

    

        [HttpGet]
        public Object GetReport(string month)
        {
            int m= Convert.ToInt32(month);
            return Vacation.Report(m);
        }

        // POST api/<VacationsController>
        [HttpPost]
        public int Post([FromBody] Vacation vac)
        {
            return vac.insert();
        }

        // PUT api/<VacationsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VacationsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
