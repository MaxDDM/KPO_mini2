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
    public class FeedingRepositoryImpl : FeedingRepository
    {
        ConnectionMultiplexer _connection;
        IDatabase _database;

        public FeedingRepositoryImpl()
        {
            _connection = ConnectionMultiplexer.Connect("localhost");
            _database = _connection.GetDatabase();
        }

        public void AddFeeding(Feeding feeding)
        {
            _database.StringSet(ObjectsCounter.Count.ToString(), feeding.ToString());
        }

        public void DeleteFeeding(int id)
        {
            _database.KeyDelete(id.ToString());
        }

        public List<Feeding> GetAllFeedings()
        {
            List<Feeding> feedings = new List<Feeding>();
            for (int i = 1; i < ObjectsCounter.Count + 1; ++i)
            {
                string feeding = _database.StringGet(i.ToString());
                if (feeding != null && feeding.Contains(';'))
                {
                    feedings.Add(Feeding.Parse(feeding));
                }
            }
            return feedings;
        }

        public Feeding GetFeeding(int id)
        {
            string feeding = _database.StringGet(id.ToString());
            if (feeding == null || !feeding.Contains(';'))
            {
                return null;
            }
            return Feeding.Parse(feeding);
        }
    }
}
