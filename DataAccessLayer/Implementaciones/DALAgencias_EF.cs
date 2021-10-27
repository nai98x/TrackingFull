using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Entities;
using Shared.Exception;

namespace DataAccessLayer
{
    public class DALAgencias_EF : IDALAgencias
    {
        public void AddAgencia(Agencia ag)
        {
            if(ag != null)
            {
                using (var context = new Model.TFEntities())
                {
                    Model.Agencia nuevo;
                    if(ag.CodigoExterno == null)
                    {
                        nuevo = new Model.Agencia()
                        {
                            Nombre = ag.Nombre,
                            Direccion = ag.Direccion,
                            EntregaDomicilio = ag.EntregaDomicilio,
                            Borrado = false
                        };
                    }
                    else
                    {
                        nuevo = new Model.Agencia()
                        {
                            CodigoExterno = ag.CodigoExterno,
                            Nombre = ag.Nombre,
                            Direccion = ag.Direccion,
                            EntregaDomicilio = ag.EntregaDomicilio,
                            Borrado = false
                        };
                    }
                    context.Agencias.Add(nuevo);
                    context.SaveChanges();
                }
            }
            else
            {
                throw new AgenciaNoExistenteException();
            }
        }

        public void DeleteAgencia(int id)
        {
            using (var context = new Model.TFEntities())
            {
                Model.Agencia ag = context.Agencias.FirstOrDefault(x => x.Id == id);
                if(ag != null)
                {
                    ag.Borrado = true;
                    context.SaveChanges();
                }
                else
                {
                    throw new AgenciaNoExistenteException();
                }
            }
        }

        public Agencia GetAgencia(int id)
        {
            using (Model.TFEntities en = new Model.TFEntities())
            {
                Model.Agencia ag = en.Agencias.FirstOrDefault(x => x.Id == id);
                if (ag != null)
                {
                    return ag.ToEntity();
                }
                else
                {
                    throw new AgenciaNoExistenteException();
                }
            }
        }

        public List<Agencia> GetAllAgencias()
        {
            List<Agencia> res = new List<Agencia>();
            using (Model.TFEntities en = new Model.TFEntities())
            {
                en.Agencias.ToList().ForEach(x =>
                {
                    if (!x.Borrado)
                    {
                        res.Add(x.ToEntity());
                    }
                });
            }
            return res;
        }

        public void UpdateAgencia(Agencia ag)
        {
            using (Model.TFEntities en = new Model.TFEntities())
            {
                Model.Agencia age = en.Agencias.FirstOrDefault(x => x.Id == ag.Id);
                if(age != null)
                {
                    age.Nombre = ag.Nombre;
                    age.Direccion = ag.Direccion;
                    age.EntregaDomicilio = ag.EntregaDomicilio;
                    en.SaveChanges();
                }
                else
                {
                    throw new AgenciaNoExistenteException();
                }
            }
        }

        public Agencia GetAgenciaByCodigoExterno(string codigo)
        {
            using (Model.TFEntities en = new Model.TFEntities())
            {
                Model.Agencia ag = en.Agencias.FirstOrDefault(x => x.CodigoExterno == codigo);
                if (ag != null)
                {
                    return ag.ToEntity();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
