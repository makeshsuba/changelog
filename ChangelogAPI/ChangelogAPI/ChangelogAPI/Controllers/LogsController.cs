using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Changelog.Abstraction.Abstractions;
using Changelog.Abstraction.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChangelogAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ILogsService _logService;

        public LogsController(ILogsService logService)
        {
            _logService = logService;
        }

        [HttpPost("AddLog")]
        public async Task<IActionResult> Addlog(LogsContextDTO log)
        {
            await _logService.AddLog(log);
            return StatusCode(201);
        }

        [HttpPut("UpdateLog")]
        public async Task<IActionResult> UpdateLog(LogsContextDTO log)
        {
            await _logService.UpdateLog(log);
            return StatusCode(201);
        }

        [HttpDelete("DeleteLog")]
        public async Task<IActionResult> DeleteLog([Required]int logId)
        {
            await _logService.DeleteLog(logId);
            return StatusCode(201);
        }

        [HttpGet("GetLog")]
        public async Task<IActionResult> GetLog([Required]int logId)
        {
            var result = await _logService.GetLog(logId);
            return Ok(result);
        }

        [HttpGet("GetLogs")]
        public async Task<IActionResult> GetLogs(string userName)
        {
            LogsListContextDTO input = new LogsListContextDTO
            {
                UserName = userName
            };
            var result = await _logService.GetLogs(input);
            if (result == null)
                return BadRequest("No Logs Found");
            return Ok(result);
        }

        [HttpGet("GetLogTypes")]
        public async Task<IActionResult> GetLogTypes()
        {
            var result = await _logService.GetLogType();
            return Ok(result);
        }


    }
}