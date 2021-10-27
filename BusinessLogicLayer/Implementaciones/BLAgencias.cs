using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Shared.Entities;

namespace BusinessLogicLayer
{
    public class BLAgencias : IBLAgencias
    {
        private IDALAgencias dal_age;

        public BLAgencias()
        {
            dal_age = new DALAgencias_EF();
        }

        public void AddAgencia(Agencia ag)
        {
            dal_age.AddAgencia(ag);
        }

        public void DeleteAgencia(int id)
        {
            dal_age.DeleteAgencia(id);
        }

        public Agencia GetAgencia(int id)
        {
            return dal_age.GetAgencia(id);
        }

        public Agencia GetAgenciaByCodigoExterno(string codigo)
        {
            return dal_age.GetAgenciaByCodigoExterno(codigo);
        }

        public List<Agencia> GetAllAgencias()
        {
            return dal_age.GetAllAgencias();
        }

        public void UpdateAgencia(Agencia ag)
        {
            dal_age.UpdateAgencia(ag);
        }
    }
}
