using Project.Core.Domains;
using Project.Core.Repositories;

namespace Project.Persistence.Repositories
{
    public class LectureRepository : Repository<Lecture>, ILectureRepository
    {
        public LectureRepository(AppDbContext context)
            :base(context)
        {

        }
    }
}
