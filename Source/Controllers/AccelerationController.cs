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
    public class AccelerationController : ControllerBase
    {
        private readonly IAccelerationService _service;
        private readonly IMapper _mapper;

        public AccelerationController(IAccelerationService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET api/acceleration/{id}
        [HttpGet("{id}")]
        public ActionResult<AccelerationDTO> Get(int id)
        {
            var acceleration = _service.FindById(id);

            return Ok(_mapper.Map<AccelerationDTO>(acceleration));
        }

        // GET api/acceleration
        [HttpGet]
        public ActionResult<IEnumerable<AccelerationDTO>> GetAll(int? companyId = null)
        {
            if (companyId.HasValue)
            {
                var accelerations = _service.FindByCompanyId(companyId.Value);

                return Ok(_mapper.Map<IEnumerable<AccelerationDTO>>(accelerations));
            }

            return NoContent();
        }

        // POST api/acceleration
        [HttpPost]
        public ActionResult<AccelerationDTO> Post([FromBody] AccelerationDTO value)
        {
            var acceleration = _service.Save(_mapper.Map<Acceleration>(value));

            return Ok(_mapper.Map<AccelerationDTO>(acceleration));
        }
    }
}
