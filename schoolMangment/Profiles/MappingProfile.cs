using AutoMapper;
using schoolMangment.DTO;
using schoolMangment.Models;

namespace schoolMangment.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //---------------------------------------------- for student----------------
            // map from sudent to DTO
            CreateMap<Student, StudentDTO>()
             .ForMember(des => des.Email, opt => opt.MapFrom(src => src.user.Email));
                //ForMember(des => des.Name, opt => opt.MapFrom(src => src.Name)).
                //  ForMember(des => des.Email, opt => opt.MapFrom(src => src.Email)).
                //    ForMember(des => des.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth)).
                //      ForMember(des => des.Gender, opt => opt.MapFrom(src => src.Gender));
                // map from DTO to student

            CreateMap<StudentDTO, Student>()
                  .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            //CreateMap<Student, StudentupdatedDTO>()
            //      .ForMember(des => des.id, opt => opt.MapFrom(src => src.Id))
            //        .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Name))

            //    .ForMember(des => des.Email, opt => opt.MapFrom(src => src.user.Email));
            //CreateMap<StudentupdatedDTO, Student>();
            //---------------------------------------------- for course--------------------
            CreateMap<Course, CourseDetilesDTO>();
            CreateMap<CourseDetilesDTO, Course>();
            CreateMap<Course,CourseDTO>();
            CreateMap<CourseDTO, Course>()
    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


            // ------------------------------studentAndcoures----------------------------

            //for Add
            CreateMap<StudentCourse, StudentCorseDTO>()
               .ForMember(des => des.course_id, opt => opt.MapFrom(src => src.CourseId))
               .ForMember(des => des.student_id, opt => opt.MapFrom(src => src.student_id))
            .ForMember(des => des.Grade, opt => opt.MapFrom(src => src.Grade));
            CreateMap<StudentCorseDTO, StudentCourse>()
                 .ForMember(des => des.CourseId, opt => opt.MapFrom(src => src.course_id))
               .ForMember(des => des.student_id, opt => opt.MapFrom(src => src.student_id))
               .ForMember(des => des.Grade, opt => opt.MapFrom(src => src.Grade));

            // for Get
            CreateMap<Student, StudentAndCours>()
                .ForMember(des => des.coursename, opt => opt.MapFrom(src => src.studentCourses.Select(x => x.Course.Name)));
            //------------------------ for Teacher----------------------------------------
            CreateMap<Teacher, TeacherDTO>();
            CreateMap< TeacherDTO, Teacher>();
            CreateMap<Teacher, TeacherDetilesDTO>();
            CreateMap<TeacherDetilesDTO, Teacher>();

            // --------- what courses that teacher do it ----------------------
           
                CreateMap<Teacher, CourseTeacherDTO>()
    .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Name)) // Map teacher name correctly
   
    .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Courses.Select(x => x.Name).ToList())); // Map courses

            //--------------------------- for user --------------------------------------
            //-------for public regster
            CreateMap<RegsterDTO, AppUser>()
   .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
      .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
         .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username));

           




            //-------------for Login 
            CreateMap<RegsterDTO, AppUser>()
           .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
           .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));


            // --------------- for exam table 

            CreateMap<ExamDTO, Exam>()
                  .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap< Exam, ExamDTO>();
            CreateMap<ExamDetilesDTO, Exam>();
            CreateMap<Exam, ExamDetilesDTO>();


            //------------- for studentExam Table
            CreateMap<ExamStudentDTO, StudentExam>()
         .ForMember(dest => dest.studentId, opt => opt.MapFrom(src => src.Student_id))
         .ForMember(dest => dest.examId, opt => opt.MapFrom(src => src.Examp_id))
             .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Grade));

            CreateMap< StudentExam,ExamStudentDTO>()
      .ForMember(dest => dest.Student_id, opt => opt.MapFrom(src => src.studentId))
      .ForMember(dest => dest.Examp_id, opt => opt.MapFrom(src => src.examId))
          .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Grade));

            CreateMap<Student, ExamStudentDetitles>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                

.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)) // Map teacher name correctly

.ForMember(dest => dest.ExamTitle, opt => opt.MapFrom(src => src.studentExams.Select(x => x.exam.Title).ToList()));

            // for getgrad student
            CreateMap<StudentCourse, StudentGradeDTO>()
        .ForMember(dest => dest.gradName, opt => opt.MapFrom(src => src.Grade))
        .ForMember(dest => dest.coursename, opt => opt.MapFrom(src => src.Course.Name))
        .ForMember(dest => dest.studentName, opt => opt.MapFrom(src => src.student.Name));


            //----------------------------------- for Departments--------------------------------
            CreateMap<Department, DepartmentDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name));
            CreateMap< DepartmentDTO, Department>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name));
            CreateMap<Department, DepartmentAddDTO>()
            .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name));
            CreateMap<DepartmentAddDTO, Department>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name));

            //---------------------- for class ------------------------------

            CreateMap<Class, ClassDTO>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<ClassDTO, Class>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<Class, ClassAddDTO>()
                
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.DepatmentId, opt => opt.MapFrom(src => src.DepartmentId));
            CreateMap<ClassAddDTO, Class>()
              
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepatmentId));

            //---------------- GetClassAndDEpartmetAndStudent------------------

            CreateMap<Class, ClassDetitlesDTO>()
              .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.className, opt => opt.MapFrom(src => src.Name))

             .ForMember(dest => dest.department_Detitles, opt => opt.MapFrom(src => src.Department != null ? new List<DepartmentDTO> { new DepartmentDTO { id = src.Department.Id, name = src.Department.Name } } : new List<DepartmentDTO>()))
            .ForMember(dest => dest.Student_Detiles, opt => opt.MapFrom(src => src.Students.Select(s => new StudentDTO {   Name = s.Name }).ToList()));
            //----------------- GetAllstudent in Spasific class ------------------------------

            CreateMap<Class, StudentClassDTO>()
         .ForMember(dest => dest.ClassId, opt => opt.MapFrom(src => src.Id))
         .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Name))
         .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students.Select(x => new StudentsDataDTO
         {
             Id = x.Id,
             Name = x.Name
         }).ToList()));

            //--------------- Get Department and the modelRelationData

            // for student-----------------------
            CreateMap<Department, StudentDepartmentsDTO>()
              .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.students, opt => opt.MapFrom(src => src.Students.Select(x => new StudentData
                {
                    StudentId = x.Id,
                    StudentName = x.Name
                }).ToList()));
            // for class-----------------------
            CreateMap<Department, ClassDepartmentDTO>()
           .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.Classes, opt => opt.MapFrom(src => src.Classes.Select(x => new ClassData
             {
                 ClassId = x.Id,
                 ClassName = x.Name
             }).ToList()));
            // for Teacher-----------------------
            CreateMap<Department, TeachersDepartmentDTO>()
           .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.Teachers, opt => opt.MapFrom(src => src.Teachers.Select(x => new TeahcerData
             {
                 TeacherId = x.Id,
                 TeacherName = x.Name
             }).ToList()));

        }
    }
}
