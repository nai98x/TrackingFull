using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Entities;
using Shared.Exception;

namespace DataAccessLayer
{
    public class DALPaquetes_EF : IDALPaquetes
    {
        public void AddPaquete(Paquete paq)
        {
            if(paq != null)
            {
                using (var context = new Model.TFEntities())
                {
                    Model.Paquete nuevo;
                    if(paq.CodigoExterno == null)
                    {
                        nuevo = new Model.Paquete()
                        {
                            Descripcion = paq.Descripcion,
                            FechaEntrega = paq.FechaEntrega,
                            Codigo = paq.Codigo,
                            Entregado = false,
                            FechaIngreso = DateTime.Now,
                            TrayectoId = paq.Trayecto.Id,
                            RemitenteId = paq.Remitente.Id,
                            DestinatarioId = paq.Destinatario.Id,
                            PuntoDeControlId = paq.PDCActual.Id,
                            HoraDeEntrega = -1,
                            Borrado = false
                        };
                    }
                    else
                    {
                        nuevo = new Model.Paquete()
                        {
                            CodigoExterno = paq.CodigoExterno,
                            Descripcion = paq.Descripcion,
                            FechaEntrega = paq.FechaEntrega,
                            Codigo = paq.Codigo,
                            Entregado = false,
                            FechaIngreso = DateTime.Now,
                            TrayectoId = paq.Trayecto.Id,
                            RemitenteId = paq.Remitente.Id,
                            DestinatarioId = paq.Destinatario.Id,
                            PuntoDeControlId = paq.PDCActual.Id,
                            HoraDeEntrega = -1,
                            Borrado = false
                        };
                    }
                    context.Paquetes.Add(nuevo);
                    context.SaveChanges();
                }
            }
            else
            {
                throw new PaqueteNoExistenteException();
            }
        }

        public void DeletePaquete(int id)
        {
            using (var context = new Model.TFEntities())
            {
                Model.Paquete paq = context.Paquetes.FirstOrDefault(x => x.Id == id);
                if (paq != null)
                {
                    paq.Borrado = true;
                    context.SaveChanges();
                }
                else
                {
                    throw new PaqueteNoExistenteException();
                }
            }
        }

        public List<Paquete> GetAllPaquetes()
        {
            List<Paquete> res = new List<Paquete>();
            using (Model.TFEntities en = new Model.TFEntities())
            {
                en.Paquetes.ToList().ForEach(x =>
                {
                    if (!x.Borrado)
                    {
                        res.Add(x.ToEntity());
                    }
                });
            }
            return res;
        }

        public Paquete GetPaquete(int id)
        {
            using (Model.TFEntities en = new Model.TFEntities())
            {
                Model.Paquete paq = en.Paquetes.FirstOrDefault(x => x.Id == id);
                if(paq != null)
                {
                    return paq.ToEntity();
                }
                else
                {
                    return null;
                }
            }
        }

        public PuntoDeControl GetPuntoDeControlActual(int id)
        {
            using (Model.TFEntities en = new Model.TFEntities())
            {
                Model.Paquete paq = en.Paquetes.FirstOrDefault(x => x.Id == id);
                if (paq != null)
                {
                    return paq.PDC_Actual.ToEntity();
                }
                return null;
            }
        }

        public void UpdatePaquete(Paquete paq)
        {
            using (Model.TFEntities en = new Model.TFEntities())
            {
                Model.Paquete p = en.Paquetes.FirstOrDefault(x => x.Id == paq.Id);
                if (paq != null)
                {
                    p.Descripcion = paq.Descripcion;
                    p.FechaEntrega = paq.FechaEntrega;
                    en.SaveChanges();
                }
                else
                {
                    throw new PaqueteNoExistenteException();
                }
            }
        }

        public void UpdatePDCActual(int id, int idPdc)
        {
            using (Model.TFEntities en = new Model.TFEntities())
            {
                Model.Paquete p = en.Paquetes.FirstOrDefault(x => x.Id == id);
                if (p != null && idPdc > 0)
                {
                    p.PuntoDeControlId = idPdc;
                    en.SaveChanges();
                }
                else
                {
                    throw new PaqueteNoExistenteException();
                }
            }
        }

        public void FinalizarEntrega(int id)
        {
            using (Model.TFEntities en = new Model.TFEntities())
            {
                Model.Paquete p = en.Paquetes.FirstOrDefault(x => x.Id == id);
                if(p != null)
                {
                    p.Entregado = true;
                    en.SaveChanges();
                }
            }
        }

        public void UpdateHoraDeEntrega(int id, int hora)
        {
            using (Model.TFEntities en = new Model.TFEntities())
            {
                Model.Paquete p = en.Paquetes.FirstOrDefault(x => x.Id == id);
                if (p != null)
                {
                    p.HoraDeEntrega = hora;
                    en.SaveChanges();
                }
            }
        }

        public int GetHoraDeEntrega(int id)
        {
            using (Model.TFEntities en = new Model.TFEntities())
            {
                Model.Paquete p = en.Paquetes.FirstOrDefault(x => x.Id == id);
                return (int)p.HoraDeEntrega;
            }
        }

        public Paquete GetPaqueteByCodigoExterno(string codigo)
        {
            using (Model.TFEntities en = new Model.TFEntities())
            {
                Model.Paquete paq = en.Paquetes.FirstOrDefault(x => x.CodigoExterno == codigo);
                if (paq != null)
                {
                    return paq.ToEntity();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
