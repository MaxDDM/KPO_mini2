using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RepositoryInterfaces
{
    public interface AnimalRepository
    {
        public Animal GetAnimal(int id);
        public void DeleteAnimal(int id);
        public void AddAnimal(Animal animal);
    }
}
