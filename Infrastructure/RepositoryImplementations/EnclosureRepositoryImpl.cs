using Domain;
using Infrastructure.RepositoryInterfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RepositoryImplementations
{
    public class EnclosureRepositoryImpl : EnclosureRepository
    {
        ConnectionMultiplexer _connection;
        IDatabase _database;

        public EnclosureRepositoryImpl()
        {
            _connection = ConnectionMultiplexer.Connect("localhost");
            _database = _connection.GetDatabase();
        }

        public void DeleteEnclosure(int id)
        {
            _database.KeyDelete(id.ToString());
        }

        public Enclosure GetEnclosure(int id)
        {

            String enclosure = _database.StringGet(id.ToString());
            if (enclosure == null || !enclosure.Contains('-'))
            {
                return null;
            }
            return Enclosure.Parse(enclosure);
        }

        public void AddEnclosure(Enclosure enclosure)
        {
            _database.StringSet(ObjectsCounter.Count.ToString(), enclosure.ToString());
        }

        public void ChangeEnclosure(int id, Enclosure enclosure)
        {
            _database.StringSet(id.ToString(), enclosure.ToString());
        }

        public void DeleteFromAllEnclosures(int id)
        {
            for (int i = 1; i < ObjectsCounter.Count + 1; ++i)
            {
                string enclosure = _database.StringGet(i.ToString());
                if (enclosure != null && enclosure.Contains('-'))
                {
                    Enclosure enc = Enclosure.Parse(enclosure);
                    enc.Animals.RemoveAll(animal => animal.Id ==id);
                    ChangeEnclosure(enc.Id, enc);
                }
            }
        }
    }
}
