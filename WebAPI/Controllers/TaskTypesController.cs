﻿using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskTypesController : ControllerBase
    {
        private ITaskTypeService _taskTypeService;

        public TaskTypesController(ITaskTypeService taskTypeService)
        {
            _taskTypeService = taskTypeService;
        }
        [HttpGet("GetAllByLicenceIdAndActivity")]
        public IActionResult GetAllByLicenceIdAndActivity(int licenceId, int isActive)
        {
            var result = _taskTypeService.GetAllByLicenceIdAndActivity(licenceId, isActive);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _taskTypeService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("Delete")]
        public IActionResult Delete(int id)
        {
            var result = _taskTypeService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("Add")]
        public IActionResult Add(TaskType taskType)
        {
            var result = _taskTypeService.Add(taskType);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("Update")]
        public IActionResult Update(TaskType taskType)
        {
            var result = _taskTypeService.Update(taskType);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
