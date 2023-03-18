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
        // Définition de l'interface pour les méthodes servant d'opération CRUD
        Task<List<Exercice>> GetAllExercicesAsync();
        Task AddExerciceAsync(Exercice exercice);
        Task UpdateExerciceAsync(Exercice exercice);
        Task DeleteExerciceAsync(int id);
    }
}
