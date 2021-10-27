using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer;
using Shared.Entities;

namespace ServiceLayer
{
    class ServicePaquete : IServicePaquete
    {
        private static IBLPaquetes blHandler;

        public ServicePaquete()
        {
            blHandler = Program.blHandlerPaq;
        }

        public void AddPaquete(Paquete paq)
        {
            blHandler.AddPaquete(paq);
        }

        public void DeletePaquete(int id)
        {
            blHandler.DeletePaquete(id);
        }

        public List<Paquete> GetAllPaquetes()
        {
            return blHandler.GetAllPaquetes();
        }

        public Paquete GetPaquete(int id)
        {
            return blHandler.GetPaquete(id);
        }

        public Paquete GetPaqueteByCodigoExterno(string codigo)
        {
            return blHandler.GetPaqueteByCodigoExterno(codigo);
        }

        public void UpdatePaquete(Paquete paq)
        {
            blHandler.UpdatePaquete(paq);
        }
    }
}
