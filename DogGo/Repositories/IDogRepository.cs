using DogGo.Models;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public interface IDogRepository
    {
        List<Dog> GetAllDogs();

        void AddDog(Dog dog);
        Dog GetDogById(int id);
        void UpdateDog(Dog dog);
        void DeleteDog(int id);
        List<Dog> GetDogsByOwnerId(int ownerId);
    }
}