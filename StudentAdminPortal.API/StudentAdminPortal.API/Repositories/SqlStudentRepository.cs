using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using StudentAdminPortal.API.DataModels;

namespace StudentAdminPortal.API.Repositories
{
    public class SqlStudentRepository : IStudentRepository
    {
        private readonly StudentAdminContext studentAdminContext;

        public SqlStudentRepository(StudentAdminContext studentAdminContext)
        {
            this.studentAdminContext = studentAdminContext;
        }


        public async Task<List<Gender>> GetGenderAsync()
        {
            return await studentAdminContext.Gender.ToListAsync();
        }

        public async Task<Student> GetStudentAsync(Guid Id)
        {
            return await studentAdminContext.Student
                .Include(nameof(Gender))
                .Include(nameof(Address))
                .FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async  Task<List<Student>> GetStudentsAsync()
        {
            return await studentAdminContext.Student.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
        }

        public async Task<bool> Exists(Guid studentId)
        {
            return await studentAdminContext.Student.AnyAsync(x => x.Id == studentId);
        }

        public async Task<Student> UpdateStudent(Guid studentId, Student request)
        {
            var existingStudent = await GetStudentAsync(studentId);
            if (existingStudent != null)
            { 
                existingStudent.FirstName = request.FirstName;
                existingStudent.LastName = request.LastName;
                existingStudent.DateOfBirth = request.DateOfBirth;
                existingStudent.Email = request.Email;
                existingStudent.Mobile = request.Mobile;
                existingStudent.GenderId = request.GenderId;
                existingStudent.Address.PhysicalAddress = request.Address.PhysicalAddress;
                existingStudent.Address.PostalAddress = request.Address.PostalAddress;
                
                await studentAdminContext.SaveChangesAsync();
                return existingStudent;
            }
            return null;
        }

        public async Task<Student> DeleteStudent(Guid studentId)
        {
            var student =await GetStudentAsync(studentId);
            if (student != null)
            {
                studentAdminContext.Student.Remove(student);
                await studentAdminContext.SaveChangesAsync();
                return student;
            }
            return null;
        }

        public async Task<Student> AddStudent(DataModels.Student request)
        {
            var student = await studentAdminContext.Student.AddAsync(request);
            await studentAdminContext.SaveChangesAsync();
            return student.Entity;
        }

        public async  Task<bool> UpdateProfileImage(Guid Id, string profileImageUrl)
        {
            var student = await GetStudentAsync(Id);
            if (student != null)
            {
                student.ProfileImageUrl = profileImageUrl;
                await studentAdminContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
