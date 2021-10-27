using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer;
using Shared.Entities;

namespace ServiceLayer
{
    class ServiceTrayecto : IServiceTrayecto
    {
        private static IBLTrayectos blHandler;

        public ServiceTrayecto()
        {
            blHandler = Program.blHandlerTr;
        }

        public void AddTrayecto(Trayecto tr)
        {
            blHandler.AddTrayecto(tr);
        }

        public void DeleteTrayecto(int id)
        {
            blHandler.DeleteTrayecto(id);
        }

        public List<Trayecto> getAllTrayectos()
        {
            return blHandler.getAllTrayectos();
        }

        public Trayecto getTrayecto(int id)
        {
            return blHandler.getTrayecto(id);
        }

        public Trayecto GetTrayectoByCodigoExterno(string codigo)
        {
            return blHandler.GetTrayectoByCodigoExterno(codigo);
        }

        public void UpdateTrayecto(Trayecto tr)
        {
            blHandler.UpdateTrayecto(tr);
        }
    }
}
