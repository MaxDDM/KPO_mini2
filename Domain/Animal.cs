using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Animal
    {
        public int Id { get; set; }
        [Required]
        public string Kind { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Sex { get; set; }
        [Required]
        public string FavouriteFood { get; set; }
        [Required]
        public string HealthStatus { get; set; }

        public Animal() { }

        public Animal(string kind, string name, DateTime birthDate, string sex, string favouriteFood, string healthStatus)
        {
            Kind = kind;
            Name = name;
            BirthDate = birthDate;
            Sex = sex;
            FavouriteFood = favouriteFood;
            HealthStatus = healthStatus;
        }

        public override string ToString()
        {
            return Id + "_" + Kind + "_" + Name + "_" +
                BirthDate.ToString() + "_" + Sex + "_" +
                FavouriteFood + "_" + HealthStatus;
        }

        public static Animal Parse(string s)
        {
            String[] data = s.Split('_');
            Animal animal = new Animal(data[1], data[2], DateTime.Parse(data[3]), data[4], data[5], data[6]);
            animal.Id = int.Parse(data[0]);
            return animal;
        }
    }
}
