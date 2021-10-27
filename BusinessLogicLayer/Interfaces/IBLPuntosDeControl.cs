using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IBLPuntosDeControl
    {
        void AddPuntoDeControl(PuntoDeControl p);

        void DeletePuntoDeControl(int id, int idTr);

        void UpdatePuntoDeControl(PuntoDeControl p);

        List<PuntoDeControl> GetAllPuntosDeControl();

        PuntoDeControl GetPuntoDeControl(int id);

        List<PuntoDeControl> GetAllPuntosDeControlDeTrayecto(int idTrayecto);

        PuntoDeControl GetPuntoDeControlByCodigoExterno(string codigo);
    }
}
