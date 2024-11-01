using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using schoolMangment.Data;
using schoolMangment.DTO;
using schoolMangment.Models;
using schoolMangment.Repository.Interfaces;
using schoolMangment.UnitOfWork;

namespace schoolMangment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentExamController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        

        public StudentExamController( IMapper mapper,IUnitOfWork unitOfWork)
        {
            
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        // ------------ EnrollStudentInExam
        [HttpPost("enroll")]
        [Authorize(Roles ="Teacher")]
        public async Task<IActionResult> EnrollStudentInExam(ExamStudentDTO examStudentDTO)
        {
            var response = new GenralResponse<ExamStudentDTO>();
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
            var studentdata = await unitOfWork.StudentRepository.GetByIdAsync(examStudentDTO.Student_id);
            var exam = await unitOfWork.ExamRepository.GetByIdAsync(examStudentDTO.Examp_id);

            if(studentdata == null || exam == null)
            {
                response.Success = false;
                response.Message = "student or exam no has data";
                return BadRequest(response);
            }

            var ExistStuentExam = await unitOfWork.ExamStudentDetailRepository.CheckEnrollmentAsync(examStudentDTO.Student_id, examStudentDTO.Examp_id);
            if (ExistStuentExam != null)
            {
                response.Success = false;
                response.Message = "this student alrady Enroll this Exam";
                return BadRequest(response);

            }
          
            

            var _studentexam = mapper.Map<StudentExam>(examStudentDTO);
             await unitOfWork.ExamStudentDetailRepository.EnrollStudentInExamAsync(_studentexam);
            await unitOfWork.CompleteAsync();

            response.Success = true;
            response.Message = "Student enrolled in exam successfully";
           

            return Ok(response);


        }
        //------------------- RemoveStudentFromExam
        [HttpDelete("RemoveStudentFromExam")]
        [Authorize(Roles = "Teacher,Admin")]


        public async Task<IActionResult> RemoveStudentFromExam(RemoveStudentExamDTO removeStudentExamDTO)
        {
            var response = new GenralResponse<string>();

            try
            {
                await unitOfWork.ExamStudentDetailRepository.
                    RemoveStudentFromExamAsync(removeStudentExamDTO.Student_id, removeStudentExamDTO.Examp_id);
                await unitOfWork.CompleteAsync();
                response.Success = true;
                response.Message = "Student successfully removed from the exam.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while removing the student from the exam.";
                response.Errors.Add(ex.Message);
                return BadRequest(response);
            }

            return Ok(response);
        
    }

    }
}
