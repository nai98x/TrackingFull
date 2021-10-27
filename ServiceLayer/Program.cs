using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    class Program
    {
        public static IBLCasosDeUso blHandler;
        public static IBLAgencias blHandlerAG;
        public static IBLPaquetes blHandlerPaq;
        public static IBLUsuarios blHandlerUsu;
        public static IBLTrayectos blHandlerTr;
        public static IBLPuntosDeControl blHandlerPdc;

        static void Main(string[] args)
        {
            SetupDependencies();
            SetupService();
        }

        private static void SetupDependencies()
        {
            blHandler = new BLCasosDeUso();
            blHandlerAG = new BLAgencias();
            blHandlerPaq = new BLPaquetes();
            blHandlerUsu = new BLUsuarios();
            blHandlerTr = new BLTrayectos();
            blHandlerPdc = new BLPuntosDeControl();
        }

        private static void SetupService()
        {
            //Create a URI to serve as the base address
            Uri httpUrl = new Uri("http://localhost:8834/tf-soap-ag");
            Uri httpUrl2 = new Uri("http://localhost:8834/tf-soap-cu");
            Uri httpUrl3 = new Uri("http://localhost:8834/tf-soap-paq");
            Uri httpUrl4 = new Uri("http://localhost:8834/tf-soap-pdc");
            Uri httpUrl5 = new Uri("http://localhost:8834/tf-soap-tr");
            Uri httpUrl6 = new Uri("http://localhost:8834/tf-soap-usu");
            //Create ServiceHost
            ServiceHost hostag = new ServiceHost(typeof(ServiceAgencia), httpUrl);
            ServiceHost hostcu = new ServiceHost(typeof(ServiceCasosDeUso), httpUrl2);
            ServiceHost hostpaq = new ServiceHost(typeof(ServicePaquete), httpUrl3);
            ServiceHost hostpdc = new ServiceHost(typeof(ServicePuntosDeControl), httpUrl4);
            ServiceHost hosttr = new ServiceHost(typeof(ServiceTrayecto), httpUrl5);
            ServiceHost hostusu = new ServiceHost(typeof(ServiceUsuario), httpUrl6);
            //Add a service endpoint
            hostag.AddServiceEndpoint(typeof(IServiceAgencia), new WSHttpBinding(), "");
            hostcu.AddServiceEndpoint(typeof(IServiceCasosDeUso), new WSHttpBinding(), "");
            hostpaq.AddServiceEndpoint(typeof(IServicePaquete), new WSHttpBinding(), "");
            hostpdc.AddServiceEndpoint(typeof(IServicePuntosDeControl), new WSHttpBinding(), "");
            hosttr.AddServiceEndpoint(typeof(IServiceTrayecto), new WSHttpBinding(), "");
            hostusu.AddServiceEndpoint(typeof(IServiceUsuario), new WSHttpBinding(), "");
            //Enable metadata exchange
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            hostag.Description.Behaviors.Add(smb);
            hostcu.Description.Behaviors.Add(smb);
            hostpaq.Description.Behaviors.Add(smb);
            hostpdc.Description.Behaviors.Add(smb);
            hosttr.Description.Behaviors.Add(smb);
            hostusu.Description.Behaviors.Add(smb);
            //Start the Service
            hostag.Open();
            hostcu.Open();
            hostpaq.Open();
            hostpdc.Open();
            hosttr.Open();
            hostusu.Open();

            Console.WriteLine("Service is host at " + DateTime.Now.ToString());
            Console.WriteLine("Host is running... Press <Enter> key to stop");
            Console.ReadLine();
        }
    }
}
