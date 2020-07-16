using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET api/user
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetAll(string accelerationName = null, int? companyId = null)
        {
            if (!string.IsNullOrWhiteSpace(accelerationName) && companyId == null)
            {
                var users = _service.FindByAccelerationName(accelerationName);

                return Ok(_mapper.Map<IEnumerable<UserDTO>>(users));
            }

            if (companyId.HasValue && accelerationName == null)
            {
                var users = _service.FindByCompanyId(companyId.Value);

                return Ok(_mapper.Map<IEnumerable<UserDTO>>(users));
            }

            return NoContent();
        }

        // GET api/user/{id}
        [HttpGet("{id}")]
        public ActionResult<UserDTO> Get(int id)
        {
            var user = _service.FindById(id);

            return Ok(_mapper.Map<UserDTO>(user));
        }

        // POST api/user
        [HttpPost]
        public ActionResult<UserDTO> Post([FromBody] UserDTO value)
        {
            var user = _service.Save(_mapper.Map<User>(value));

            return Ok(_mapper.Map<UserDTO>(user));
        }
    }
}
