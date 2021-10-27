using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IBLPaquetes
    {
        void AddPaquete(Paquete paq);

        void DeletePaquete(int id);

        void UpdatePaquete(Paquete paq);

        List<Paquete> GetAllPaquetes();

        Paquete GetPaquete(int id);

        Paquete GetPaqueteByCodigoExterno(string codigo);
    }
}
