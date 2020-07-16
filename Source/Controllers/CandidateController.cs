using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _service;
        private readonly IMapper _mapper;

        public CandidateController(ICandidateService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET api/candidate/{userId}/{accelerationId}/{companyId}
        [HttpGet("{id}")]
        public ActionResult<CandidateDTO> Get(int userId, int accelerationId, int companyId)
        {
            var candidate = _service.FindById(userId, accelerationId, companyId);

            return Ok(_mapper.Map<CandidateDTO>(candidate));
        }

        // GET api/candidate
        [HttpGet]
        public ActionResult<IEnumerable<CandidateDTO>> GetAll(int? accelerationId = null, int? companyId = null)
        {
            if (accelerationId.HasValue && companyId == null)
            {
                var candidates = _service.FindByAccelerationId(accelerationId.Value);

                return Ok(_mapper.Map<IEnumerable<CandidateDTO>>(candidates));
            }

            if (companyId.HasValue && accelerationId == null)
            {
                var candidates = _service.FindByCompanyId(companyId.Value);

                return Ok(_mapper.Map<IEnumerable<CandidateDTO>>(candidates));
            }

            return NoContent();
        }
    }
}
