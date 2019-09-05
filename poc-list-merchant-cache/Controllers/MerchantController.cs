using Microsoft.AspNetCore.Mvc;
using pocListMerchantCache.Model.RequestMessages;
using pocListMerchantCache.Services;
using System;
using System.Threading.Tasks;

namespace poc_list_merchant_cache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantController : ControllerBase
    {
        MerchantService MerchantService { get; } = new MerchantService();

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddAsync(AddMerchantRequestMessage addMerchantRequestMessage)
        {
            try
            {
                return Ok(await MerchantService.AddAsync(addMerchantRequestMessage));
            }
            catch (Exception e)
            {
                return BadRequest(new { e.GetBaseException().Message });
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var merchant = await MerchantService.GetAsync(id);

            if (merchant == null) return NotFound();

            return Ok(merchant);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] UpdateMerchantRequestMessage updateMerchantRequestMessage)
        {
            var merchant = await MerchantService.UpdateAsync(id, updateMerchantRequestMessage);

            if (merchant == null) return NotFound();

            return Ok(merchant);
        }

        [HttpGet]
        [Route("keys")]
        public async Task<IActionResult> GetAllKeysAsync()
        {
            return Ok(await MerchantService.GetAllKeysAsync());
        }

        [HttpPost]
        [Route("clear")]
        public async Task<IActionResult> ClearAsync()
        {
            await MerchantService.ClearAsync();
            return Ok();
        }
    }
}