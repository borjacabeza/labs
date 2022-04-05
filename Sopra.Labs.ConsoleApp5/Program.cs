using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Net.Http.Json;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using Sopra.Labs.Objects;

namespace Sopra.Labs.ConsoleApp5
{
    internal class Program
    {
        private static HttpClient http = new HttpClient();

        private static string clientID = "";
        private static string passKey = "";
        private static string token = "";

        /// <summary>
        /// Inicio del programa
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("".PadRight(56, '*'));
                Console.WriteLine("*  DEMO Y EJERCICIOS".PadRight(55) + "*");
                Console.WriteLine("".PadRight(56, '*'));
                Console.WriteLine("*".PadRight(55) + "*");
                Console.WriteLine("*  1. Geo-localizar dirección IP".PadRight(55) + "*");
                Console.WriteLine("*  2. Información Código Postal".PadRight(55) + "*");
                Console.WriteLine("*  3. Consulta autobuses/parada EMT".PadRight(55) + "*");
                Console.WriteLine("*  4. Consulta plazas libre parkings de Madrid".PadRight(55) + "*");;
                Console.WriteLine("*  9. Salir".PadRight(55) + "*");
                Console.WriteLine("*".PadRight(55) + "*");
                Console.WriteLine("".PadRight(56, '*'));

                Console.WriteLine(Environment.NewLine);
                Console.Write("   Opción: ");

                Console.ForegroundColor = ConsoleColor.Cyan;

                int.TryParse(Console.ReadLine(), out int opcion);
                switch (opcion)
                {
                    case 1:
                        GeoLocationIP();
                        break;
                    case 2:
                        ZipInfo();
                        break;
                    case 3:
                        EMT();
                        break;
                    case 4:
                        Parkings();
                        break;
                    case 9:
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(Environment.NewLine + $"La opción {opcion} no es valida.");
                        break;
                }

                Console.WriteLine(Environment.NewLine);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Pulsa una tecla para continuar...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Geo-localizar una dirección IP
        /// WEB: https://ip-api.com/
        /// </summary>
        static void GeoLocationIP()
        {
            /////////////////////////////////////////////////////////////////////////////////
            // Ejemplo para geo-localizar la dirección ip 193.146.141.207
            /////////////////////////////////////////////////////////////////////////////////
            //  Método: GET
            //     URL: http://ip-api.com/json/<dirección ip>
            // Ejemplo: http://ip-api.com/json/193.146.141.207
            /////////////////////////////////////////////////////////////////////////////////

            // Preparamos el cliente HTTP
            http = new HttpClient();
            http.BaseAddress = new Uri("http://ip-api.com/json/");

            // Preguntamos la dirección IP
            Console.Write("Dirección IP: ");
            string ip = Console.ReadLine();

            // Realizamos una llamada GET al APIRest y recogemos la respuesta 
            HttpResponseMessage response = http.GetAsync(ip).Result;

            // Se analiza el código de estado de la respuesta (HttpStatusCode.OK equivalente a 200)
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                // Leer los datos de la respuesta con formato JSON
                string content = response.Content.ReadAsStringAsync().Result;

                // Mostramos los datos de la respuesta con formato JSON
                Console.WriteLine("Contenido en JSON:");
                Console.WriteLine(content);

                // Deserializamos para tranformar los datos JSON a un objeto
                var data = JsonConvert.DeserializeObject<dynamic>(content);

                // Mostramos los datos de la respuesta
                Console.WriteLine($"Información del {data["post code"]}");
                Console.WriteLine($"{data.country} {data["country abbreviation"]}");

                Console.WriteLine($"------------------------------------------------------");
                Console.WriteLine($" Datos de la dirección IP: {data.query}");
                Console.WriteLine($"------------------------------------------------------");
                Console.WriteLine($" País: {data.country} ({data.countryCode})");
                Console.WriteLine($" Región {data.regionName}");
                Console.WriteLine($" Ciudad: {data.zip} {data.city}");
                Console.WriteLine($" Proveedor: {data.isp}");
                Console.WriteLine($" Posición: {data.lat}, {data.lon}");
                Console.WriteLine($" Registro: {data["as"]}");
            }
            else Console.WriteLine($"Error: {response.StatusCode}");

            Console.ReadKey();
        }

