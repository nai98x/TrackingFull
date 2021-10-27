using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using RestSharp;
using Shared.Entities;

namespace BusinessLogicLayer
{
    public class BLPaquetes : IBLPaquetes
    {
        private IDALPaquetes dal_paq;
        private IDALTrayectos dal_tr;
        private IDALUsuarios dal_usu;

        public BLPaquetes()
        {
            dal_paq = new DALPaquetes_EF();
            dal_tr = new DALTrayectos_EF();
            dal_usu = new DALUsuarios_EF();
        }

        internal static string GetRandomString(int stringLength)
        {
            StringBuilder sb = new StringBuilder();
            int numGuidsToConcat = (((stringLength - 1) / 32) + 1);
            for (int i = 1; i <= numGuidsToConcat; i++)
            {
                sb.Append(Guid.NewGuid().ToString("N"));
            }

            return sb.ToString(0, stringLength);
        }

        public void AddPaquete(Paquete paq)
        {
            Usuario destinatario;
            if(paq.CodigoExterno == null)
            {
                destinatario = dal_usu.getUsuario(paq.Destinatario.Id);
            }
            else
            {
                destinatario = paq.Destinatario;
            }
            var client = new RestClient("https://api.sendinblue.com/v3/smtp/email");
            var request = new RestRequest(Method.POST);
            request.AddHeader("accept", "application/json");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("api-key", "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");
            request.AddParameter("application/json", "{\"sender\":{\"name\":\"TrackingFULL\",\"email\":\"no-reply@trackingfull.com\"},\"to\":[{\"email\":\"" + destinatario.Email + "\",\"name\":\"" + destinatario.Nombre + "\"}],\"textContent\":\"Le avisamos que su paquete " + paq.Descripcion + " ya ha sido dado de alta.\",\"subject\":\"TrackingFULL - Alta de paquete\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            paq.Codigo = GetRandomString(6);
            paq.PDCActual = dal_tr.getPuntosDeControl(paq.Trayecto.Id).First();
            dal_paq.AddPaquete(paq);
        }

        public void DeletePaquete(int id)
        {
            Paquete paq = dal_paq.GetPaquete(id);
            Usuario destinatario = dal_usu.getUsuario(paq.Destinatario.Id);
            var client = new RestClient("https://api.sendinblue.com/v3/smtp/email");
            var request = new RestRequest(Method.POST);
            request.AddHeader("accept", "application/json");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("api-key", "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");
            request.AddParameter("application/json", "{\"sender\":{\"name\":\"TrackingFULL\",\"email\":\"no-reply@trackingfull.com\"},\"to\":[{\"email\":\"" + destinatario.Email + "\",\"name\":\"" + destinatario.Nombre + "\"}],\"textContent\":\"Le avisamos que su paquete " + paq.Descripcion + " ya ha sido dado de baja.\",\"subject\":\"TrackingFULL - Alta de paquete\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            dal_paq.DeletePaquete(id);
        }

        public List<Paquete> GetAllPaquetes()
        {
            return dal_paq.GetAllPaquetes();
        }

        public Paquete GetPaquete(int id)
        {
            return dal_paq.GetPaquete(id);
        }

        public Paquete GetPaqueteByCodigoExterno(string codigo)
        {
            return dal_paq.GetPaqueteByCodigoExterno(codigo);
        }

        public void UpdatePaquete(Paquete paq)
        {
            dal_paq.UpdatePaquete(paq);
        }
    }
}
