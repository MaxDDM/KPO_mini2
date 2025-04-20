using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RepositoryInterfaces
{
    public interface StatisticsRepository
    {
        public int GetAnimalCount();
        public List<int> GetFreeEnclosures();
    }
}