        /// <summary>
        /// Información sobre un código postal
        /// WEB: https://www.zippopotam.us/
        /// </summary>
        static void ZipInfo()
        {
            /////////////////////////////////////////////////////////////////////////////////
            // Ejemplo para geo-localizar la dirección ip 193.146.141.207
            /////////////////////////////////////////////////////////////////////////////////
            //  Método: GET
            //     URL: http://api.zippopotam.us/<código país>/<código postal>
            // Ejemplo: http://api.zippopotam.us/es/28013
            /////////////////////////////////////////////////////////////////////////////////

            // Preparamos el cliente HTTP
            http = new HttpClient();
            http.BaseAddress = new Uri("http://api.zippopotam.us/es/");

            // Preguntamos la dirección IP
            Console.Write("Código Postal: ");

            // Realizamos una llamada GET al APIRest y recogemos la respuesta 
            var response = http.GetAsync(Console.ReadLine()).Result;

            // Se analiza el código de estado de la respuesta (cualquier código de estado entre 200-299)
            if (response.IsSuccessStatusCode)
            {
                // Leer los datos de la respuesta con formato JSON mediante -> response.Content.ReadAsStringAsync().Result
                // Deserializamos para tranformar los datos JSON a un objeto
                var data = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);

                // Mostramos la información directamente desde el objeto
                Console.WriteLine($"------------------------------------------------------");
                Console.WriteLine($" Información del {data["post code"]}");
                Console.WriteLine($"------------------------------------------------------");
                Console.WriteLine($" {data.country} {data["country abbreviation"]}");

                foreach (var item in data.places)
                {
                    Console.WriteLine($"  - {item["place name"]} > {item.state} {item["state abbreviation"]}");
                    Console.WriteLine($"    {item["longitude"]}, {item.latitude}");
                }
            }
            else Console.WriteLine($"Error: {response.StatusCode}");
        }

        /// <summary>
        /// Consultar los próximos autobuses en llegar a una parada en Madrid
        /// WEB: https://apidocs.emtmadrid.es/
        /// </summary>
        static void EMT()
        {
            http = new HttpClient();
            http.BaseAddress = new Uri("https://openapi.emtmadrid.es/v2/");

            try
            {
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                // Proceso de autenticación, obtención del token de acceso
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////

                http.DefaultRequestHeaders.Add("X-ClientId", clientID);
                http.DefaultRequestHeaders.Add("passKey", passKey);

                var response = http.GetAsync("mobilitylabs/user/login").Result;
                if (response.IsSuccessStatusCode)
                {
                    ////// Opción A: Utilizando dynamic /////////////////////////////////////////////////////////////////////////

                    var data = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);
                    token = data["data"][0]["accessToken"];

                    ////// Opción A: Utilizando el objeto EMTLogin //////////////////////////////////////////////////////////////

                    var dataB = JsonConvert.DeserializeObject<EMTLogin>(response.Content.ReadAsStringAsync().Result);
                    token = dataB.Data[0].AccessToken;


                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    // Consulta de próximos autobuses
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //  Método: POST
                    //     URL: https://openapi.emtmadrid.es/v2/transport/busemtmad/stops/<stopId>/arrives/<lineArrive>/
                    // Ejemplo: https://openapi.emtmadrid.es/v2/transport/busemtmad/stops/888/arrives/
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    Console.Clear();
                    Console.Write("Número de Parada: ");
                    string numParada = Console.ReadLine();

                    http.DefaultRequestHeaders.Clear();
                    http.DefaultRequestHeaders.Add("accessToken", token);

                    var content = new StringContent(JsonConvert.SerializeObject(new
                    {
                        cultureInfo = "ES",
                        Text_StopRequired_YN = "Y",
                        Text_EstimationsRequired_YN = "Y",
                        Text_IncidencesRequired_YN = "Y",
                        DateTime_Referenced_Incidencies_YYYYMMDD = DateTime.Now.ToString("yyyyMMdd")
                    }), Encoding.UTF8, "application/json");

                    response = http.PostAsync($"transport/busemtmad/stops/{numParada}/arrives/", content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        data = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);

                        Console.Clear();
                        if (data.code == "00")
                        {
                            Console.WriteLine("===============================================================");
                            Console.WriteLine($"  Información de la Parada {data["data"][0]["StopInfo"][0]["stopId"]}, {data["data"][0]["StopInfo"][0]["stopName"]}");
                            Console.WriteLine("===============================================================");
                            foreach (var item in data["data"][0]["Arrive"])
                            {
                                Console.WriteLine($"  {item.line} " +
                                    $"{(item.DistanceBus > 999 ? "a " + ((decimal)item.DistanceBus / 1000).ToString("N2") + " km." : (item.DistanceBus < 30 ? "en la parada" : item.DistanceBus + " m."))}" +
                                    $"{((item.estimateArrive / 60) < 1 ? "" : " llegara en " + (item.estimateArrive / 60).ToString("N0") + " min.")}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("===============================================================");
                            Console.WriteLine($"  Code: {data.code}");
                            if (data.code == 80) Console.WriteLine($"  {data.description[0].ES}");
                            else Console.WriteLine($"  {data.description}");
                            Console.WriteLine("===============================================================");
                        }
                    }
                    else Console.WriteLine($"Estado: {response.StatusCode}");
                }
                else Console.WriteLine($"Estado: {response.StatusCode}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Consultar el número de plazas libre en parkings de Madrid
        /// WEB: https://apidocs.emtmadrid.es/
        /// </summary>
        static void Parkings()
        {
            http = new HttpClient();
            http.BaseAddress = new Uri("https://openapi.emtmadrid.es/v2/");

            try
            {
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                // Proceso de autenticación, obtención del token de acceso
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////

                http.DefaultRequestHeaders.Add("X-ClientId", clientID);
                http.DefaultRequestHeaders.Add("passKey", passKey);

                var response = http.GetAsync("mobilitylabs/user/login").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);
                    token = data["data"][0]["accessToken"];

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    // Consulta plazas libres en parkings de Madrid
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //  Método: GET
                    //     URL: https://openapi.emtmadrid.es/<version>/citymad/places/parkings/availability/
                    // Ejemplo: https://openapi.emtmadrid.es/v2/citymad/places/parkings/availability/
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    http.DefaultRequestHeaders.Clear();
                    http.DefaultRequestHeaders.Add("accessToken", token);

                    if ((response = http.GetAsync($"citymad/places/parkings/availability/").Result).IsSuccessStatusCode)
                    {
                        var data2 = JsonConvert.DeserializeObject<ParkingResponse>(response.Content.ReadAsStringAsync().Result);

                        Console.Clear();
                        if (data2.Code == "01")
                        {
                            var plazas = data2.Data
                                .Where(r => r.FreeParking != null)
                                .Sum(r => r.FreeParking);

                            Console.WriteLine("===============================================================");
                            Console.WriteLine($"  {plazas} plazas de Parkings - { Convert.ToDateTime(data.datetime).ToString("dd-MM-yyyy hh:mm")}");
                            Console.WriteLine("===============================================================");

                            foreach (var item in data2.Data)
                            {
                                Console.WriteLine($"  {item.Id.ToString().PadLeft(3)} " +
                                    $"{item.Name.PadRight(35)} " +
                                    $"{(item.FreeParking == null ? "      sin datos" : item.FreeParking.ToString().PadLeft(5) + " plazas libres")}");
                            }

                        }
                        else
                        {
                            Console.WriteLine("===============================================================");
                            Console.WriteLine($"  Code: {data.code}");
                            Console.WriteLine($"  {data.description}");
                            Console.WriteLine("===============================================================");
                        }
                    }
                    else Console.WriteLine($"Estado: {response.StatusCode}");
                }
                else Console.WriteLine($"Estado: {response.StatusCode}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

namespace Sopra.Labs.Objects
{
    public class EMTLogin
    {
        public string Code { get; set; }
        public string Description { get; set; }

        [JsonProperty("datetime")]
        public DateTime DateTimeData { get; set; }
        public List<EMTLoginData> Data { get; set; }
    }
    public class EMTLoginData
    {
        public string AccessToken { get; set; }
        public int TokenSecExpiration { get; set; }
    }

    public class ParkingResponse
    {
        public string Code { get; set; }
        public string Description { get; set; }

        [JsonProperty("datetime")]
        public DateTime DateTimeData { get; set; }
        public List<ParkingData> Data { get; set; }
    }
    public class ParkingData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? FreeParking { get; set; }
    }
}
