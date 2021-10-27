using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Karumi.Dexter;
using Com.Karumi.Dexter.Listener;
using Com.Karumi.Dexter.Listener.Single;
using EDMTDev.ZXingXamarinAndroid;
using RestSharp;
using ZXing;

namespace TrackingFullMobile
{
    [Activity(Label = "FinalizarEntrega")]
    public class FinalizarEntrega : Activity, IPermissionListener
    {
        ZXingScannerView scannerView;
        EditText txtCodigo;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FinalizarEntrega);

            scannerView = FindViewById<ZXingScannerView>(Resource.Id.zxscan);
            txtCodigo = FindViewById<EditText>(Resource.Id.editCodigo);

            //Permisos
            Dexter.WithActivity(this)
                .WithPermission(Manifest.Permission.Camera)
                .WithListener(this)
                .Check();
        }

        protected override void OnDestroy()
        {
            scannerView.StopCamera();
            base.OnDestroy();
        }

        public void OnPermissionDenied(PermissionDeniedResponse p0)
        {
            Toast.MakeText(this, "No tienes permisos", ToastLength.Long).Show();
        }

        public void OnPermissionGranted(PermissionGrantedResponse p0)
        {
            scannerView.SetResultHandler(new MyResultHandler(this));
            scannerView.StartCamera();
        }

        public void OnPermissionRationaleShouldBeShown(PermissionRequest p0, IPermissionToken p1)
        {

        }

        private class MyResultHandler : IResultHandler
        {
            private FinalizarEntrega escanear;

            public MyResultHandler(FinalizarEntrega escanear)
            {
                this.escanear = escanear;
            }

            public void HandleResult(ZXing.Result rawResult)
            {
                int id = Int32.Parse(rawResult.Text);
                Finalizar(id, escanear.txtCodigo.Text);
                escanear.StartActivity(typeof(MenuPrincipal_activity));
            }

            public string Finalizar(int id, string codigo)
            {
                var client = new RestClient("https://apirest20191105064440.azurewebsites.net/api/Paquete/FinalizarEntrega?id=" + id + "&codigo=" + codigo);
                var request = new RestRequest(Method.PUT);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", "Bearer " + Global.TokenAPI);

                IRestResponse Res = client.Execute(request);

                if (Res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return "OK";
                }
                else
                {
                    return "Error en la verificación, el código del cliente no es válido";
                }
            }
        }
    }
}