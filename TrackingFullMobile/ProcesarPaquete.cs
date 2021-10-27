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
using Newtonsoft.Json;
using RestSharp;
using ZXing;

namespace TrackingFullMobile
{
    [Activity(Label = "ProcesarPaquete")]
    public class ProcesarPaquete : Activity,IPermissionListener
    {
        private ZXingScannerView scannerView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Escaner);

            scannerView = FindViewById<ZXingScannerView>(Resource.Id.zxscan);

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
            private ProcesarPaquete escanear;

            public MyResultHandler(ProcesarPaquete escanear)
            {
                this.escanear = escanear;
            }

            public void HandleResult(ZXing.Result rawResult)
            {
                int result = Int32.Parse(rawResult.Text);
                Procesar(result);
                escanear.StartActivity(typeof(MenuPrincipal_activity));
            }

            public string Procesar(int id)
            {
                var client = new RestClient("https://apirest20191105064440.azurewebsites.net/api/Paquete/AvanzarEstado?id=" + id);
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
                    return "ERROR";
                }
            }
        }
    }
}