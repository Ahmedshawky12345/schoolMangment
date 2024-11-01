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
    public class TeacherController : ControllerBase
    {
       
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
     

        public TeacherController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            
        }

       
        //--------------------- Teacher update----------------------------------------------------
        [HttpPut("updateTeacher")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> UpdateTeacher([FromBody] TeacherDTO teacherDTO)
        {
            var response = new GenralResponse<TeacherDetilesDTO>();
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
            var teacher = await unitOfWork.TeacherRepository.GetByIdAsync(teacherDTO.Id);
            if (teacher == null)
            {
                response.Success = false;
                response.Message = "Teacher not found";
                return NotFound(response);
            }
            //map from DTO to  main model
            mapper.Map(teacherDTO, teacher);


            var update = await unitOfWork.TeacherRepository.UpdateAsync(teacher);
            await unitOfWork.CompleteAsync();
            // map from model to DTO
            var data = mapper.Map<TeacherDetilesDTO>(update);


            response.Success = true;
            response.Message = "Teacher updated succfully";
            response.Data = data;
            return Ok(response);

        }
        //--------------------------- Teacher Getbyid -----------------------------------
        [HttpGet("GetByid")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = new GenralResponse<TeacherDetilesDTO>();
            var teacher = await unitOfWork.TeacherRepository.GetByIdAsync(id);
            if (teacher == null)
            {
                response.Success = false;
                response.Message = "this teacher not found";
                return BadRequest(response);
            }
            var data = mapper.Map<TeacherDetilesDTO>(teacher);


            response.Success = true;
            response.Message = " succfully response";
            response.Data = data;
            return Ok(response);
        }

        //--------------------------- Get All Teacher------------------------------------------
        [HttpGet("GetAllTeachers")]

        [Authorize(Roles = "Admin,Teacher,Student")]
        public async Task<IActionResult> GetAllTeachers()
        {
            var response = new GenralResponse<List<TeacherDetilesDTO>>();
            var teachers = await unitOfWork.TeacherRepository.GetAllAsync();
            if (teachers == null || !teachers.Any())
            {
                response.Success = false;
                response.Message = "no Teachers";
                return BadRequest(response);
            }
            var data = mapper.Map<List<TeacherDetilesDTO>>(teachers);

           
            response.Success = true;
            response.Message = "response sussfully ";
            response.Data = data;
            return Ok(response);
        }

        // ----------------------------- Delete Teacher----------------------
        [HttpDelete("DeleteTeacher")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var response = new GenralResponse<TeacherDetilesDTO>();
            var teacher = await unitOfWork.TeacherRepository.GetByIdAsync(id);
            if (teacher == null)
            {
                response.Success = false;
                response.Message = "the student not found";
                return BadRequest(response);
            }
            await unitOfWork.TeacherRepository.DeleteAsync(teacher);
            await unitOfWork.CompleteAsync();
            response.Success = true;
            response.Message = "Teacher removed succesfully";

            return Ok(response);
        }



        [HttpGet("GetTeacherAndThierCourses")]

        [Authorize(Roles = "Admin,Teacher,Student")]
        public async Task<IActionResult> TeacherCorures()
        {
            var response = new GenralResponse<List<CourseTeacherDTO>>();
            var teachers = await unitOfWork.TeacherDetailRepository.GetTeachersAndCourses();
            if (teachers == null || !teachers.Any())
            {
                response.Success = false;
                response.Message = "No Teachers";
                return BadRequest(response);
            }
            var data = mapper.Map<List<CourseTeacherDTO>>(teachers);

            response.Success = true;
            response.Message = "Response successfully";
            response.Data = data;
            return Ok(response);
        }
      


    }
}
