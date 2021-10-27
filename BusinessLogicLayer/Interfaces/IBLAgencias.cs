using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IBLAgencias
    {
        void AddAgencia(Agencia ag);

        void DeleteAgencia(int id);

        void UpdateAgencia(Agencia ag);

        List<Agencia> GetAllAgencias();

        Agencia GetAgencia(int id);

        Agencia GetAgenciaByCodigoExterno(string codigo);
    }
}
