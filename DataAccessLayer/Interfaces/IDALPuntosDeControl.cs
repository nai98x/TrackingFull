using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IDALPuntosDeControl
    {
        void AddPuntoDeControl(PuntoDeControl p);

        void DeletePuntoDeControl(int id);

        void UpdatePuntoDeControl(PuntoDeControl p);

        List<PuntoDeControl> GetAllPuntosDeControl();

        PuntoDeControl GetPuntoDeControl(int id);

        List<PuntoDeControl> GetAllPuntosDeControlDeTrayecto(int idTrayecto);

        string getNombreAgencia(int id);

        void UpdatePosicionPuntoDeControl(int id, int nuevaPos);

        PuntoDeControl GetPuntoDeControlByCodigoExterno(string codigo);
    }
}
