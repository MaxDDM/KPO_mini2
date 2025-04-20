using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain
{
    public class Enclosure
    {
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int Size { get; set; }
        public int AnimalsCount { get; set; }
        [Required]
        public int Capacity { get; set; }
        public List<Animal> Animals { get; set; } = new List<Animal>();

        public Enclosure() { }

        public Enclosure(String type, int size, int animalsCount, int capacity, List<Animal> animals)
        {
            Type = type;
            Size = size;
            AnimalsCount = animalsCount;
            Capacity = capacity;
            Animals = animals;
        }

        public override string ToString()
        {
            String res = Id + "-" + Type + "-" + Size + "-" +
                AnimalsCount + "-" + Capacity;
            for (int i = 0; i < Animals.Count; ++i)
            {
                res += "-" + Animals[i].ToString();
            }
            return res;
        }

        public static Enclosure Parse(string s)
        {
            String[] data = s.Split('-');
            List<Animal> animals = new List<Animal>();
            for (int i = 5; i < data.Length; ++i)
            {
                animals.Add(Animal.Parse(data[i]));
            }
            Enclosure enc = new Enclosure(data[1], int.Parse(data[2]), int.Parse(data[3]), int.Parse(data[4]), animals);
            enc.Id = int.Parse(data[0]);
            return enc;
        }
    }
}
