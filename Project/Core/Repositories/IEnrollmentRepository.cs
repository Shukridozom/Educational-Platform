using Project.Core.Domains;

namespace Project.Core.Repositories
{
    public interface IEnrollmentRepository : IRepository<Enrollment>
    {
        IList<int> GetStudentEnrollmentsKeyList(int userId);
        double GetSystemProfit();

    }
}
