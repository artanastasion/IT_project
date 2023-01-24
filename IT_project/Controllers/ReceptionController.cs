using domain.Data.Models;
using domain.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IT_Project.Controllers
{
    [ApiController]
    [Route("api/receptions")]
    public class ReceptionController : Controller
    {
        private readonly ReceptionInteractor _receptions;
        private readonly ScheduleInteractor _schedules;
        public ReceptionController(ReceptionInteractor _receptions, ScheduleInteractor schedule)
        {
            _schedules = schedule;
            _receptions = reception;
        }
        [Authorize]
        [HttpPost("save")]
        public IActionResult SaveReception(int patientId,
            int doctorId,
            DateTime start_time,
            DateTime end_time,
            int schedule_id)
        {
            Reception reception = new Reception(start_time, end_time, patientId, doctorId);
            var schedule = _schedules.GetSchedule(schedule_id);
            if (schedule.isFailure)
            {
                return Problem(statusCode: 404, detail: schedule.Error);
            }

            var res = _receptions.SaveReception(reception, schedule.Value);
            if (res.isFailure)
                return Problem(statusCode: 404, detail: res.Error);
            return Ok(res.Value);
        }

        [HttpGet("get")]
        public IActionResult GetReception(int specialization_id)
        {
            Specialization spec = new Specialization(specialization_id, "tmp");
            var res = _receptions.GetReception(spec);

            if (res.isFailure)
                return Problem(statusCode: 404, detail: res.Error);
            return Ok(res.Value);
        }
    }
}