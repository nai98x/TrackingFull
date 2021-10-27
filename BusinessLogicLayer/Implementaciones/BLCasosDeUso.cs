using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using RestSharp;
using Shared.Entities;

namespace BusinessLogicLayer
{
    public class BLCasosDeUso : IBLCasosDeUso
    {
        private IDALUsuarios dal_usu;
        private IDALAgencias dal_age;
        private IDALPaquetes dal_paq;
        private IDALTrayectos dal_tr;
        private IDALPuntosDeControl dal_pdc;

        public BLCasosDeUso()
        {
            dal_usu = new DALUsuarios_EF();
            dal_age = new DALAgencias_EF();
            dal_paq = new DALPaquetes_EF();
            dal_tr = new DALTrayectos_EF();
            dal_pdc = new DALPuntosDeControl_EF();
        }
        public void AltaAgencia(Agencia a)
        {
            if (a != null)
            {
                dal_age.AddAgencia(a);
            }
        }

        public void AltaCliente(Usuario cli)
        {
            if (cli != null)
            {
                cli.Rol = "Cliente";
                dal_usu.AddUsuario(cli);
            }
        }

        public void AltaPaquete(Paquete p, Agencia ag)
        {
            if (p != null && ag != null)
            {
                dal_paq.AddPaquete(p);
                //ag.Paquetes.Add(p);
                dal_age.UpdateAgencia(ag);
            }
        }

        public void AltaTrayecto(Trayecto t)
        {
            if (t != null)
            {
                dal_tr.AddTrayecto(t);
            }
        }

        public void AsignarAdministrador(Usuario adm)
        {
            if (adm != null)
            {
                adm.Rol = "Administrador";
                dal_usu.AddUsuario(adm);
            }
        }

        public void AsignarEmpleado(Usuario emp)
        {
            if (emp != null)
            {
                emp.Rol = "Funcionario";
                dal_usu.AddUsuario(emp);
            }
        }

        public void AsignarSistemaExterno(Usuario usu)
        {
            if (usu != null)
            {
                usu.Rol = "Sistema Externo";
                dal_usu.AddUsuario(usu);
            }
        }

        public bool AvanzarEstadoPaquete(int id)
        {
            bool done = false;
            Paquete p = dal_paq.GetPaquete(id);
            if(p != null)
            {
                string emailUsu = p.Destinatario.Email;
                List<PuntoDeControl>lista = dal_tr.getPuntosDeControl(p.Trayecto.Id);
                int indActual = p.PDCActual.Posicion;
                lista.ForEach(x => {
                    if(x.Posicion > indActual)
                    {
                        if (!done)
                        {
                            dal_paq.UpdatePDCActual(p.Id, x.Id);

                            var client = new RestClient("https://api.sendinblue.com/v3/smtp/email");
                            var request = new RestRequest(Method.POST);
                            request.AddHeader("accept", "application/json");
                            request.AddHeader("content-type", "application/json");
                            request.AddHeader("api-key", "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");
                            request.AddParameter("application/json", "{\"sender\":{\"name\":\"TrackingFULL\",\"email\":\"no-reply@trackingfull.com\"},\"to\":[{\"email\":\"" + emailUsu + "\",\"name\":\"" + p.Destinatario.Nombre + "\"}],\"textContent\":\"El nuevo estado de su paquete " + p.Descripcion + " es " + x.Nombre + ".\",\"subject\":\"TrackingFULL - Actualizacion de paquete\"}", ParameterType.RequestBody);
                            IRestResponse response = client.Execute(request);
                            done = true;
                        }
                    }
                });
            }
            return done;
        }

        public void BajaAgencia(int idAgencia)
        {
            Agencia verificar = dal_age.GetAgencia(idAgencia);
            if(verificar != null)
            {
                dal_age.DeleteAgencia(idAgencia);
            }
        }

        public void BajaCliente(int id)
        {
            Usuario verificar = dal_usu.getUsuario(id);
            if(verificar != null)
            {
                dal_usu.DeleteUsuario(id);
            }
        }

        public void BajaTrayecto(int idTrayecto)
        {
            Trayecto verificar = dal_tr.getTrayecto(idTrayecto);
            if(verificar != null)
            {
                dal_tr.DeleteTrayecto(idTrayecto);
            }
        }

        public bool CambiarPermisos(int id, string rol)
        {
            Usuario usu = dal_usu.getUsuario(id);
            if(usu != null)
            {
                if(rol == "Funcionario" || rol == "Cliente")
                {
                    dal_usu.CambiarPermisos(id, rol);
                    return true;
                }
            }
            return false;
        }

