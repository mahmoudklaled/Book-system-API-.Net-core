using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using my_books.Data.Services;
using my_books.Data.ViewModels;
using my_books.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private PublishersService _publishersService;
        private readonly ILogger<PublishersController> _logger;
        public PublishersController(PublishersService publishersService , ILogger<PublishersController> logger)
        {
            _publishersService = publishersService;
            _logger= logger;
        }

        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublisher(string sortby , string searchstring ,int pagenumber)
        {
            try
            {
                _logger.LogInformation("This is a log inGetAllPublisher() ");
                var _result = _publishersService.getAllPublishers(sortby, searchstring,pagenumber);
                return Ok(_result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            try
            {

                var newPublisher = _publishersService.AddPublisher(publisher);
                return Created(nameof(AddPublisher), newPublisher);
            }
            catch (PublisherNameExeption ex) 
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {

                return BadRequest($"{ ex.Message} , Publisher Name : {publisher.Name}");
            }
        }

        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherById(int id)
        {
            var _response = _publishersService.GetPublisherById(id);

            if(_response != null)
            {
                return Ok(_response);
            } else
            {
                return NotFound();
            }
        }


        [HttpGet("get-publisher-books-with-authors/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            var _response = _publishersService.GetPublisherData(id);
            return Ok(_response);
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            
            try
            {
                _publishersService.DeletePublisherById(id);
                return Ok();
            }
            catch (Exception  ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
