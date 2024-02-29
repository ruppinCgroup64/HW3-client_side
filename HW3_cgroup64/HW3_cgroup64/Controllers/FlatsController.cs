using HW2_cgroup64.BL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HW1_cgroup64.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlatsController : ControllerBase
    {
        // GET: api/<FlatController>
        [HttpGet]
        public IEnumerable<Flat> Get()
        {
            Flat f = new Flat();
            return f.Read();
        }

        [HttpGet("max")]
        public IEnumerable<Flat> GetMaxPriceAndCity(string city, double price)
        {
            return Flat.ReadByMaxAndCity(city, price);
        }

        // GET api/<FlatController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FlatController>
        [HttpPost]
        public int Post([FromBody] Flat f)
        {
            return f.Insert();
        }

        // PUT api/<FlatController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FlatController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
