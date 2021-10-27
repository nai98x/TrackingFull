using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer;
using Shared.Entities;
using System.ServiceModel;

namespace ServiceLayer
{
    class ServicePaqueteProxy : ClientBase<IServicePaquete>, IServicePaquete
    {
        public void AddPaquete(Paquete paq)
        {
            base.Channel.AddPaquete(paq);
        }

        public void DeletePaquete(int id)
        {
            base.Channel.DeletePaquete(id);
        }

        public List<Paquete> GetAllPaquetes()
        {
            return base.Channel.GetAllPaquetes();
        }

        public Paquete GetPaquete(int id)
        {
            return base.Channel.GetPaquete(id);
        }

        public Paquete GetPaqueteByCodigoExterno(string codigo)
        {
            return base.Channel.GetPaqueteByCodigoExterno(codigo);
        }

        public void UpdatePaquete(Paquete paq)
        {
            base.Channel.UpdatePaquete(paq);
        }
    }
}
