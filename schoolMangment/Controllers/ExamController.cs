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
   
    public class ExamController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public ExamController(IMapper mapper,IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        //---------------------------- Add Exam-----------------------------------------
        [HttpPost]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> AddExam([FromBody] ExamDetilesDTO examDTO)
        {
            var response = new GenralResponse<ExamDTO>();
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
            var _exam = mapper.Map<Exam>(examDTO);
            var exam = await unitOfWork.ExamRepository.AddAsync(_exam);
            await unitOfWork.CompleteAsync();
            // map from model to DTO
            var data = mapper.Map<ExamDTO>(exam);


            response.Success = true;
            response.Message = "succfully create Exam";
            response.Data = data;
            return Ok(response);

        }
        //----------------------------------- update exam---------------------
        [HttpPut]
        [Authorize(Roles = "Teacher,Admin")]

        public async Task<IActionResult> UpdateCourse([FromBody] ExamDTO examDTO)
        {
            var response = new GenralResponse<ExamDTO>();
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
            var exam = await unitOfWork.ExamRepository.GetByIdAsync(examDTO.Id);
            if (exam == null)
            {
                response.Success = false;
                response.Message = "Exam not found";
                return NotFound(response);
            }
            //map from DTO to  main model
            mapper.Map(examDTO, exam);


            var Exam = await unitOfWork.ExamRepository.UpdateAsync(exam);
            await unitOfWork.CompleteAsync();

            // map from model to DTO
            var data = mapper.Map<ExamDTO>(Exam);


            response.Success = true;
            response.Message = "Exam updated successully";
            response.Data = data;
            return Ok(response);

        }
        //-------------------- Get All Exam---------------------
        [HttpGet("GetAllExam")]
        [Authorize(Roles = "Student,Teacher,Admin")]

        public async Task<IActionResult> GetAllExams()
        {
            var response = new GenralResponse<List<ExamDTO>>();
            var exams = await unitOfWork.ExamRepository.GetAllAsync();
            if (exams == null)
            {
                response.Success = false;
                response.Message = "no Exam";
                return BadRequest(response);
            }
            var data = mapper.Map<List<ExamDTO>>(exams);


            response.Success = true;
            response.Message = " succfully response ";
            response.Data = data;
            return Ok(response);
        }
        [HttpDelete("DeletExam")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletExam(int id)
        {
            var response = new GenralResponse<ExamDTO>();
            var Exam = await unitOfWork.ExamRepository.GetByIdAsync(id);
            if (Exam == null)
            {
                response.Success = false;
                response.Message = "the student not found";
                return BadRequest(response);
            }
            await unitOfWork.ExamRepository.DeleteAsync(Exam);
            await unitOfWork.CompleteAsync();
            response.Success = true;
            response.Message = "Exam removed succesfully";

            return Ok(response);
        }

        // --------------- getbyid -------------
        [HttpGet("GetExamById")]
        [Authorize(Roles = "Student,Teacher,Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = new GenralResponse<ExamDTO>();
            var exam = await unitOfWork.ExamRepository.GetByIdAsync(id);
            if (exam == null)
            {
                response.Success = false;
                response.Message = "the Exam not found";
                return BadRequest(response);
            }
            var data = mapper.Map<ExamDTO>(exam);


            response.Success = true;
            response.Message = " sucessfully response";
            response.Data = data;
            return Ok(response);
        }

     
    }
}
