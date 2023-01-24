﻿using domain.Data.Models;
using domain.Data.Interfaces;
using IT_Project.Views;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace IT_Project.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorController : Controller
    {
        private readonly DoctorInteractor _doctors;

        public DoctorController(DoctorInteractor doctors)
        {
            _doctors = doctors;
        }
        [Authorize]
        [HttpPost("create")]
        public IActionResult CreateDoctor(string fullname, int specialization_id)
        {
            Doctor doctor = new Doctor(0, fullname, specialization_id);
            var res = _doctors.CreateDoctor(doctor);
            if (res.isFailure)
                return Problem(statusCode: 400, detail: res.Error);
            return Ok(new DoctorSerializer
            {
                Id = res.Value.Id,
                Fullname = res.Value.Fullname,
                SpecializationId = res.Value.SpecializationId
            });
        }
        [Authorize]
        [HttpDelete("delete")]
        public IActionResult DeleteDoctor(int id)
        {
            var res = _doctors.DeleteDoctor(id);
            if (res.isFailure)
                return Problem(statusCode: 400, detail: res.Error);
            return Ok(new DoctorSerializer
            {
                Id = res.Value.Id,
                Fullname = res.Value.Fullname,
                SpecializationId = res.Value.SpecializationId
            });
        }

        [HttpGet("get_all")]
        public IActionResult GetAllDoctors()
        {
            var res = _doctors.GetAllDoctors();
            if (res.isFailure)
                return Problem(statusCode: 400, detail: res.Error);
            return Ok(res.Value);
        }
        
        [HttpGet("get")]
        public IActionResult Get(int id)
        {
           var res = _doctors.GetDoctor(id);
            if (res.isFailure)
                return Problem(statusCode: 404, detail: res.Error);
            return Ok(new DoctorSerializer
            {
                Id = res.Value.Id,
                Name = res.Value.Fullname,
                SpecializationId = res.Value.SpecializationId
            });
        }
        [HttpGet("getBySpecialization")]
        public IActionResult GetBySpec(int specialization_id)
        {
            Specialization spec = new Specialization(specialization_id, "tmp");
            var res = _doctors.GetDoctor(spec);
            if (res.isFailure)
                return Problem(statusCode: 400, detail: res.Error);
            return Ok(res.Value);
        }
    }
}