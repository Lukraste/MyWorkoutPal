using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWorkoutPal.Models;

namespace MyWorkoutPal.Services
{
    public interface IDataService
    {
        Task<List<Exercice>> GetAllExercicesAsync();
        Task AddExerciceAsync(Exercice exercice);
        Task UpdateExerciceAsync(Exercice exercice);
        Task DeleteExerciceAsync(int id);
    }
}
