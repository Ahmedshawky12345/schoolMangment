using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using schoolMangment.DTO;
using schoolMangment.Models;
using schoolMangment.Repository.Interfaces;
using schoolMangment.Repository.RepoClass;
using schoolMangment.UnitOfWork;

namespace schoolMangment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        

        public ClassController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
    
        }
        //---------------------------- Add Class-----------------------------------------
        [HttpPost("AddClass")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> AddClass([FromBody] ClassAddDTO classDTO)
        {
            var response = new GenralResponse<ClassDTO>();
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
                var _class = mapper.Map<Class>(classDTO);
                var Class = await unitOfWork.ClassRepository.AddAsync(_class);
                await unitOfWork.CompleteAsync();
                // map from model to DTO
                var data = mapper.Map<ClassDTO>(Class);


                response.Success = true;
                response.Message = "Class Added succfully";
                response.Data = data;
                return Ok(response);
           

            
        }
        //---------------------------- Update Class-----------------------------------------

        [HttpPut("UpdateClass")]
        [Authorize(Roles = "Teacher,Admin")]

        public async Task<IActionResult> UpdateCourse([FromBody] ClassDTO classDTO)
        {
            var response = new GenralResponse<ClassDTO>();
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
            var Class = await unitOfWork.ClassRepository.GetByIdAsync(classDTO.Id);
            if (Class == null)
            {
                response.Success = false;
                response.Message = "Department not found";
                return NotFound(response);
            }
            //map from DTO to  main model
            mapper.Map(classDTO, Class);


            var updateClass = await unitOfWork.ClassRepository.UpdateAsync(Class);
            // map from model to DTO
            var data = mapper.Map<ClassDTO>(updateClass);


            response.Success = true;
            response.Message = "Class updated succfully";
            response.Data = data;
            return Ok(response);

        }

        //-------------------- Get All Class---------------------
        [HttpGet("GetAll")]
        [Authorize(Roles = "Student,Teacher,Admin")]

        public async Task<IActionResult> GetAllClass()
        {
            var response = new GenralResponse<List<ClassDTO>>();
            var classes = await unitOfWork.ClassRepository.GetAllAsync();
            if (classes == null || !classes.Any())
            {
                response.Success = false;
                response.Message = "No classes found in the database.";
                return BadRequest(response);
            }
            var data = mapper.Map<List<ClassDTO>>(classes);


            response.Success = true;
            response.Message = " succfully response ";
            response.Data = data;
            return Ok(response);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletCourse(int id)
        {
            var response = new GenralResponse<ClassDTO>();
            var course = await unitOfWork.StudentRepository.GetByIdAsync(id);
            if (course == null)
            {
                response.Success = false;
                response.Message = "the student not found";
                return BadRequest(response);
            }
            await unitOfWork.StudentRepository.DeleteAsync(course);
            await unitOfWork.CompleteAsync();
            response.Success = true;
            response.Message = "student removed succesfully";

            return Ok(response);
        }

        //------------------------------ getClass By id-------------------------
        [HttpGet("GetClassById")]
        [Authorize(Roles = "Student,Teacher,Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = new GenralResponse<ClassDTO>();
            var Class = await unitOfWork.ClassRepository.GetByIdAsync(id);
            if (Class == null)
            {
                response.Success = false;
                response.Message = "the class not found";
                return BadRequest(response);
            }
            var data = mapper.Map<ClassDTO>(Class);


            response.Success = true;
            response.Message = " sucessfully response";
            response.Data = data;
            return Ok(response);
        }

        //----------------------------- Get All Data For class 
        [HttpGet("GetClassDepartmentAndStdent")]
        [Authorize(Roles = "Student,Teacher,Admin")]

        public async Task<IActionResult> GetClassDetiles(int id)
        {
            var response = new GenralResponse<ClassDetitlesDTO>();
            var classes = await unitOfWork.ClassDetailRepository.ClassDetitles(id);
            if (classes == null)
            {
                response.Success = false;
                response.Message = "no student";
                return BadRequest(response);
            }
            var data = mapper.Map<ClassDetitlesDTO>(classes);


            response.Success = true;
            response.Message = " succfully response ";
            response.Data = data;
            return Ok(response);
        }

        [HttpGet("GetAllstudentInClass")]
        [Authorize(Roles = "Student,Teacher,Admin")]

        public async Task<IActionResult> GetStudentClass(int id)
        {
            var response = new GenralResponse<StudentClassDTO>();
            var classes = await unitOfWork.ClassDetailRepository.StudentClass(id);
            if (classes == null)
            {
                response.Success = false;
                response.Message = "no student";
                return BadRequest(response);
            }
            var data = mapper.Map<StudentClassDTO>(classes);


            response.Success = true;
            response.Message = " succfully response ";
            response.Data = data;
            return Ok(response);
        }


    }
}
