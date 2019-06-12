using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SafHackathon.Domain.Models;
using SafHackathon.Domain.Services;

namespace SafHackathon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet]
        public async Task<IEnumerable<Card>> Get()
        {
            var cards = await _cardService.ListAsync();
            return cards;
        }

        [HttpGet("Serial/{serial}")]
        public async Task<IActionResult> GetBySerial([FromRoute] string serial)
        {
            var list = await _cardService.GetBySerialAsync(serial);

            if (list.Count() ==0)
            {
                return NotFound();
            }

            return Ok(list);
        }

        [HttpGet("Scratch/Active")]
        public async Task<IActionResult> GetActive()
        {
            var list = await _cardService.GetActive();

            if (list.Count() == 0)
            {
                return NotFound();
            }

            return Ok(list);
        }



        // POST: api/Card
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CardResource input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _cardService.InsertAsync(input.RandomInput.ToString());
            //return Created($"Card/{hour.Id}", Map(hourlyShareRate));
            return Ok();
        }

        [HttpPut("RequestCard/{serial}")]
        public async Task<IActionResult> UpdateStatus([FromRoute]string serial)
        {
            var updated = await _cardService.Update(serial);
            if (updated == null)
            {
                return NotFound();
            }

            return Ok(updated);
        }


        // DELETE: api/ApiWithActions/5
        [HttpDelete("Status")]
        public async Task<IActionResult> Delete()
        {
            var result = await _cardService.DeleteAsync();

            if (result ==0)
                return NotFound();

            return Ok();
        }

        [HttpGet("DateCreated")]
        public async Task<IActionResult> GetByDate()
        {
            var list = await _cardService.GetByDate();

            if (list.Count() == 0)
            {
                return NotFound();
            }

            return Ok(list);
        }



    }
}
