using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Domain.Entities;

namespace Domain.Managers.Pagination
{
    public class PaginationManager
    {
        private IData data;

        public PaginationManager(IData d)
        {
            data = d;
        }

        public IEnumerable<Program> FormProgramsPage(int pageSize, int page)
        {
            /*
             * Админ может обновить программы, так что нужно получать акутальные
             */
            data.Reload();
            return data.Programs.Data.OrderBy(x => x.Name).
                Skip((page - 1)*pageSize).
                Take(pageSize);
        }


        public IEnumerable<Robot> FormMyRobotsPage(IEnumerable<Robot> robots, int pageSize, int page) =>
            robots.OrderBy(x => x.RobotID).
                Skip((page - 1)*pageSize).
                Take(pageSize);
    }
}
