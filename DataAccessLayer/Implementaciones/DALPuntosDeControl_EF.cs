using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Entities;

namespace DataAccessLayer
{
    public class DALPuntosDeControl_EF : IDALPuntosDeControl
    {
        public void AddPuntoDeControl(PuntoDeControl p)
        {
            if(p != null)
            {
                using (var context = new Model.TFEntities())
                {
                    Model.PuntoDeControl nuevo;
                    if (p.Agencia != null)
                    {
                        if(p.CodigoExterno == null)
                        {
                            nuevo = new Model.PuntoDeControl()
                            {
                                Nombre = p.Nombre,
                                Posicion = p.Posicion,
                                TiempoEstimado = p.TiempoEstimado,
                                IdAgencia = p.Agencia.Id,
                                IdTrayecto = p.Trayecto.Id,
                                Borrado = false
                            };
                        }
                        else
                        {
                            nuevo = new Model.PuntoDeControl()
                            {
                                CodigoExterno = p.CodigoExterno,
                                Nombre = p.Nombre,
                                Posicion = p.Posicion,
                                TiempoEstimado = p.TiempoEstimado,
                                IdAgencia = p.Agencia.Id,
                                IdTrayecto = p.Trayecto.Id,
                                Borrado = false
                            };
                        }
                        context.PuntosDeControl.Add(nuevo);
                    }
                    else
                    {
                        if(p.CodigoExterno == null)
                        {
                            nuevo = new Model.PuntoDeControl()
                            {
                                Nombre = p.Nombre,
                                Posicion = p.Posicion,
                                TiempoEstimado = p.TiempoEstimado,
                                IdTrayecto = p.Trayecto.Id,
                                Borrado = false
                            };
                        }
                        else
                        {
                            nuevo = new Model.PuntoDeControl()
                            {
                                CodigoExterno = p.CodigoExterno,
                                Nombre = p.Nombre,
                                Posicion = p.Posicion,
                                TiempoEstimado = p.TiempoEstimado,
                                IdTrayecto = p.Trayecto.Id,
                                Borrado = false
                            };
                        }
                        context.PuntosDeControl.Add(nuevo);
                    }
                    context.SaveChanges();
                }
            }
        }

        public void DeletePuntoDeControl(int id)
        {
            using (var context = new Model.TFEntities())
            {
                Model.PuntoDeControl p = context.PuntosDeControl.FirstOrDefault(x => x.Id == id);
                if (p != null)
                {
                    p.Borrado = true;
                    context.SaveChanges();
                }
            }
        }

        public List<PuntoDeControl> GetAllPuntosDeControl()
        {
            List<PuntoDeControl> res = new List<PuntoDeControl>();
            using (Model.TFEntities en = new Model.TFEntities())
            {
                en.PuntosDeControl.ToList().ForEach(x =>
                {
                    if (!x.Borrado)
                    {
                        res.Add(x.ToEntity());
                    }
                });
            }
            return res;
        }

        public PuntoDeControl GetPuntoDeControl(int id)
        {
            using (Model.TFEntities en = new Model.TFEntities())
            {
                Model.PuntoDeControl p = en.PuntosDeControl.FirstOrDefault(x => x.Id == id);
                if (p != null)
                {
                    return p.ToEntity();
                }
                else
                {
                    return null;
                }
            }
        }

        public void UpdatePuntoDeControl(PuntoDeControl p)
        {
            if (p != null)
            {
                using (Model.TFEntities en = new Model.TFEntities())
                {
                    Model.PuntoDeControl pdc = en.PuntosDeControl.FirstOrDefault(x => x.Id == p.Id);
                    if (pdc != null)
                    {
                        pdc.Nombre = p.Nombre;
                        pdc.Posicion = p.Posicion;
                        pdc.TiempoEstimado = p.TiempoEstimado;
                        pdc.IdAgencia = p.Agencia.Id;
                        en.SaveChanges();
                    }
                }
            }
        }

        public List<PuntoDeControl> GetAllPuntosDeControlDeTrayecto(int idTrayecto)
        {
            List<PuntoDeControl> res = new List<PuntoDeControl>();
            using (Model.TFEntities en = new Model.TFEntities())
            {
                en.PuntosDeControl.ToList().ForEach(x =>
                {
                    if(x.IdTrayecto == idTrayecto)
                    {
                        if (!x.Borrado)
                        {
                            res.Add(x.ToEntity());
                        }
                    }
                });
            }
            return res;
        }

        public string getNombreAgencia(int id)
        {
            using (Model.TFEntities en = new Model.TFEntities())
            {
                Model.PuntoDeControl p = en.PuntosDeControl.FirstOrDefault(x => x.Id == id);
                if (p != null)
                {
                    if(p.Agencia != null && p.Agencia.Nombre != null)
                    {
                        return p.Agencia.Nombre;
                    }
                    else
                    {
                        return "(Ninguna)";
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public void UpdatePosicionPuntoDeControl(int id, int nuevaPos)
        {
            using (Model.TFEntities en = new Model.TFEntities())
            {
                Model.PuntoDeControl p = en.PuntosDeControl.FirstOrDefault(x => x.Id == id);
                if (p != null)
                {
                    p.Posicion = nuevaPos;
                    en.SaveChanges();
                }
            }
        }

        public PuntoDeControl GetPuntoDeControlByCodigoExterno(string codigo)
        {
            using (Model.TFEntities en = new Model.TFEntities())
            {
                Model.PuntoDeControl p = en.PuntosDeControl.FirstOrDefault(x => x.CodigoExterno == codigo);
                if (p != null)
                {
                    return p.ToEntity();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
