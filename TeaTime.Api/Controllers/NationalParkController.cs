using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeaTime.Api.DTOS;
using TeaTime.Entities;
using TeaTime.Services;

namespace TeaTime.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class NationalParkController : ControllerBase
    {
        private readonly INationalParkService _nationalParkService;
        private readonly IMapper _mapper;
        public NationalParkController(INationalParkService nationalParkService, IMapper mapper)
        {
            _nationalParkService = nationalParkService;
            _mapper = mapper;
        }




        /// <summary>
        /// Get The List of All National Park
        /// </summary>
        /// <returns></returns>


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<NationalParkDtos>))]
        // [ProducesResponseType(400)]
        public IActionResult GetNationalParks()
        {
            var objList = _nationalParkService.GetNationalParks();
            var objDto = new List<NationalParkDtos>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<NationalParkDtos>(obj));
            }
            return Ok(objDto);

        }





        /// <summary>
        /// Get Individual National Park by Id
        /// </summary>
        /// <param name="nationalParkId"> The Id of the National Park +</param> 
        /// <returns></returns>


        [HttpGet("{nationalParkId:int}", Name = "GetNationalPark")]
        [ProducesResponseType(200, Type = typeof(List<NationalParkDtos>))]
        // [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetNationalPark(int nationalParkId)
        {
            var obj = _nationalParkService.GetNationalPark(nationalParkId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDtos = _mapper.Map<NationalParkDtos>(obj);
            return Ok(objDtos);
        }




        [HttpPost]
        //[ProducesResponseType(201, Type = typeof(List<NationalParkDtos>))]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(500)]


        [ProducesResponseType(201, Type = typeof(List<NationalParkDtos>))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateNationalPark([FromBody] NationalParkDtos nationalParkDtos)
        {
            if (nationalParkDtos == null)
            {
                return BadRequest(ModelState);
            }
            if (_nationalParkService.NationalParkExists(nationalParkDtos.Name))
            {
                ModelState.AddModelError("", "National Park Exists ! ");
                return StatusCode(404, ModelState);
            }
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            var nationalParakObj = _mapper.Map<NationalPark>(nationalParkDtos);
            if (!_nationalParkService.CreateNationalPark(nationalParakObj))
            {
                ModelState.AddModelError("", $"Something Went Wrong When Saving the Record {nationalParakObj.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetNationalPark", new { NationalParkId = nationalParakObj.Id }, nationalParakObj);
        }





        [HttpPatch("{nationalParkId:int}", Name = "UpdateNationalPark")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpDateNationalPark(int nationalParkId, [FromBody] NationalParkDtos nationalParkDto)
        {


            if (nationalParkDto == null || nationalParkId != nationalParkDto.Id)
            {
                return BadRequest(ModelState);
            }

            //if (_nationalParkService.NationalParkExists(nationalParkDto.Name))
            //{
            //    ModelState.AddModelError("", "National Park Exists ! ");
            //    return StatusCode(404, ModelState);
            //}
            var nationalParakObj = _mapper.Map<NationalPark>(nationalParkDto);
            if (!_nationalParkService.UpdateNationalPark(nationalParakObj))
            {
                ModelState.AddModelError("", $"Something Went Wrong When Updating the Record {nationalParakObj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }





        [HttpDelete("{nationalParkId:int}", Name = "DeleteNationalPark")]

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteNationalPark(int nationalParkId)
        {



            if(!_nationalParkService.NationalParkExists(nationalParkId))
            {
                return NotFound();
            }

            var nationalParkObj = _nationalParkService.GetNationalPark(nationalParkId);
         
            if (!_nationalParkService.DeleteNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Something Went Wrong When Deleting the Record {nationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }





    }








}
