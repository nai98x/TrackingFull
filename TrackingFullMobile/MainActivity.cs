using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using RestSharp;
using System.Configuration;
using System.Text;
using Newtonsoft.Json;

namespace TrackingFullMobile
{
    [Activity(Label = "TrackingFullMobile", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {
        EditText txtUsername;
        EditText txtPassword;
        Button btnsign;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.activity_main);
            // Get our button from the layout resource,  
            // and attach an event to it  
            btnsign = FindViewById<Button>(Resource.Id.buttonLogin);
            txtUsername = FindViewById<EditText>(Resource.Id.editUser);
            txtPassword = FindViewById<EditText>(Resource.Id.editPass);
            btnsign.Click += Btnsign_Click;
        }

        private void Btnsign_Click(object sender, EventArgs e)
        {
            var client = new RestClient("https://apirest20191105064440.azurewebsites.net/api/login/authenticateAPP");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            string email = txtUsername.Text;
            string pass = txtPassword.Text;
            var body = new
            {
                Username = email,
                Password = pass
            };
            request.AddJsonBody(body);
            IRestResponse Res = client.Execute(request);

            if (Res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var EmpResponse = Res.Content;
                String[] response = JsonConvert.DeserializeObject<String[]>(EmpResponse);
                Global.Rol = response[0];
                Global.TokenAPI = response[1];
                Toast.MakeText(this, "Sesión iniciada con éxito", ToastLength.Long).Show();
                StartActivity(typeof(MenuPrincipal_activity));
            }
            else
            {
                Toast.MakeText(this, "Error al iniciar sesión, la combinación de contraseña y email no es correcta.", ToastLength.Long).Show();
            }
        }
    }
}