using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Entities;
using Shared.Exception;

namespace DataAccessLayer
{
    public class DALTrayectos_EF : IDALTrayectos
    {
        public void AddTrayecto(Trayecto tr)
        {
            if(tr != null)
            {
                using (var context = new Model.TFEntities())
                {
                    Model.Trayecto nuevo;
                    if(tr.CodigoExterno == null)
                    {
                        nuevo = new Model.Trayecto()
                        {
                            Nombre = tr.Nombre,
                            idAgenciaOrigen = tr.Origen.Id,
                            idAgenciaDestino = tr.Destino.Id,
                            Borrado = false
                        };
                    }
                    else
                    {
                        nuevo = new Model.Trayecto()
                        {
                            CodigoExterno = tr.CodigoExterno,
                            Nombre = tr.Nombre,
                            idAgenciaOrigen = tr.Origen.Id,
                            idAgenciaDestino = tr.Destino.Id,
                            Borrado = false
                        };
                    }
                    context.Trayectos.Add(nuevo);
                    context.SaveChanges();
                }
            }
        }

        public void DeleteTrayecto(int id)
        {
            using (var context = new Model.TFEntities())
            {
                Model.Trayecto tr = context.Trayectos.FirstOrDefault(x => x.Id == id);
                if(tr != null)
                {
                    tr.Borrado = true;
                    context.SaveChanges();
                }
            }
        }

        public List<Trayecto> getAllTrayectos()
        {
            List<Trayecto> res = new List<Trayecto>();
            using (Model.TFEntities en = new Model.TFEntities())
            {
                en.Trayectos.ToList().ForEach(x =>
                {
                    if (!x.Borrado)
                    {
                        res.Add(x.ToEntity());
                    }
                });
            }
            return res;
        }

        public List<PuntoDeControl> getPuntosDeControl(int id)
        {
            List<PuntoDeControl> ret = new List<PuntoDeControl>();
            using (Model.TFEntities en = new Model.TFEntities())
            {
                Model.Trayecto tr = en.Trayectos.FirstOrDefault(x => x.Id == id);
                if (tr != null)
                {
                    tr.PuntoDeControl.ToList().ForEach(x =>
                    {
                        ret.Add(x.ToEntity());
                    });
                    ret.Sort((x, y) => x.Posicion.CompareTo(y.Posicion));
                }
            }
            return ret;
        }

        public Trayecto getTrayecto(int id)
        {
            using (Model.TFEntities en = new Model.TFEntities())
            {
                Model.Trayecto tr = en.Trayectos.FirstOrDefault(x => x.Id == id);
                if(tr != null)
                {
                    return tr.ToEntity();
                }
                else
                {
                    return null;
                }
            }
        }

        public Trayecto getTrayectoByNombre(string nom)
        {
            using (Model.TFEntities en = new Model.TFEntities())
            {
                Model.Trayecto tr = en.Trayectos.FirstOrDefault(x => x.Nombre == nom);
                if (tr != null)
                {
                    return tr.ToEntity();
                }
                else
                {
                    return null;
                }
            }
        }

        public void UpdateTrayecto(Trayecto tr)
        {
            using (Model.TFEntities en = new Model.TFEntities())
            {
                Model.Trayecto trM = en.Trayectos.FirstOrDefault(x => x.Id == tr.Id);
                if (trM != null)
                {
                    trM.Nombre = tr.Nombre;
                    trM.idAgenciaOrigen = tr.Origen.Id;
                    trM.idAgenciaDestino = tr.Destino.Id;
                    en.SaveChanges();
                }
            }
        }

        public Trayecto getTrayectoByCodigoExterno(string codigo)
        {
            using (Model.TFEntities en = new Model.TFEntities())
            {
                Model.Trayecto tr = en.Trayectos.FirstOrDefault(x => x.CodigoExterno == codigo);
                if (tr != null)
                {
                    return tr.ToEntity();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
