using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;

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
            throw new NotImplementedException();
        }

        // POST api/user
        [HttpPost]
        public ActionResult<UserDTO> Post([FromBody] UserDTO value)
        {
            throw new NotImplementedException();
        }

    }
}