        public void CoordinarHoraEntregaPaquete(Paquete p)
        {
            if(p != null)
            {
                dal_paq.UpdatePaquete(p);
            }
        }

        public bool FinalizarEntregaPaquete(int id, string codigoVerificacion)
        {
            Paquete paq = dal_paq.GetPaquete(id);
            if (paq != null)
            {
                string emailUsu = paq.Destinatario.Email;
                PuntoDeControl pdcActual = dal_paq.GetPuntoDeControlActual(id);
                List<PuntoDeControl> lista = dal_tr.getPuntosDeControl(dal_paq.GetPaquete(id).Trayecto.Id);
                if (lista.Last().Id == pdcActual.Id)
                {
                    if (paq.Codigo == codigoVerificacion)
                    {
                        dal_paq.FinalizarEntrega(id);
                        var client = new RestClient("https://api.sendinblue.com/v3/smtp/email");
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("accept", "application/json");
                        request.AddHeader("content-type", "application/json");
                        request.AddHeader("api-key", "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");
                        request.AddParameter("application/json", "{\"sender\":{\"name\":\"TrackingFULL\",\"email\":\"no-reply@trackingfull.com\"},\"to\":[{\"email\":\"" + emailUsu + "\",\"name\":\"" + paq.Destinatario.Nombre + "\"}],\"textContent\":\"Le avisamos que su paquete " + paq.Descripcion + " ya ha sido entregado.\",\"subject\":\"TrackingFULL - Actualizacion de paquete\"}", ParameterType.RequestBody);
                        IRestResponse response = client.Execute(request);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public List<Usuario> GetClientes()
        {
            List<Usuario> ret = new List<Usuario>();
            List<Usuario> lista= dal_usu.getAllUsuarios();
            lista.ForEach(x => {
                if(x.Rol == "Cliente")
                {
                    ret.Add(x);
                }
            });
            return ret;
        }

        public string GetEstadoPaquete(int id)
        {
            if (dal_paq.GetPaquete(id).Entregado)
            {
                return "Entregado";
            }
            else
            {
                return dal_paq.GetPuntoDeControlActual(id).Nombre;
            }   
        }

        public int GetHoraDeEntregaPaquete(int id)
        {
            return dal_paq.GetHoraDeEntrega(id);
        }

        public List<Paquete> getPaquetesFiltro(DateTime fechaDesde, DateTime fechaHasta, string estado, int idDestinatario, int idRemitente)
        {
            List<Paquete> pool = dal_paq.GetAllPaquetes();
            List<Paquete> aux;
            if(fechaDesde != null && fechaDesde.Date != DateTime.Now.AddYears(14).Date)
            {
                aux = pool.Where(x => x.FechaIngreso.Date >= fechaDesde.Date).ToList();
                pool = aux;
            }
            if (fechaHasta != null && fechaHasta.Date != DateTime.Now.AddYears(14).Date)
            {
                aux = pool.Where(x => x.FechaIngreso.Date <= fechaHasta.Date).ToList();
                pool = aux;
            }
            if (estado != null && estado != "")
            {
                aux = pool.Where(x => x.PDCActual.Nombre == estado).ToList();
                pool = aux;
            }
            if(idDestinatario != 0 && idDestinatario != 999)
            {
                aux = pool.Where(x => x.Destinatario.Id == idDestinatario).ToList();
                pool = aux;
            }
            if (idRemitente != 0 && idRemitente != 999)
            {
                aux = pool.Where(x => x.Remitente.Id == idRemitente).ToList();
                pool = aux;
            }
            return pool;
        }

        public string GetPassByEmail(string email)
        {
            Usuario usu = dal_usu.getUsuarioByEmail(email);
            if(usu != null)
            {
                return usu.Password;
            }
            else
            {
                return "ERROR";
            }
        }

        public int[] GraficasPorEstado()
        {
            int RecibidoEnOrigen = 0;
            int EsperandoEnOrigen = 0;
            int EnViaje = 0;
            int RecibidoEnDestino = 0;
            int EntregadoAlCliente = 0;

            List<Paquete> paquetes = dal_paq.GetAllPaquetes();
            paquetes.ForEach(x => {
                if(x.PDCActual.Nombre.ToUpper() == "Recibido en origen".ToUpper() && !x.Entregado)
                {
                    RecibidoEnOrigen++;
                }
                if (x.PDCActual.Nombre.ToUpper() == "Esperando en origen".ToUpper() && !x.Entregado)
                {
                    EsperandoEnOrigen++;
                }
                if (x.PDCActual.Nombre.ToUpper() == "En viaje".ToUpper() && !x.Entregado)
                {
                    EnViaje++;
                }
                if (x.PDCActual.Nombre.ToUpper() == "Recibido en Destino".ToUpper() && !x.Entregado)
                {
                    RecibidoEnDestino++;
                }
                if (x.Entregado)
                {
                    EntregadoAlCliente++;
                }
            });

            return new int[5] { RecibidoEnOrigen, EsperandoEnOrigen , EnViaje, RecibidoEnDestino, EntregadoAlCliente };
        }

        public int[] GraficasPorUsuario()
        {
            int administradores = 0;
            int funcionarios = 0;
            int clientes = 0;

            List<Usuario> usuarios = dal_usu.getAllUsuarios();
            usuarios.ForEach(x => {
                if (x.Rol == "Administrador")
                {
                    administradores++;
                }
                if (x.Rol == "Funcionario")
                {
                    funcionarios++;
                }
                if (x.Rol == "Cliente")
                {
                    clientes++;
                }
            });

            return new int[3] { administradores, funcionarios, clientes };
        }

        public bool IniciarSesion(string email, string pass)
        {
            Usuario usu = dal_usu.getUsuarioByEmail(email);
            if(usu != null)
            {
                if(usu.Password == pass)
                {
                    return true;
                }
            }
            return false;
        }

        public void ModificarAgencia(Agencia a)
        {
            if(a != null)
            {
                dal_age.UpdateAgencia(a);
            }
        }

        public void ModificarCliente(Usuario cli)
        {
            if(cli != null)
            {
                dal_usu.UpdateUsuario(cli);
            }
        }

        public void ModificarTrayecto(Trayecto t)
        {
            if(t != null)
            {
                dal_tr.UpdateTrayecto(t);
            }
        }

        public string NombreAgenciaDePuntoDeControl(int idPdc)
        {
            return dal_pdc.getNombreAgencia(idPdc);
        }

        public string[] NombresAgenciasOrigenDestino(int id)
        {
            Trayecto t = dal_paq.GetPaquete(id).Trayecto;
            return new string[2] {t.Origen.Nombre, t.Destino.Nombre };
        }

        public bool PuedeCambiarHoraDeEntregaPaquete(int id)
        {
            Paquete p = dal_paq.GetPaquete(id);
            if (p != null && p.PDCActual != null && p.PDCActual.Trayecto != null && p.PDCActual.Trayecto.Destino != null)
            {
                if (p.PDCActual.Trayecto.Destino.EntregaDomicilio)
                {
                    return true;
                }
            }
            return false;
        }

        public List<PuntoDeControl> PuntosDeControlDeTrayecto(int idTrayecto)
        {
            return dal_pdc.GetAllPuntosDeControlDeTrayecto(idTrayecto);
        }

        public bool RetrocederEstadoPaquete(int id)
        {
            bool done = false;
            Paquete p = dal_paq.GetPaquete(id);
            if (p != null)
            {
                string emailUsu = p.Destinatario.Email;
                List<PuntoDeControl> lista = dal_tr.getPuntosDeControl(p.Trayecto.Id);
                lista.Reverse();
                int indActual = p.PDCActual.Posicion;
                lista.ForEach(x => {
                    if (x.Posicion < indActual)
                    {
                        if (!done)
                        {
                            dal_paq.UpdatePDCActual(p.Id, x.Id);
                            var client = new RestClient("https://api.sendinblue.com/v3/smtp/email");
                            var request = new RestRequest(Method.POST);
                            request.AddHeader("accept", "application/json");
                            request.AddHeader("content-type", "application/json");
                            request.AddHeader("api-key", "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");
                            request.AddParameter("application/json", "{\"sender\":{\"name\":\"TrackingFULL\",\"email\":\"no-reply@trackingfull.com\"},\"to\":[{\"email\":\"" + emailUsu + "\",\"name\":\"" + p.Destinatario.Nombre + "\"}],\"textContent\":\"El nuevo estado de su paquete " + p.Descripcion + " es " + x.Nombre + ".\",\"subject\":\"TrackingFULL - Actualizacion de paquete\"}", ParameterType.RequestBody);
                            IRestResponse response = client.Execute(request);
                            done = true;
                        }
                    }
                });
            }
            return done;
        }

        public bool UpdateHoraDeEntrega(int id, int hora)
        {
            Paquete p = dal_paq.GetPaquete(id);
            if(p != null && p.PDCActual != null && p.PDCActual.Trayecto != null && p.PDCActual.Trayecto.Destino != null)
            {
                if (p.PDCActual.Trayecto.Destino.EntregaDomicilio)
                {
                    if(hora >= 0 && hora < 24)
                    {
                        dal_paq.UpdateHoraDeEntrega(id, hora);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
