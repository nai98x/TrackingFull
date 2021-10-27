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
    class ServiceTrayectoProxy : ClientBase<IServiceTrayecto>, IServiceTrayecto
    {
        public void AddTrayecto(Trayecto tr)
        {
            base.Channel.AddTrayecto(tr);
        }

        public void DeleteTrayecto(int id)
        {
            base.Channel.DeleteTrayecto(id);
        }

        public List<Trayecto> getAllTrayectos()
        {
            return base.Channel.getAllTrayectos();
        }

        public Trayecto getTrayecto(int id)
        {
            return base.Channel.getTrayecto(id);
        }

        public Trayecto GetTrayectoByCodigoExterno(string codigo)
        {
            return base.Channel.GetTrayectoByCodigoExterno(codigo);
        }

        public void UpdateTrayecto(Trayecto tr)
        {
            base.Channel.UpdateTrayecto(tr);
        }
    }
}
