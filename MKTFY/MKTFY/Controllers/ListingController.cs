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
    /// <summary>
    /// Get the listing controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "member")] // This may be removed to allow all users to access service without authorization     
    public class ListingController : ControllerBase
    {
        private readonly IListingRepository _listingRepository;
        /// <summary>
        /// Listing info
        /// </summary>
        /// <param name="listingRepository"></param>
        public ListingController(IListingRepository listingRepository)
        {
            _listingRepository = listingRepository;
        }

        [HttpPost("create")]
        // [AllowAnonymous] // This can be added in line to bypass the global settings
        public async Task<ActionResult<ListingVM>> Create([FromBody] ListingAddVM data)
        {
            var result = await _listingRepository.Create(new Listing(data));
            return Ok(new ListingVM(result));

        }
        /// <summary>
        /// Get all listing
        /// </summary>
        /// <remarks>Shows all the availaible listing</remarks>
        /// <returns>
        /// Array of all listing
        /// </returns>
        /// <response code="200">Listing found</response>
        /// <response code="401">Not currently logged in</response>
        /// <response code="403">User has no access to stuff</response>
        /// <response code="500">Server failure, unknown reason</response>
        [HttpGet("getall")]
        public async Task<ActionResult<List<ListingVM>>> GetAll()
        {
            var results = await _listingRepository.GetAll();
            var models = results.Select(item => new ListingVM(item));
            return Ok(models);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ListingVM>> Get([FromRoute] Guid id)
        {
            var results = await _listingRepository.Get(id);
            return Ok(new ListingVM(results));
        }

        [HttpPut("updatelisting")]
        public async Task<ActionResult<Listing>> UpdateListItems([FromBody] ListingUpdateVM list)
        {
            var result = await _listingRepository.UpdateListing(new Listing(list));
            return Ok(result);
            //{
            //    isSuccess = true,
            //    Message = "Your listing is properly updated"
            //};
        }

        // For searching items based on UserId and CategoryId
        [HttpGet("searchitem")]
        public async Task<ActionResult> SearchItems(SearchAddVM src)
        {
            var output = await _listingRepository.Search(new Search(src));
            var query = output.ItemName;
            // Adding the filtering items from the Repository
            if (output != null)
            {
                //query = output
            }
            return Ok(output);
        }

        // Below for searching Any Item listed on the web page without user being logged in
        [HttpGet("searchall")]
        public async Task<ActionResult<IEnumerable<Listing>>> SearchAll(string product, string listing)
        {
            var result = await _listingRepository.SearchList(listing, product);
            if (result.Any())
            {
                return Ok(result);
            }
            return NotFound();
        }

        // Getting the User listing
        [HttpGet("getuserlist/{Id}")] //id = UserId
        public async Task<ActionResult<List<ListingVM>>> GetUserListing([FromRoute] string userid)

        {
            var lists = await _listingRepository.UserListing(userid);
            List<Listing> result = new List<Listing>();
            foreach (var item in lists)
            {
                Listing newitems = new Listing(item);
                result.Add(newitems);
            }
            return Ok(result);
        }

        // for deleting listed items
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromBody] Guid id)
        {
            await _listingRepository.Delete(id);
            return Ok();
        }

    }
}
