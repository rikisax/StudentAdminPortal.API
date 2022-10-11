using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Repositories;

namespace StudentAdminPortal.API.Controllers
{
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;

        public StudentController(IStudentRepository studentRepository, IMapper mapper)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("[controller]/{Id:guid}")]
        [ActionName("GetStudentAsync")]
        public async Task<IActionResult> GetStudentAsync([FromRoute] Guid Id)
        {
            var student = await studentRepository.GetStudentAsync(Id);
            if (student == null)
            {
                return NotFound();
            }   
            return Ok(mapper.Map<Student>(student));
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            var students = await  studentRepository.GetStudentsAsync();
            return Ok(mapper.Map<List<Student>>(students));

            //var domainModelStudents = new List<Student>();
            //foreach (var student in students)
            //{
            //    domainModelStudents.Add(new Student()
            //    {
            //        Id = student.Id,
            //        FirstName = student.FirstName,
            //        LastName = student.LastName,
            //        DateOfBirth = student.DateOfBirth,
            //        Email = student.Email,
            //        Mobile = student.Mobile,
            //        ProfileImageUrl = student.ProfileImageUrl,
            //        GenderId = student.GenderId,
            //        Address = new Address()
            //        {
            //            Id = student.Address.Id,
            //            PhysicalAddress = student.Address.PhysicalAddress,
            //            PostalAddress = student.Address.PostalAddress
            //        },
            //        Gender = new Gender()
            //        {
            //            Id= student.Gender.Id,
            //            Description=student.Gender.Description
            //        }

            //    });
            //}
            //return Ok(domainModelStudents);
        }

        [HttpPut]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> UpdateStudentAsync([FromRoute] Guid studentId, [FromBody] UpdateStudentRequest request)
        {
            //ricevo dalla UI un oggetto DomainModel e lo passo al repository trasformandolo prima in DataModel tramite automapper.
            //quindi ritorno un oggetto di nuovo rimappato da DataModel a DomainModel
            if (await studentRepository.Exists(studentId))
            {

                //update Details
                var updatedStudent = await studentRepository.UpdateStudent(studentId, mapper.Map<DataModels.Student>(request));
                if (updatedStudent != null)
                {
                    return Ok(mapper.Map<Student>(updatedStudent));
                }
            }
                return NotFound();
        }

        [HttpDelete]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] Guid studentId)
        {
            if (await studentRepository.Exists(studentId)) 
            { 
                var student = await studentRepository.DeleteStudent(studentId);
                    return Ok(mapper.Map<Student>(student));  //gli passo un DataModel e ritorno un DomainModel
            }
            return NotFound();
        }

        [HttpPost]
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentRequest request)
        {
            var student = await studentRepository.AddStudent(mapper.Map<DataModels.Student>(request));
            var addedStudent = mapper.Map<DomainModels.Student>(student);
            return CreatedAtAction(nameof(GetStudentAsync),new {Id = student.Id},addedStudent);
        }
    }
}
