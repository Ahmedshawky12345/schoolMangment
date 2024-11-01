using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using schoolMangment.DTO;
using schoolMangment.Models;
using schoolMangment.Repository.Interfaces;
using schoolMangment.UnitOfWork;

namespace schoolMangment.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public CourseController(IMapper mapper,IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }


        //---------------------------- Add course-----------------------------------------
        [HttpPost]
        [Authorize(Roles = "Teacher, Admin")]
        public async Task<IActionResult> AddStudent([FromBody] CourseDetilesDTO CourrseDTO)
        {
            var response = new GenralResponse<CourseDTO>();
            if (!ModelState.IsValid)
            {
                response.Success = false;
                response.Message = "Valdation falid";
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        response.Errors.Add(error.ErrorMessage);
                    }
                }
                return BadRequest(response);
            }
            //map from DTO to  main model
            var _course = mapper.Map<Course>(CourrseDTO);
            var course = await unitOfWork.CourseRepository.AddAsync(_course);
            await unitOfWork.CompleteAsync();
            // map from model to DTO
            var data = mapper.Map<CourseDTO>(course);


            response.Success = true;
            response.Message = "course Added succfully";
            response.Data = data;
            return Ok(response);

        }
        [HttpPut("UpdateCourse")]
        [Authorize(Roles = "Teacher, Admin")]
        public async Task<IActionResult> UpdateCourse([FromBody] CourseDTO courseDTO)
        {
            var response = new GenralResponse<CourseDTO>();
            if (!ModelState.IsValid)
            {
                response.Success = false;
                response.Message = "Valdation falid";
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        response.Errors.Add(error.ErrorMessage);
                    }
                }
                return BadRequest(response);
            }
            var course = await unitOfWork.CourseRepository.GetByIdAsync(courseDTO.Id);
            
            if (course == null)
            {
                response.Success = false;
                response.Message = "Course not found";
                return NotFound(response);
            }
            //map from DTO to  main model
            mapper.Map(courseDTO, course);


            var updatedcourse = await unitOfWork.CourseRepository.UpdateAsync(course);
            await unitOfWork.CompleteAsync();
            // map from model to DTO
            var data = mapper.Map<CourseDTO>(updatedcourse);


            response.Success = true;
            response.Message = "Course updated succfully";
            response.Data = data;
            return Ok(response);

        }

        //-------------------- Get All course---------------------
        [HttpGet("GetAllCourses")]
        [Authorize(Roles = "Teacher, Admin,Student")]
        public async Task<IActionResult> GetAllCourse()
        {
            var response = new GenralResponse<List<CourseDTO>>();
            var course = await unitOfWork.CourseRepository.GetAllAsync();
            if (course == null)
            {
                response.Success = false;
                response.Message = "no Course";
                return BadRequest(response);
            }
            var data = mapper.Map<List<CourseDTO>>(course);


            response.Success = true;
            response.Message = "Course updated sussfully ";
            response.Data = data;
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletCourse(int id)
        {
            var response = new GenralResponse<CourseDetilesDTO>();
            var course = await unitOfWork.CourseRepository.GetByIdAsync(id);
            if (course == null)
            {
                response.Success = false;
                response.Message = "the course not found";
                return BadRequest(response);
            }
            await unitOfWork.CourseRepository.DeleteAsync(course);
            await unitOfWork.CompleteAsync();
            response.Success = true;
            response.Message = "Course removed succesfully";

            return Ok(response);
        }
    }
}
