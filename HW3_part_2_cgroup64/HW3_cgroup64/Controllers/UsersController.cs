using HW2_cgroup64.BL;
using HW3_cgroup64.BL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HW3_cgroup64.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            User u = new User();
            return u.Read();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsersController>
        [HttpPost()]
        public int Post([FromBody] User u)
        {
            return u.Insert();
        }

        [HttpPost]
        [Route("Login")]
        public User Login([FromBody] User u)
        {
            return u.LoginUser();
        }

        // PUT api/<UsersController>/5
        [HttpPut]
        public User Put([FromBody] User u)
        {
            return u.Update();
        }

        [HttpPut]
        [Route("isActive")]
        public int updateActive([FromBody] User u)
        {
            return u.UpdateIsActive();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
