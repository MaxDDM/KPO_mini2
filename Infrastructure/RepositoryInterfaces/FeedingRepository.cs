using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Infrastructure.RepositoryInterfaces
{
    public interface FeedingRepository
    {
        public Feeding GetFeeding(int id);
        public void DeleteFeeding(int id);
        public void AddFeeding(Feeding schedule);
        public List<Feeding> GetAllFeedings();
    }
}
