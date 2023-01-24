﻿using dbase.Repository;
using domain.Data.Services;
using domain.Data.Models;
using domain.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IT_Project.Controllers
{
    [Route("api/specialization")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        private readonly SpecializationInteractor _specializations;

        public SpecializationController(SpecializationInteractor specializationInteractor)
        {
            _specializations = specializationInteractor;
        }

        [Authorize]
        [HttpPost("create")]
        public IActionResult CreateSpecialization(string name)
        {
            Specialization specialization = new Specialization(0, name);
            var res = specialization.IsValid();
            if (res.IsFailure)
                return Problem(statusCode: 400, detail: res.Error);
            if (_specializations.CreateSpeciailization(specialization) != null)
                return Ok(_specializations.GetByName(name));
            return Problem(statusCode: 400, detail: "Error while creating");
        }

        [Authorize]
        [HttpDelete("delete")]
        public IActionResult DeleteSpecialization(int id)
        {
            var specialization = _specializations.DeleteSpecialization(id);
            if (specialization.isFailure)
                return Problem(statusCode: 404, detail: specialization.Error);
            return Ok(specialization);
        }
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            return Ok(_specializations.GetAllSpecializations());
        }
    }
}