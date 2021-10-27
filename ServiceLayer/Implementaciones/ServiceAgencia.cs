using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer;
using Shared.Entities;

namespace ServiceLayer
{
    class ServiceAgencia : IServiceAgencia
    {
        private static IBLAgencias blHandler;

        public ServiceAgencia()
        {
            blHandler = Program.blHandlerAG;
        }

        public void AddAgencia(Agencia ag)
        {
            blHandler.AddAgencia(ag);
        }

        public void DeleteAgencia(int id)
        {
            blHandler.DeleteAgencia(id);
        }

        public Agencia GetAgencia(int id)
        {
            return blHandler.GetAgencia(id);
        }

        public Agencia GetAgenciaByCodigoExterno(string codigo)
        {
            return blHandler.GetAgenciaByCodigoExterno(codigo);
        }

        public List<Agencia> GetAllAgencias()
        {
            return blHandler.GetAllAgencias();
        }

        public void UpdateAgencia(Agencia ag)
        {
            blHandler.UpdateAgencia(ag);
        }
    }
}
