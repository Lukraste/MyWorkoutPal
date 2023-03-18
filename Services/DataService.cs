using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MyWorkoutPal.Models;

namespace MyWorkoutPal.Services
{
    public class DataService : IDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public DataService(HttpClient httpClient)
        {
            // Définition de l'url de la requête HTTP pour l'utilisation de l'API

            _httpClient = httpClient;
            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5272" : "https://localhost:7272";
            _url = $"{_baseAddress}/api/exercices";

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        // Méthode asynchrone pour ajouter un exercice via POST (API)

        public async Task AddExerciceAsync(Exercice exercice)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("Pas de connexion internet..");
                return;
            }
            try
            {
                string jsonExercice = JsonSerializer.Serialize<Exercice>(exercice, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonExercice, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/add", content);

                if(response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Exercice créé avec succès");
                }
                else
                {
                    Debug.WriteLine("Pas de réponse Http");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
            }
            return;
        }

        // Méthode asynchrone pour supprimer un exercice via DELETE (API)

        public async Task DeleteExerciceAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("Pas de connexion internet.");
                return;
            }
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/delete/{id}");

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Exercice supprimé avec succès");
                }
                else
                {
                    Debug.WriteLine("Pas de réponse Http");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
            }
            return;
        }

        // Méthode asynchrone pour récupérer tous les exercices via GET (API)

        public async Task<List<Exercice>> GetAllExercicesAsync()
        {
            List<Exercice> exercices = new List<Exercice>();

            if(Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("Pas de connexion internet..");
                return exercices;
            }
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}");

                if(response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    exercices = JsonSerializer.Deserialize<List<Exercice>>(content, _jsonSerializerOptions);
                }
                else
                {
                    Debug.WriteLine("Pas de réponse Http");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
            }

            return exercices;
        }

        // Méthode asynchrone pour mettre à jour un exercice un exercice via PUT (API)

        public async Task UpdateExerciceAsync(Exercice exercice)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("Pas de connexion internet..");
                return;
            }
            try
            {
                string jsonExercice = JsonSerializer.Serialize<Exercice>(exercice, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonExercice, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/update/{exercice.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Exercice mit à jour avec succès");
                }
                else
                {
                    Debug.WriteLine("Pas de réponse Http");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
            }
            return;
        }
    }
}
