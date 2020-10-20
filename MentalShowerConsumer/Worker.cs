using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ModelLib.Models;
using Newtonsoft.Json;

namespace MentalShowerConsumer
{
    public class Worker
    {
        string URI = "https://localhost:5001/api/IndoorClimate";
        public void Start()
        {
            Console.WriteLine(string.Join("\n", GetAllDataAsync().Result));
            while (true)
            {
            Console.WriteLine("Vælg ID:");
            var id = Console.ReadLine();
            GetDataById(Convert.ToInt32(id));

            }
        }

        public async Task<IList<SensorDataModel>> GetAllDataAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(URI);
                IList<SensorDataModel> cList = JsonConvert.DeserializeObject<IList<SensorDataModel>>(content);
                return cList;
            }
        }

        public async Task<SensorDataModel> GetDataById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync($"{URI}/{id}");
                SensorDataModel data = JsonConvert.DeserializeObject<SensorDataModel>(content);
                return data;
            }
        }







    }
}
