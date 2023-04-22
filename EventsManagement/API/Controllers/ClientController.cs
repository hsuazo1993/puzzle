using BusinessLogic.Services;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }


        [HttpPost]
        public async Task<ActionResult> CreateAsync(ClientDto clientDto)
        {
            var result = await _clientService.CreateAsync(clientDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }


            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDto>> GetByIdAsync(int id)
        {
            var result = await _clientService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }


            return Ok(result.Data);
        }

        [HttpGet]
        public async Task<ActionResult<List<ClientDto>>> GetAllAsync()
        {
            var result = await _clientService.GetAllAsync();
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }


            return Ok(result.Data);
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<List<ClientDto>>> GetClientsByStatusAsync(ClientStatusEnum status)
        {
            var result = await _clientService.GetClientsByStatusAsync(status);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }


            return Ok(result.Data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, ClientDto clientDto)
        {
            if (id != clientDto.Id)
            {
                return BadRequest("Client ID in URL does not match Client ID in body.");
            }


            var result = await _clientService.UpdateAsync(clientDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }


            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await _clientService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }


            return NoContent();
        }
    }
}
