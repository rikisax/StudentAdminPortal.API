using StudentAdminPortal.API.DataModels;



namespace StudentAdminPortal.API.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudentsAsync();
        Task<Student> GetStudentAsync(Guid Id);
        Task<List<Gender>> GetGenderAsync();
        Task<bool> Exists(Guid studentId);
        Task<Student> UpdateStudent(Guid studentId, Student request);
        Task<Student> DeleteStudent(Guid studentId);
        Task<Student> AddStudent(Student student);
        Task<bool> UpdateProfileImage(Guid Id, string profileImageUrl);
    }
}
