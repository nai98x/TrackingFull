using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IBLTrayectos
    {
        void AddTrayecto(Trayecto tr);

        void DeleteTrayecto(int id);

        void UpdateTrayecto(Trayecto tr);

        List<Trayecto> getAllTrayectos();

        Trayecto getTrayecto(int id);

        Trayecto GetTrayectoByCodigoExterno(string codigo);
    }
}
