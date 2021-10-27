using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TrackingFullMobile
{
    [Activity(Label = "MenuPrincipal_activity")]
    public class MenuPrincipal_activity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Button cerrarsession;
            Button procesar;
            Button anular;
            Button finalizar;
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.MenuPrincipal);
            // Get our button from the layout resource,  
            // and attach an event to it  
            cerrarsession = FindViewById<Button>(Resource.Id.buttonCerrarSesion);
            procesar = FindViewById<Button>(Resource.Id.buttonProcesarPaquete);
            anular = FindViewById<Button>(Resource.Id.buttonAnularProcesamientoDePaquete);
            finalizar = FindViewById<Button>(Resource.Id.buttonFinalizarEntregaDePaquete);
            cerrarsession.Click += CerrarSesion;
            procesar.Click += Procesar;
            anular.Click += Retroceder;
            finalizar.Click += Finalizar;
        }
        private void CerrarSesion(object sender, EventArgs e)
        {
            Global.Rol = null;
            Global.TokenAPI = null;
            StartActivity(typeof(MainActivity));
            Toast.MakeText(this, "Sesión cerrada con éxito", ToastLength.Long).Show();
        }
        private void Procesar(object sender, EventArgs e)
        {
            StartActivity(typeof(ProcesarPaquete));
        }
        private void Retroceder(object sender, EventArgs e)
        {
            StartActivity(typeof(RetrocederPaquete));
        }
        private void Finalizar(object sender, EventArgs e)
        {
            StartActivity(typeof(FinalizarEntrega));
        }
    }
}