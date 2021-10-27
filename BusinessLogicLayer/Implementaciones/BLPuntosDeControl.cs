using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Shared.Entities;

namespace BusinessLogicLayer
{
    public class BLPuntosDeControl : IBLPuntosDeControl
    {
        private IDALPuntosDeControl dal_pdc;
        private IDALTrayectos dal_tr;

        public BLPuntosDeControl()
        {
            dal_pdc = new DALPuntosDeControl_EF();
            dal_tr = new DALTrayectos_EF();
        }

        public void AddPuntoDeControl(PuntoDeControl p)
        {
            if(p != null && p.Trayecto.Id > 0)
            {
                int idTrayecto = p.Trayecto.Id;
                List<PuntoDeControl> pdcs = dal_tr.getPuntosDeControl(idTrayecto);
                int mayor = 0;
                pdcs.ForEach(x => { 
                    if (x.Posicion > mayor)
                    {
                        mayor = x.Posicion;
                    }
                });
                if(p.Posicion <= mayor)
                {
                    if(p.Posicion == mayor)
                    {
                        PuntoDeControl cambiar = pdcs.Find(x => x.Posicion == mayor);
                        dal_pdc.UpdatePosicionPuntoDeControl(cambiar.Id, cambiar.Posicion + 1);
                    }
                    else
                    {
                        pdcs.ForEach(x => { 
                            if(x.Posicion >= p.Posicion)
                            {
                                dal_pdc.UpdatePosicionPuntoDeControl(x.Id, x.Posicion + 1);
                            }
                        });
                    }
                }
                dal_pdc.AddPuntoDeControl(p);
            } 
        }

        public void DeletePuntoDeControl(int id, int idTr)
        {
            PuntoDeControl p = dal_pdc.GetPuntoDeControl(id);
            if (p != null)
            {
                List<PuntoDeControl> pdcs = dal_tr.getPuntosDeControl(idTr);
                pdcs.ForEach(x => {
                    if (x.Posicion > p.Posicion)
                    {
                        dal_pdc.UpdatePosicionPuntoDeControl(x.Id, x.Posicion - 1);
                    }
                });
                dal_pdc.DeletePuntoDeControl(id);
            }
        }

        public List<PuntoDeControl> GetAllPuntosDeControl()
        {
            return dal_pdc.GetAllPuntosDeControl();
        }

        public List<PuntoDeControl> GetAllPuntosDeControlDeTrayecto(int idTrayecto)
        {
            return dal_pdc.GetAllPuntosDeControlDeTrayecto(idTrayecto);
        }

        public PuntoDeControl GetPuntoDeControl(int id)
        {
            return dal_pdc.GetPuntoDeControl(id);
        }

        public PuntoDeControl GetPuntoDeControlByCodigoExterno(string codigo)
        {
            return dal_pdc.GetPuntoDeControlByCodigoExterno(codigo);
        }

        public void UpdatePuntoDeControl(PuntoDeControl p)
        {
            if (p != null && p.Trayecto.Id > 0)
            {
                int idTrayecto = p.Trayecto.Id;
                List<PuntoDeControl> pdcs = dal_tr.getPuntosDeControl(idTrayecto);
                PuntoDeControl actual = dal_pdc.GetPuntoDeControl(p.Id);
                if (p.Posicion < actual.Posicion) 
                {
                    pdcs.ForEach(x => {
                        if (x.Posicion < actual.Posicion && x.Posicion >= p.Posicion)
                        {
                            dal_pdc.UpdatePosicionPuntoDeControl(x.Id, x.Posicion + 1);
                        }
                    });
                }
                else 
                {
                    pdcs.ForEach(x => {
                        if (x.Posicion > actual.Posicion && x.Posicion <= p.Posicion)
                        {
                            dal_pdc.UpdatePosicionPuntoDeControl(x.Id, x.Posicion - 1);
                        }
                    });
                }
                dal_pdc.UpdatePuntoDeControl(p);
            }
        }
    }
}
