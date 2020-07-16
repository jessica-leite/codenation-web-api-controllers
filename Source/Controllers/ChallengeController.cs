using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengeController : ControllerBase
    {
        private readonly IChallengeService _service;
        private readonly IMapper _mapper;

        public ChallengeController(IChallengeService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET api/challenge
        [HttpGet]
        public ActionResult<IEnumerable<ChallengeDTO>> GetAll(int? accelerationId = null, int? userId = null)
        {
            if (accelerationId == null && userId == null)
            {
                return NoContent();
            }

            var challenges = _service.FindByAccelerationIdAndUserId(accelerationId.GetValueOrDefault(), userId.GetValueOrDefault());

            return Ok(_mapper.Map<IEnumerable<ChallengeDTO>>(challenges));
        }

        // POST api/challenge
        [HttpPost]
        public ActionResult<ChallengeDTO> Post([FromBody] ChallengeDTO value)
        {
            var challenge = _service.Save(_mapper.Map<Models.Challenge>(value));

            return Ok(_mapper.Map<ChallengeDTO>(challenge));
        }
    }
}
