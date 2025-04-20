using Domain;
using Infrastructure.RepositoryInterfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RepositoryImplementations
{
    public class StatisticsRepositoryImpl : StatisticsRepository
    {
        ConnectionMultiplexer _connection;
        IDatabase _database;

        public StatisticsRepositoryImpl()
        {
            _connection = ConnectionMultiplexer.Connect("localhost");
            _database = _connection.GetDatabase();
        }
        public int GetAnimalCount()
        {
            int counter = 0;
            for (int i = 1; i < ObjectsCounter.Count + 1; ++i)
            {
                string animal = _database.StringGet(i.ToString());
                if (animal != null && animal.Contains('_') && !animal.Contains('-'))
                {
                    ++counter;
                }
            }
            return counter;
        }

        public Feeding GetFeeding()
        {
            throw new NotImplementedException();
        }

        public List<int> GetFreeEnclosures()
        {
            List<int> freeEnclosures = new List<int>();
            for (int i = 1; i < ObjectsCounter.Count + 1; ++i)
            {
                string enclosure = _database.StringGet(i.ToString());
                if (enclosure != null && enclosure.Contains('-'))
                {
                    Enclosure enc = Enclosure.Parse(enclosure);
                    if (enc.AnimalsCount < enc.Capacity)
                    {
                        freeEnclosures.Add(enc.Id);
                    }
                }
            }
            return freeEnclosures;
        }
    }
}
