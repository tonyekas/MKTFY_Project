using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FAQController : ControllerBase
    {
        private readonly IFAQRepository _faqRepository;
        public FAQController(IFAQRepository faqRepository)
        {
            _faqRepository = faqRepository;
        }


        //User may not be allowed to post to the FAQ but to search or request for the page
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpPost("create")] //create
        public async Task<ActionResult<FAQVM>> Create([FromBody] FAQAddVM data)
        {

            var result = await _faqRepository.Create(new FAQ(data)); //await _faqRepository.Create
            return Ok(new FAQVM(result)); // new FAQVM

        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<ActionResult<List<FAQVM>>> GetAll()
        {
            var results = await _faqRepository.GetAll();
            var models = results.Select(item => new FAQVM(item));
            return Ok(models);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FAQVM>> Get([FromRoute] Guid id)
        {
            var results = await _faqRepository.Get(id);
            return Ok(new FAQVM(results));
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpPut("updateFAQ")]
        public async Task<ActionResult<GenResponseVM>> UpdateFaq(FAQUpdateVM list)
        {
            var result = await _faqRepository.UpdateFaq(new FAQ(list));
            return new GenResponseVM
            {
                isSuccess = true,
                Message = "Your item is properly updated"
            };
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpDelete("id")]
        public async Task<ActionResult> Delete([FromBody] Guid id)
        {
            await _faqRepository.Delete(id);
            return Ok();
        }
    }
}
