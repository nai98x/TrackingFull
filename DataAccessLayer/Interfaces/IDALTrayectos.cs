using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IDALTrayectos
    {
        void AddTrayecto(Trayecto tr);

        void DeleteTrayecto(int id);

        void UpdateTrayecto(Trayecto tr);

        List<Trayecto> getAllTrayectos();

        Trayecto getTrayecto(int id);

        Trayecto getTrayectoByNombre(string nom);

        List<PuntoDeControl> getPuntosDeControl(int id);

        Trayecto getTrayectoByCodigoExterno(string codigo);
    }
}
