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
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _service;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET api/company/{id}
        [HttpGet("{id}")]
        public ActionResult<CompanyDTO> Get(int id)
        {
            var company = _service.FindById(id);

            return Ok(_mapper.Map<CompanyDTO>(company));
        }

        // GET api/company
        [HttpGet]
        public ActionResult<IEnumerable<CompanyDTO>> GetAll(int? accelerationId = null, int? userId = null)
        {
            if (accelerationId.HasValue && userId == null)
            {
                var companies = _service.FindByAccelerationId(accelerationId.Value);

                return Ok(_mapper.Map<IEnumerable<CompanyDTO>>(companies));
            }

            if (userId.HasValue && accelerationId == null)
            {
                var companies = _service.FindByUserId(userId.Value);

                return Ok(_mapper.Map<IEnumerable<CompanyDTO>>(companies));
            }

            return NoContent();
        }

        // POST api/company
        [HttpPost]
        public ActionResult<CompanyDTO> Post([FromBody] CompanyDTO value)
        {
            var company = _service.Save(_mapper.Map<Company>(value));

            return Ok(company);
        }
    }
}
