using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Shared.Entities;

namespace BusinessLogicLayer
{
    public class BLTrayectos : IBLTrayectos
    {
        private IDALTrayectos dal_tr;
        private IDALPuntosDeControl dal_pdc;

        public BLTrayectos()
        {
            dal_tr = new DALTrayectos_EF();
            dal_pdc = new DALPuntosDeControl_EF();
        }

        public void AddTrayecto(Trayecto tr)
        {
            dal_tr.AddTrayecto(tr);
            Trayecto nuevo = dal_tr.getTrayectoByNombre(tr.Nombre);
            PuntoDeControl pc1 = new PuntoDeControl()
            {
                Nombre = "Recibido en origen",
                Posicion = 1,
                TiempoEstimado = 0,
                Trayecto = nuevo
            };
            PuntoDeControl pc2 = new PuntoDeControl()
            {
                Nombre = "Esperando en origen",
                Posicion = 2,
                TiempoEstimado = 0,
                Trayecto = nuevo
            };
            PuntoDeControl pc3 = new PuntoDeControl()
            {
                Nombre = "En viaje",
                Posicion = 3,
                TiempoEstimado = 0,
                Trayecto = nuevo
            };
            PuntoDeControl pc4 = new PuntoDeControl()
            {
                Nombre = "Recibido en Destino",
                Posicion = 4,
                TiempoEstimado = 0,
                Trayecto = nuevo
            };
            dal_pdc.AddPuntoDeControl(pc1);
            dal_pdc.AddPuntoDeControl(pc2);
            dal_pdc.AddPuntoDeControl(pc3);
            dal_pdc.AddPuntoDeControl(pc4);
        }

        public void DeleteTrayecto(int id)
        {
            dal_tr.DeleteTrayecto(id);
        }

        public List<Trayecto> getAllTrayectos()
        {
            return dal_tr.getAllTrayectos();
        }

        public Trayecto getTrayecto(int id)
        {
            return dal_tr.getTrayecto(id);
        }

        public Trayecto GetTrayectoByCodigoExterno(string codigo)
        {
            return dal_tr.getTrayectoByCodigoExterno(codigo);
        }

        public void UpdateTrayecto(Trayecto tr)
        {
            dal_tr.UpdateTrayecto(tr);
        }
    }
}
