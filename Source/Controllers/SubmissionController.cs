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
        public ActionResult<decimal> GetAll(int? challengeId = null)
        {
            if (challengeId.HasValue)
            {
                var score = _service.FindHigherScoreByChallengeId(challengeId.Value);

                return Ok(score);
            }

            return NoContent();
        }
    }
}
