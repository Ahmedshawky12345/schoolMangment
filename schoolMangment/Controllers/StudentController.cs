using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using schoolMangment.DTO;
using schoolMangment.Models;
using schoolMangment.Repository.Interfaces;
using schoolMangment.UnitOfWork;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace schoolMangment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
      
        private readonly UserManager<AppUser> userManager;

        public StudentController(IUnitOfWork unitOfWork,IMapper mapper,UserManager<AppUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            
            this.userManager = userManager;
        }
        [HttpPut("update-email")]
        public async Task<IActionResult> UpdateEmail([FromBody] UpdateEmailDTO emailDTO)
        {
            var response = new GenralResponse<string>();

            if (string.IsNullOrEmpty(emailDTO.NewEmail))
            {
                response.Success = false;
                response.Message = "Email is required";
                return BadRequest(response);
            }

            var student = await unitOfWork.StudentRepository.GetByIdAsync(emailDTO.StudentId);
            if (student == null)
            {
                response.Success = false;
                response.Message = "Student not found";
                return NotFound(response);
            }

            var appUser = await userManager.FindByIdAsync(student.UserId);
            if (appUser == null)
            {
                response.Success = false;
                response.Message = "Associated user not found";
                return NotFound(response);
            }

            if (appUser.Email != emailDTO.NewEmail)
            {
                var emailResult = await userManager.SetEmailAsync(appUser, emailDTO.NewEmail);
                if (!emailResult.Succeeded)
                {
                    response.Success = false;
                    response.Message = "Failed to update email";
                    response.Errors.AddRange(emailResult.Errors.Select(e => e.Description));
                    return BadRequest(response);
                }
            }

            response.Success = true;
            response.Message = "Email updated successfully";
            return Ok(response);
        }
        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDTO passwordDTO)
        {
            var response = new GenralResponse<string>();

            if (string.IsNullOrEmpty(passwordDTO.NewPassword))
            {
                response.Success = false;
                response.Message = "New password is required";
                return BadRequest(response);
            }

            var student = await unitOfWork.StudentRepository.GetByIdAsync(passwordDTO.StudentId);
            if (student == null)
            {
                response.Success = false;
                response.Message = "Student not found";
                return NotFound(response);
            }

            var appUser = await userManager.FindByIdAsync(student.UserId);
            if (appUser == null)
            {
                response.Success = false;
                response.Message = "Associated user not found";
                return NotFound(response);
            }

            // Verify old password before updating
            var passwordVerification = await userManager.CheckPasswordAsync(appUser, passwordDTO.OldPassword);
            if (!passwordVerification)
            {
                response.Success = false;
                response.Message = "Current password is incorrect";
                return Unauthorized(response);
            }

            var passwordResult = await userManager.ChangePasswordAsync(appUser, passwordDTO.OldPassword, passwordDTO.NewPassword);
            if (!passwordResult.Succeeded)
            {
                response.Success = false;
                response.Message = "Failed to update password";
                response.Errors.AddRange(passwordResult.Errors.Select(e => e.Description));
                return BadRequest(response);
            }

            response.Success = true;
            response.Message = "Password updated successfully";
            return Ok(response);
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> updatestudent([FromBody] StudentDTO studentDTO)
        {
            var response = new GenralResponse<StudentDTO>();
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
            var student = await unitOfWork.StudentRepository.GetByIdAsync(studentDTO.id);
            if (student == null)
            {
                response.Success = false;
                response.Message = "Student not found";
                return NotFound(response);
            }
            //map from DTO to  main model
            mapper.Map(studentDTO, student);
            
           
            var update_student = await unitOfWork.StudentRepository.UpdateAsync(student);
            await unitOfWork.CompleteAsync();
            // map from model to DTO
            var data = mapper.Map<StudentDTO>(student);


            response.Success = true;
            response.Message = "student updated succfully";
            response.Data = data;
            return Ok(response);

        }

        //--------------------------------------------------- getbyid student--------------------------
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Teacher,Student")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = new GenralResponse<StudentDTO>();
          var student= await unitOfWork.StudentRepository.GetByIdAsync(id);
            if (student == null)
            {
                response.Success = false;
                response.Message = "the student not found";
                return BadRequest(response);
            }
            var data = mapper.Map<StudentDTO>(student);


            response.Success = true;
            response.Message = $"this student name {data.Name} ";
            response.Data = data;
            return Ok(response);
        }
        [HttpGet("GetAllStudents")]
        [Authorize("Admin,Teacher")]
        public async Task<IActionResult> GetAllStudents()
        {
            var response = new GenralResponse<List<StudentDTO>>();
            var students = await unitOfWork.StudentRepository.GetAllAsync();
            await unitOfWork.CompleteAsync();
            if (students == null)
            {
                response.Success = false;
                response.Message = "no student";
                return BadRequest(response);
            }

           
           

            var data = mapper.Map<List<StudentDTO>>(students);


            response.Success = true;
            response.Message = "this student updated sussfully ";
            response.Data = data;
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult>Deletestudent(int id)
        {
            var response = new GenralResponse<StudentDTO>();
            var student = await unitOfWork.StudentRepository.GetByIdAsync(id);
            if (student == null)
            {
                response.Success = false;
                response.Message = "the student not found";
                return BadRequest(response);
            }
            await unitOfWork.StudentRepository.DeleteAsync(student);
            await unitOfWork.CompleteAsync();
            response.Success = true;
            response.Message = "student removed succesfully";

            return Ok(response);
        }

        //------------------- Get StudentAndCourses-----------------
        [Authorize(Roles = "Admin,Teacher,Student")]
        [HttpGet("GetstudentwithCourses/{id}")]
        public async Task<IActionResult> getStudentAndCourses(int id)
        {
            var response = new GenralResponse<StudentAndCours>();
            var student = await unitOfWork.StudentDetailRepository.GetStudentWithCoursesByIdAsync(id);
            if (student== null)
            {
                response.Success = false;
                response.Message = "no student";
                return BadRequest(response);
            }
           var data= mapper.Map<StudentAndCours>(student);
            response.Success = true;
            response.Message = "succes get data";
            response.Data = data;
            return Ok(response);


        }

        //------------------------ add courseToStudent------------------------

        [HttpPost("AddCourseToStudent")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> AddCourseToStudent(StudentCorseDTO studentCourseDTO)
        {
            var response = new GenralResponse<StudentCorseDTO>();

            if (!ModelState.IsValid)
            {
                response.Success = false;
                response.Message = "Validation failed";
                response.Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                return BadRequest(response);
            }

            // Log incoming data
            Console.WriteLine($"Incoming Student ID: {studentCourseDTO.student_id}, Course ID: {studentCourseDTO.course_id}");

            var studentData = await unitOfWork.StudentRepository.GetByIdAsync(studentCourseDTO.student_id);
            var courseData = await unitOfWork.CourseRepository.GetByIdAsync(studentCourseDTO.course_id);

            // Debug output
            Console.WriteLine($"Student Data: {studentData}, Course Data: {courseData}");

            if (studentData == null || courseData == null)
            {
                response.Success = false;
                response.Message = "The student or course does not exist.";
                return BadRequest(response);
            }

            // Check if the student is already enrolled in the course
            var existingStudentCourse = await unitOfWork.StudentDetailRepository.FindAsync(x =>
                x.student_id == studentCourseDTO.student_id &&
                x.CourseId == studentCourseDTO.course_id);

            if (existingStudentCourse != null)
            {
                response.Success = false;
                response.Message = "This student is already enrolled in this course.";
                return BadRequest(response);
            }

            // Map from DTO to the main model
            var studentCourse = mapper.Map<StudentCourse>(studentCourseDTO);

            // Add the new enrollment
            await unitOfWork.StudentDetailRepository.AddCourseToStudent(studentCourse);
            await unitOfWork.CompleteAsync();

            // Map from model back to DTO
            var data = mapper.Map<StudentCorseDTO>(studentCourse);

            response.Success = true;
            response.Message = "Successfully added the course for the student.";
            response.Data = data;
            return Ok(response);
        }



        //------------------- Get StudentExam-----------------
        [Authorize(Roles = "Student,student,Admin")]
        [HttpGet("GetstudentExam/{id}")]
        public async Task<IActionResult> GetStudentExam(int id)
        {
            var response = new GenralResponse<ExamStudentDetitles>();
            var studentExam = await unitOfWork.StudentDetailRepository.GetStudentWithExamsAsync(id);
            if (studentExam == null)
            {
                response.Success = false;
                response.Message = "no data";
                return BadRequest(response);
            }
            var data = mapper.Map<ExamStudentDetitles>(studentExam);
            if (data == null){
                response.Success = false;
                response.Message = "no data";
                return BadRequest(response);
            }
            response.Success = true;
            response.Message = "sucess response";
            response.Data = data;
            return Ok(response);


        }
        [HttpGet("grades")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetStudentGrades()
        {
            var response = new GenralResponse<StudentGradeDTO>();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                response.Success = false;
                    response.Message = "User not authenticated.";
                return BadRequest( Response);
            }

            var studentCourse = await unitOfWork.StudentDetailRepository.GradeForUser(userId);

            if (studentCourse == null)
            {
                response.Success = false;
                 response.Message = "No data found.";
                return BadRequest(response);
            }

            // Map to DTO
            var data = mapper.Map<StudentGradeDTO>(studentCourse);
            response.Success = true;
            response.Message = "sucess response";
            response.Data = data;
            return Ok(response);
           
        }
        [HttpGet("GetAllStudntInSpasificClass")]
        [Authorize(Roles = "Student,Teacher,Admin")]

        public async Task<IActionResult> GetStudentClass(int id)
        {
            var response = new GenralResponse<StudentClassDTO>();
            var students = await unitOfWork.StudentDetailRepository.GetStudentClass(id);
            if (students == null )
            {
                response.Success = false;
                response.Message = "no student";
                return BadRequest(response);
            }
            var data = mapper.Map<StudentClassDTO>(students);


            response.Success = true;
            response.Message = " succfully response ";
            response.Data = data;
            return Ok(response);
        }
    }
}
