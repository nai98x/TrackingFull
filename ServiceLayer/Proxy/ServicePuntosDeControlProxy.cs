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
    class ServicePuntosDeControlProxy : ClientBase<IServicePuntosDeControl>, IServicePuntosDeControl
    {
        public void AddPuntoDeControl(PuntoDeControl p)
        {
            base.Channel.AddPuntoDeControl(p);
        }

        public void DeletePuntoDeControl(int id, int idTr)
        {
            base.Channel.DeletePuntoDeControl(id, idTr);
        }

        public List<PuntoDeControl> GetAllPuntosDeControl()
        {
            return base.Channel.GetAllPuntosDeControl();
        }

        public List<PuntoDeControl> GetAllPuntosDeControlDeTrayecto(int idTrayecto)
        {
            return base.Channel.GetAllPuntosDeControlDeTrayecto(idTrayecto);
        }

        public PuntoDeControl GetPuntoDeControl(int id)
        {
            return base.Channel.GetPuntoDeControl(id);
        }

        public PuntoDeControl GetPuntoDeControlByCodigoExterno(string codigo)
        {
            return base.Channel.GetPuntoDeControlByCodigoExterno(codigo);
        }

        public void UpdatePuntoDeControl(PuntoDeControl p)
        {
            base.Channel.UpdatePuntoDeControl(p);
        }
    }
}
