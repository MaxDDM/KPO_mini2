using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Feeding
    {
        public int Id { get; set; }
        [Required]
        public int AnimalId { get; set; }
        [Required]
        public string Time { get; set; }
        [Required]
        public string TypeOfFood { get; set; }

        public Feeding() { }

        public Feeding(int animalId, string time, string typeOfFood)
        {
            AnimalId = animalId;
            Time = time;
            TypeOfFood = typeOfFood;
        }

        public override string ToString()
        {
            return Id + ";" + AnimalId + ";" + Time + ";" + TypeOfFood;
        }

        public static Feeding Parse(string s)
        {
            String[] data = s.Split(';');
            Feeding feeding = new Feeding(int.Parse(data[1]), data[2], data[3]);
            feeding.Id = int.Parse(data[0]);
            return feeding;
        }
    }
}
