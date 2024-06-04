using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Junio3Brandon
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            IniciarApi();

        }



        public async Task IniciarApi()
        {
            HttpClient httpClient = new HttpClient();
            String url = "https://www.inegi.org.mx/app/api/denue/v1/consulta/Buscar/camiones/21.85717833,-102.28487238/250/a2b1b5da-d2b7-4a02-82c7-d2d3237baa5c";


            try
            {
                // Realizar la solicitud GET
                HttpResponseMessage response = await httpClient.GetAsync(url);

                // Verificar si la solicitud fue exitosa
                if (response.IsSuccessStatusCode)
                {
                    // Leer y mostrar la respuesta
                    string responseData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Respuesta de la API:");
                    Console.WriteLine(responseData);
                }
                else
                {
                    Console.WriteLine($"Error en la solicitud: {response.StatusCode}");
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error en la solicitud: {e.Message}");
            }



        }





        public async Task getLocacion()
        {
            try
            {
                // Definir la solicitud de ubicación con alta precisión
                var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    lbllatitud.Text = location.Latitude.ToString();
                    lbllongitud.Text = location.Longitude.ToString();
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}");
               
                }
                else
                {
                    Console.WriteLine("No se pudo obtener la ubicación");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Manejar excepción para características no soportadas en el dispositivo
                Console.WriteLine($"Característica no soportada: {fnsEx.Message}");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Manejar excepción para características deshabilitadas en el dispositivo
                Console.WriteLine($"Característica deshabilitada: {fneEx.Message}");
            }
            catch (PermissionException pEx)
            {
                // Manejar excepción para permisos denegados
                Console.WriteLine($"Permiso denegado: {pEx.Message}");
            }
            catch (Exception ex)
            {
                // Manejar cualquier otra excepción
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
