using Shared.Entities;
using Shared.Exception;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer
{
    public class DALUsuarios_EF : IDALUsuarios
    {
        public void AddUsuario(Usuario usu)
        {
            using (var context = new Model.TFEntities())
            {
                Model.Usuario nuevo;
                if(usu.CodigoExterno == null)
                {
                    nuevo = new Model.Usuario()
                    {
                        Email = usu.Email,
                        Pass = usu.Password,
                        Rol = usu.Rol,
                        Nombre = usu.Nombre,
                        Direccion = usu.Direccion,
                        PaquetesEnviados = new List<Model.Paquete>(),
                        PaquetesRecibidos = new List<Model.Paquete>(),
                        Telefono = usu.Telefono,
                        TipoDocumento = usu.TipoDocumento,
                        NroDocumento = usu.NroDocumento,
                        Borrado = false
                    };
                }
                else
                {
                    nuevo = new Model.Usuario()
                    {
                        CodigoExterno = usu.CodigoExterno,
                        Email = usu.Email,
                        Pass = usu.Password,
                        Rol = usu.Rol,
                        Nombre = usu.Nombre,
                        Direccion = usu.Direccion,
                        PaquetesEnviados = new List<Model.Paquete>(),
                        PaquetesRecibidos = new List<Model.Paquete>(),
                        Telefono = usu.Telefono,
                        TipoDocumento = usu.TipoDocumento,
                        NroDocumento = usu.NroDocumento,
                        Borrado = false
                    };
                }
                context.Usuarios.Add(nuevo);
                context.SaveChanges();
            }
        }

        public void DeleteUsuario(int id)
        {
            using (var context = new Model.TFEntities())
            {
                Model.Usuario usu = context.Usuarios.FirstOrDefault(x => x.Id == id);
                if(usu != null)
                {
                    usu.Borrado = true;
                    context.SaveChanges();
                }
                else
                {
                    throw new UsuarioNoExistenteException();
                }
            }
        }

        public List<Usuario> getAllUsuarios()
        {
            List<Usuario> ret = new List<Usuario>();
            using (Model.TFEntities en = new Model.TFEntities())
            {
                en.Usuarios.ToList().ForEach(x =>
                {
                    if (!x.Borrado)
                    {
                        ret.Add(x.ToEntity());
                    }
                });
            }
            return ret;
        }

        public Usuario getUsuario(int id)
        {
            using (Model.TFEntities en = new Model.TFEntities())
            {
                Model.Usuario usu = en.Usuarios.FirstOrDefault(x => x.Id == id);
                if(usu != null)
                {
                    return usu.ToEntity();
                }
                else
                {
                    return null;
                }
            }
        }

        public Usuario getUsuarioByEmail(string email)
        {
            using (Model.TFEntities en = new Model.TFEntities())
            {
                Model.Usuario usu = en.Usuarios.FirstOrDefault(x => x.Email == email);
                if (usu != null)
                {
                    return usu.ToEntity();
                }
                else
                {
                    return null;
                }
            }
        }

        public void UpdateUsuario(Usuario usu)
        {
            using (var context = new Model.TFEntities())
            {
                Model.Usuario u = context.Usuarios.FirstOrDefault(x => x.Id == usu.Id);
                if(u != null)
                {
                    u.Email = usu.Email;
                    u.Pass = usu.Password;
                    u.Nombre = usu.Nombre;
                    u.Direccion = usu.Direccion;
                    u.Telefono = usu.Telefono;
                    u.TipoDocumento = usu.TipoDocumento;
                    u.NroDocumento = usu.NroDocumento;
                    context.SaveChanges();
                }
                else
                {
                    throw new UsuarioNoExistenteException();
                }
            }
        }

        public void AgregarPaquete(Paquete paq)
        {
            using (var context = new Model.TFEntities())
            {
                Model.Usuario remitente = context.Usuarios.FirstOrDefault(x => x.Id == paq.Remitente.Id);
                Model.Usuario destinatario = context.Usuarios.FirstOrDefault(x => x.Id == paq.Destinatario.Id);
                if (remitente != null && destinatario != null)
                {
                    remitente.PaquetesEnviados.Add(new Model.Paquete()
                    {
                        Id = paq.Id
                    });
                    destinatario.PaquetesRecibidos.Add(new Model.Paquete()
                    {
                        Id = paq.Id
                    });
                    context.SaveChanges();
                }
            }
        }

        public List<Paquete> GetPaquetesEnviados(int id)
        {
            List<Paquete> ret = new List<Paquete>();
            using (var context = new Model.TFEntities())
            {
                context.Paquetes.ToList().ForEach(x =>
                {
                    if(x.RemitenteId == id)
                    {
                        ret.Add(x.ToEntity());
                    }
                });
            }
            return ret;
        }

        public List<Paquete> GetPaquetesRecibidos(int id)
        {
            List<Paquete> ret = new List<Paquete>();
            using (var context = new Model.TFEntities())
            {
                context.Paquetes.ToList().ForEach(x =>
                {
                    if (x.DestinatarioId == id)
                    {
                        ret.Add(x.ToEntity());
                    }
                });
            }
            return ret;
        }

        public void CambiarPermisos(int id, string rol)
        {
            using (Model.TFEntities en = new Model.TFEntities())
            {
                Model.Usuario usu = en.Usuarios.FirstOrDefault(x => x.Id == id);
                if(usu != null)
                {
                    usu.Rol = rol;
                    en.SaveChanges();
                }
            }
        }

        public Usuario getUsuarioByCodigoExterno(string codigo)
        {
            using (Model.TFEntities en = new Model.TFEntities())
            {
                Model.Usuario usu = en.Usuarios.FirstOrDefault(x => x.CodigoExterno == codigo);
                if (usu != null)
                {
                    return usu.ToEntity();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
