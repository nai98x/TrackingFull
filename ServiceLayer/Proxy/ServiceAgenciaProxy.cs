using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer;
using Shared.Entities;

namespace ServiceLayer
{
    class ServiceAgenciaProxy : ClientBase<IServiceAgencia>, IServiceAgencia
    {
        public void AddAgencia(Agencia ag)
        {
            base.Channel.AddAgencia(ag);
        }

        public void DeleteAgencia(int id)
        {
            base.Channel.DeleteAgencia(id);
        }

        public Agencia GetAgencia(int id)
        {
            return base.Channel.GetAgencia(id);
        }

        public Agencia GetAgenciaByCodigoExterno(string codigo)
        {
            return base.Channel.GetAgenciaByCodigoExterno(codigo);
        }

        public List<Agencia> GetAllAgencias()
        {
            return base.Channel.GetAllAgencias();
        }

        public void UpdateAgencia(Agencia ag)
        {
            base.Channel.UpdateAgencia(ag);
        }
    }
}
