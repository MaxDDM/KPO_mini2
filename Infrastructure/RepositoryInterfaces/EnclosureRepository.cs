using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RepositoryInterfaces
{
    public interface EnclosureRepository
    { 
        public Enclosure GetEnclosure(int id);
        public void DeleteEnclosure(int id);
        public void AddEnclosure(Enclosure enclosure);
        public void ChangeEnclosure(int id, Enclosure enclosure);
        public void DeleteFromAllEnclosures(int id);
    }
}
