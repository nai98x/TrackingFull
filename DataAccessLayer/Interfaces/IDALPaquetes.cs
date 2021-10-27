using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IDALPaquetes
    {
        void AddPaquete(Paquete paq);

        void DeletePaquete(int id);

        void UpdatePaquete(Paquete paq);

        List<Paquete> GetAllPaquetes();

        Paquete GetPaquete(int id);

        PuntoDeControl GetPuntoDeControlActual(int id);

        void UpdatePDCActual(int id, int idPdc);

        void FinalizarEntrega(int id);

        void UpdateHoraDeEntrega(int id, int hora);

        int GetHoraDeEntrega(int id);

        Paquete GetPaqueteByCodigoExterno(string codigo);
    }
}
