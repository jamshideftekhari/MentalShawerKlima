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
        string URI = "https://localhost:44367/api/IndoorClimate";
        public void Start()
        {
            PostAsync(new SensorDataModel(1, "D3.05", 20, 30, 45, 2));
            PostAsync(new SensorDataModel(2, "D3.06", 23, 20, 35, 15));
            PostAsync(new SensorDataModel(3, "D3.07", 27, 50, 25, 13));
            Console.WriteLine(string.Join("\n", GetAllDataAsync().Result));
            
            Console.WriteLine("Vælg ID to get:");
            var id = Console.ReadLine();
            Console.WriteLine(GetDataById(Convert.ToInt32(id)).Result);

            Console.WriteLine("Vælg ID to delete:");
            id = Console.ReadLine();
            Console.WriteLine(DeleteAsync(Convert.ToInt32(id)).Result ? "Deleted" : "Not deleted!");

            Console.WriteLine("Vælg ID to put:");
            id = Console.ReadLine();
            Console.WriteLine(PutAsync(Convert.ToInt32(id),new SensorDataModel(Convert.ToInt32(id),"d3.09",13,33,00,99)).Result ? "Putted" : "Not putted!");


            Console.WriteLine("All sensors");
            Console.WriteLine(string.Join("\n", GetAllDataAsync().Result));
            if (DeleteAsync(1).Result)
            {
                Console.WriteLine("id 1 deleted");
            }
            else
            {
                Console.WriteLine("Error");
            }
            if (DeleteAsync(2).Result)
            {
                Console.WriteLine("id 2 deleted");
            }
            else
            {
                Console.WriteLine("Error");
            }
            if (DeleteAsync(3).Result)
            {
                Console.WriteLine("id 3 deleted");
            }
            else
            {
                Console.WriteLine("Error");
            }
            Console.WriteLine("All sensors deleted!:");
            Console.WriteLine(string.Join("\n", GetAllDataAsync().Result));
            Console.WriteLine("DONE");
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

        public async Task<bool> PutAsync(int id, SensorDataModel value)
        {
            using (HttpClient client = new HttpClient())
            {
                SensorDataModel fan = await GetDataById(id);
                if (fan != null)
                {
                    fan.CO2 = value.CO2;
                    fan.Humidity = value.Humidity;
                    fan.RoomID = value.RoomID;
                    fan.SensorID = value.SensorID;
                    fan.Temperature = value.Temperature;
                    fan.Pressure = value.Pressure;
                }

                string postBody = JsonConvert.SerializeObject(value);
                StringContent stringContent = new StringContent(postBody, Encoding.UTF8, "application/json");
                await client.PutAsync($"{URI}/{id}", stringContent);
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                await client.DeleteAsync($"{URI}/{id}");
                return true;
            }

            return false;
        }

        public async void PostAsync(SensorDataModel value)
        {
            using (HttpClient client = new HttpClient())
            {
                string postBody = JsonConvert.SerializeObject(value);
                StringContent stringContent = new StringContent(postBody, Encoding.UTF8, "application/json");
                await client.PostAsync(URI, stringContent);
            }
        }



    }
}
