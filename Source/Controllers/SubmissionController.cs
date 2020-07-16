using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        private readonly ISubmissionService _service;
        private readonly IMapper _mapper;

        public SubmissionController(ISubmissionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET api/submission/higherScore
        [HttpGet("higherScore")]
        public ActionResult<decimal> GetHigherScore(int? challengeId = null)
        {
            if (challengeId.HasValue)
            {
                var score = _service.FindHigherScoreByChallengeId(challengeId.Value);

                return Ok(score);
            }

            return NoContent();
        }

        // GET api/submission
        [HttpGet]
        public ActionResult<IEnumerable<SubmissionDTO>> GetAll(int? challengeId = null, int? accelerationId = null)
        {
            if (challengeId == null && accelerationId == null)
            {
                return NoContent();
            }

            var submissions = _service.FindByChallengeIdAndAccelerationId(challengeId.GetValueOrDefault(), accelerationId.GetValueOrDefault());

            return Ok(_mapper.Map<IEnumerable<SubmissionDTO>>(submissions));
        }
    }
}
