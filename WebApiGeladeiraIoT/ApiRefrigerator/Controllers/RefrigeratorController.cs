using ApiRefrigerator.DTOs;
using ApiRefrigerator.Models;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiRefrigerator.Controllers
{
    [Route("api/refrigerator")]
    [ApiController]
    public class RefrigeratorController : ControllerBase
    {
        private readonly IRefrigeratorService _service;
        private readonly IMapper _mapper;

        public RefrigeratorController(IRefrigeratorService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns the available HTTP methods for the API.
        /// </summary>
        /// <remarks>
        /// This endpoint responds to an OPTIONS request and includes all the HTTP methods that the API supports.
        /// </remarks>
        /// <returns>A response with the 'Allow' header indicating the supported methods.</returns>
        [HttpOptions()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult OpcoesDisponiveis()
        {
            Response.Headers.Append("Allow", "GET, POST, PATCH, DELETE, OPTIONS");
            return Ok();
        }

        /// <summary>
        /// Retrieves all items in the refrigerator.
        /// </summary>
        /// <remarks>
        /// This endpoint returns all the items stored in the refrigerator.
        /// </remarks>
        /// <returns>A response containing all the items and a success message.</returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var items = await _service.GetAllAsync();
                return Ok(new { Data = items, Mensagem = "Enjoy your item(s)." });
            }
            catch (InvalidDataException)
            {
                return NoContent();
            }
            catch
            {
                return StatusCode(500, "Oops! I think the refrigerator door is stuck. Call the support to fix it.");
            }
        }

        /// <summary>
        /// Retrieves a specific item from the refrigerator by its ID.
        /// </summary>
        /// <param name="id">The ID of the item to be retrieved.</param>
        /// <remarks>
        /// This endpoint returns an item from the refrigerator based on the provided ID.
        /// </remarks>
        /// <returns>A response containing the found item and a success message, or an error message if the item is not found.</returns>
        [HttpGet("get-item/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var item = await _service.GetByIdAsync(id);
                if (item is null)
                    return NotFound(new { Mensagem = $"Item {id} not found." });

                return Ok(new { Data = item, Mensagem = "Enjoy your item." });
            }
            catch
            {
                return StatusCode(500, "Oops! I think the refrigerator door is stuck. Call the support to fix it.");
            }
        }

        /// <summary>
        /// Searches for an item in the refrigerator by name.
        /// </summary>
        /// <param name="name">The name of the item to be searched.</param>
        /// <remarks>
        /// This endpoint searches for an item in the refrigerator based on the provided name.
        /// </remarks>
        /// <returns>A response containing the found item and a success message, or an error message if the item is not found.</returns>
        [HttpGet("search-refrigerator/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    return BadRequest("Did you forget what you were going to look for in the refrigerator?");

                var item = await _service.GetByNameAsync(name);
                if (item is null)
                    return NotFound(new { Mensagem = $"There is no item named {name} in the refrigerator." });

                return Ok(new { Data = item, Mensagem = $"Enjoy your {name}." });
            }
            catch
            {
                return StatusCode(500, "Oops! I think the refrigerator door is stuck. Call the support to fix it.");
            }
        }

        /// <summary>
        /// Adds a new item to the refrigerator.
        /// </summary>
        /// <param name="item">The DTO containing the data for the item to be added.</param>
        /// <remarks>
        /// This endpoint adds a new item to the refrigerator based on the provided data.
        /// </remarks>
        /// <returns>A response confirming the addition of the item and its location in the refrigerator.</returns>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> InsertItem([FromBody] CreateRefrigeratorItemDTO item)
        {
            try
            {
                var inserirItem = _mapper.Map<Refrigerator>(item);

                await _service.InsertItemAsync(inserirItem);

                return Ok(new { Mensagem = $"{item.Name} is stored on floor {item.Floor}, container {item.Container}, and position {item.Position} in the refrigerator." });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Oops! I think the refrigerator door is stuck. Call the support to fix it.");
            }
        }

        /// <summary>
        /// Updates an existing item in the refrigerator.
        /// </summary>
        /// <param name="item">The DTO containing the data for the item to be updated.</param>
        /// <remarks>
        /// This endpoint updates an existing item in the refrigerator based on the provided data.
        /// </remarks>
        /// <returns>A response confirming the update of the item and its new location in the refrigerator.</returns>
        [HttpPatch()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateItem([FromBody] UpdateRefrigeratorItemDTO item)
        {
            try
            {
                var editItem = _mapper.Map<Refrigerator>(item);
                await _service.UpdateItemAsync(editItem);

                return Ok(new { Mensagem = $"{item.Name} has been updated to floor {item.Floor}, container {item.Container}, and position {item.Position} in the refrigerator." });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Oops! I think the refrigerator door is stuck. Call the support to fix it.");
            }
        }

        /// <summary>
        /// Removes a specific item from the refrigerator by its ID.
        /// </summary>
        /// <param name="id">The ID of the item to be removed.</param>
        /// <remarks>
        /// This endpoint removes an item from the refrigerator based on the provided ID.
        /// </remarks>
        /// <returns>A response confirming the removal of the item or an error message if the item is not found.</returns>
        [HttpDelete("remove-item/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveItem(int id)
        {
            try
            {
                await _service.RemoveItemAsync(id);
                return Ok(new { Message = "Item removed successfully." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = $"Item {id} not found." });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Oops! I think the refrigerator door is stuck. Call the support to fix it.");
            }
        }

        /// <summary>
        /// Removes all items from the refrigerator.
        /// </summary>
        /// <remarks>
        /// This endpoint performs a complete cleanup of the refrigerator, removing all items.
        /// </remarks>
        /// <returns>A response confirming the removal of all items or an error message.</returns>
        [HttpDelete("remove-all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveAllItems()
        {
            try
            {
                int itemsRemovedCount = await _service.RemoveAllAsync();

                return Ok(new { Message = $"Cleanup done. {itemsRemovedCount} item(s) removed successfully." });
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Oops! I think the refrigerator door is stuck. Call the support to fix it.");
            }
        }
    }
}

// Exercício por Marina Varela