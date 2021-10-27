using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer;
using Shared.Entities;

namespace ServiceLayer
{
    class ServicePuntosDeControl : IServicePuntosDeControl
    {
        private static IBLPuntosDeControl blHandler;

        public ServicePuntosDeControl()
        {
            blHandler = Program.blHandlerPdc;
        }

        public void AddPuntoDeControl(PuntoDeControl p)
        {
            blHandler.AddPuntoDeControl(p);
        }

        public void DeletePuntoDeControl(int id, int idTr)
        {
            blHandler.DeletePuntoDeControl(id, idTr);
        }

        public List<PuntoDeControl> GetAllPuntosDeControl()
        {
            return blHandler.GetAllPuntosDeControl();
        }

        public List<PuntoDeControl> GetAllPuntosDeControlDeTrayecto(int idTrayecto)
        {
            return blHandler.GetAllPuntosDeControlDeTrayecto(idTrayecto);
        }

        public PuntoDeControl GetPuntoDeControl(int id)
        {
            return blHandler.GetPuntoDeControl(id);
        }

        public PuntoDeControl GetPuntoDeControlByCodigoExterno(string codigo)
        {
            return blHandler.GetPuntoDeControlByCodigoExterno(codigo);
        }

        public void UpdatePuntoDeControl(PuntoDeControl p)
        {
            blHandler.UpdatePuntoDeControl(p);
        }
    }
}
