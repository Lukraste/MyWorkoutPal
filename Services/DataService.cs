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
            _httpClient = httpClient;
            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5272" : "https://localhost:7272";
            _url = $"{_baseAddress}/api/exercices";

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task AddExerciceAsync(Exercice exercice)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> No internet access...");
                return;
            }
            try
            {
                string jsonExercice = JsonSerializer.Serialize<Exercice>(exercice, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonExercice, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/add", content);

                if(response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Successfully created Exercice");
                }
                else
                {
                    Debug.WriteLine("---> Non Http 2xx response");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Whoops exception: {ex.Message}");
            }
            return;
        }

        public async Task DeleteExerciceAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> No internet access...");
                return;
            }
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/delete/{id}");

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Successfully created Exercice");
                }
                else
                {
                    Debug.WriteLine("---> Non Http 2xx response");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Whoops exception: {ex.Message}");
            }
            return;
        }

        public async Task<List<Exercice>> GetAllExercicesAsync()
        {
            List<Exercice> exercices = new List<Exercice>();

            if(Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> No internet access...");
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
                    Debug.WriteLine("---> Non Http 2xx response");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Whoops exception: {ex.Message}");
            }

            return exercices;
        }

        public async Task UpdateExerciceAsync(Exercice exercice)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> No internet access...");
                return;
            }

            try
            {
                string jsonExercice = JsonSerializer.Serialize<Exercice>(exercice, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonExercice, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/update/{exercice.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Successfully created ToDo");
                }
                else
                {
                    Debug.WriteLine("---> Non Http 2xx response");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Whoops exception: {ex.Message}");
            }

            return;
        }
    }
}
