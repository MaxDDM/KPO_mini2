using Domain;
using Infrastructure.RepositoryInterfaces;
using StackExchange.Redis;

namespace Infrastructure.RepositoryImplementations
{
    public class AnimalRepositoryImpl : AnimalRepository
    {
        ConnectionMultiplexer _connection;
        IDatabase _database;

        public AnimalRepositoryImpl()
        {
            _connection = ConnectionMultiplexer.Connect("localhost");
            _database = _connection.GetDatabase();
        }

        public void DeleteAnimal(int id)
        {
            _database.KeyDelete(id.ToString());
        }

        public Animal GetAnimal(int id)
        {
            string animal = _database.StringGet(id.ToString());
            if (animal == null || !animal.Contains('_') || animal.Contains('-'))
            {
                return null;
            }
            return Animal.Parse(animal);
        }

        public void AddAnimal(Animal animal)
        {
            _database.StringSet(ObjectsCounter.Count.ToString(), animal.ToString());
        }
    }
}
