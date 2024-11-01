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
    public class DepartmentController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
       

        public DepartmentController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            
        }
        //---------------------------- Add Department-----------------------------------------
        [HttpPost("AddDepartment")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentAddDTO departmentAddDTO)
        {
            var response = new GenralResponse<DepartmentDTO>();
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
            var _department = mapper.Map<Department>(departmentAddDTO);
            var department = await unitOfWork.DepartmentRepository.AddAsync(_department);
            await unitOfWork.CompleteAsync();
            // map from model to DTO
            var data = mapper.Map<DepartmentDTO>(department);


            response.Success = true;
            response.Message = "Department Added succfully";
            response.Data = data;
            return Ok(response);

        }
        //---------------------------- Update Department-----------------------------------------

        [HttpPut("UpdateDepartment")]
        [Authorize(Roles = "Teacher,Admin")]

        public async Task<IActionResult> UpdateCourse([FromBody] DepartmentDTO departmentDTO)
        {
            var response = new GenralResponse<DepartmentDTO>();
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
            var department = await unitOfWork.DepartmentRepository.GetByIdAsync(departmentDTO.id);
            if (department == null)
            {
                response.Success = false;
                response.Message = "Department not found";
                return NotFound(response);
            }
            //map from DTO to  main model
            mapper.Map(departmentDTO, department);


            var updatedDepartment = await unitOfWork.DepartmentRepository.UpdateAsync(department);
            await unitOfWork.CompleteAsync();

            // map from model to DTO
            var data = mapper.Map<DepartmentDTO>(updatedDepartment);


            response.Success = true;
            response.Message = "student updated succfully";
            response.Data = data;
            return Ok(response);

        }
        //-------------------- Get All Department---------------------
        [HttpGet("GetAllDepartment")]
        [Authorize(Roles = "Student,Teacher,Admin")]

        public async Task<IActionResult> GetAllExams()
        {
            var response = new GenralResponse<List<DepartmentDTO>>();
            var departments = await unitOfWork.DepartmentRepository.GetAllAsync();
            if (departments == null)
            {
                response.Success = false;
                response.Message = "no student";
                return BadRequest(response);
            }
            var data = mapper.Map<List<DepartmentDTO>>(departments);


            response.Success = true;
            response.Message = " succfully response ";
            response.Data = data;
            return Ok(response);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletCourse(int id)
        {
            var response = new GenralResponse<DepartmentDTO>();
            var course = await unitOfWork.DepartmentRepository.GetByIdAsync(id);
            if (course == null)
            {
                response.Success = false;
                response.Message = "the student not found";
                return BadRequest(response);
            }
            await unitOfWork.DepartmentRepository.DeleteAsync(course);
            await unitOfWork.CompleteAsync();

            response.Success = true;
            response.Message = "student removed succesfully";

            return Ok(response);
        }

        //------------------------------ GetDepartment By id-------------------------
        [HttpGet("{id}")]
        [Authorize(Roles = "Student,Teacher,Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = new GenralResponse<DepartmentDTO>();
            var department = await unitOfWork.DepartmentRepository.GetByIdAsync(id);
            if (department == null)
            {
                response.Success = false;
                response.Message = "the Department not found";
                return BadRequest(response);
            }
            var data = mapper.Map<DepartmentDTO>(department);


            response.Success = true;
            response.Message = " sucessfully response";
            response.Data = data;
            return Ok(response);
        }



        // ---------------------- GetStudent in Department ------------
        [HttpGet("GetAllstudentInDepartment")]
        [Authorize(Roles = "Student,Teacher,Admin")]

        public async Task<IActionResult> GetStudentDepartment(int id)
        {
            var response = new GenralResponse<StudentDepartmentsDTO>();
            var student = await unitOfWork.DepartmentDetailRepository.GetDepartmentStudent(id);
            if (student == null)
            {
                response.Success = false;
                response.Message = "no student";
                return BadRequest(response);
            }
            var data = mapper.Map<StudentDepartmentsDTO>(student);


            response.Success = true;
            response.Message = " succfully response ";
            response.Data = data;
            return Ok(response);
        }
        //-------------------------------- Get Class in Department ------------
        [HttpGet("GetDepartmentClass")]
        [Authorize(Roles = "Student,Teacher,Admin")]

        public async Task<IActionResult> GetDepartmentClass(int id)
        {
            var response = new GenralResponse<ClassDepartmentDTO>();
            var classes = await unitOfWork.DepartmentDetailRepository.GetDepartmentClass(id);
            if (classes == null)
            {
                response.Success = false;
                response.Message = "no class";
                return BadRequest(response);
            }
            var data = mapper.Map<ClassDepartmentDTO>(classes);


            response.Success = true;
            response.Message = " succfully response ";
            response.Data = data;
            return Ok(response);
        }
        //--------------------------------- GetDepartmentTeachers----
        [HttpGet("GetDeaprtmentTeachers")]
        [Authorize(Roles = "Student,Teacher,Admin")]

        public async Task<IActionResult> GetDepartmentTeachers(int id)
        {
            var response = new GenralResponse<TeachersDepartmentDTO>();
            var teachers= await unitOfWork.DepartmentDetailRepository.GetDepartmentTeacher(id); ;
            if (teachers == null)
            {
                response.Success = false;
                response.Message = "no Teacher";
                return BadRequest(response);
            }
            var data = mapper.Map<TeachersDepartmentDTO>(teachers);


            response.Success = true;
            response.Message = " succfully response ";
            response.Data = data;
            return Ok(response);
        }
    }
}
