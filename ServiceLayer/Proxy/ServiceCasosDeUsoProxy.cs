using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer;
using Shared.Entities;
using System.ServiceModel;

namespace ServiceLayer
{
    class ServiceCasosDeUsoProxy : ClientBase<IServiceCasosDeUso>, IServiceCasosDeUso
    {
        public void AltaAgencia(Agencia a)
        {
            base.Channel.AltaAgencia(a);
        }

        public void AltaCliente(Usuario cli)
        {
            base.Channel.AltaCliente(cli);
        }

        public void AltaPaquete(Paquete p, Agencia ag)
        {
            base.Channel.AltaPaquete(p, ag);
        }

        public void AltaTrayecto(Trayecto t)
        {
            base.Channel.AltaTrayecto(t);
        }

        public void AsignarAdministrador(Usuario adm)
        {
            base.Channel.AsignarAdministrador(adm);
        }

        public void AsignarEmpleado(Usuario emp)
        {
            base.Channel.AsignarEmpleado(emp);
        }

        public void AsignarSistemaExterno(Usuario usu)
        {
            base.Channel.AsignarSistemaExterno(usu);
        }

        public bool AvanzarEstadoPaquete(int id)
        {
            return base.Channel.AvanzarEstadoPaquete(id);
        }

        public void BajaAgencia(int idAgencia)
        {
            base.Channel.BajaAgencia(idAgencia);
        }

        public void BajaCliente(int id)
        {
            base.Channel.BajaCliente(id);
        }

        public void BajaTrayecto(int idTrayecto)
        {
            base.Channel.BajaTrayecto(idTrayecto);
        }

        public bool CambiarPermisos(int id, string rol)
        {
            return base.Channel.CambiarPermisos(id, rol);
        }

        public void CoordinarHoraEntregaPaquete(Paquete p)
        {
            base.Channel.CoordinarHoraEntregaPaquete(p);
        }

        public bool FinalizarEntregaPaquete(int id, string codigoVerificacion)
        {
            return base.Channel.FinalizarEntregaPaquete(id, codigoVerificacion);
        }

        public List<Usuario> GetClientes()
        {
            return base.Channel.GetClientes();
        }

        public string GetEstadoPaquete(int id)
        {
            return base.Channel.GetEstadoPaquete(id);
        }

        public int GetHoraDeEntregaPaquete(int id)
        {
            return base.Channel.GetHoraDeEntregaPaquete(id);
        }

        public List<Paquete> getPaquetesFiltro(DateTime fechaDesde, DateTime fechaHasta, string estado, int idDestinatario, int idRemitente)
        {
            return base.Channel.getPaquetesFiltro(fechaDesde, fechaHasta, estado, idDestinatario, idRemitente);
        }

        public string GetPassByEmail(string email)
        {
            return base.Channel.GetPassByEmail(email);
        }

        public int[] GraficasPorEstado()
        {
            return base.Channel.GraficasPorEstado();
        }

        public int[] GraficasPorUsuario()
        {
            return base.Channel.GraficasPorUsuario();
        }

        public bool IniciarSesion(string email, string pass)
        {
            return base.Channel.IniciarSesion(email, pass);
        }

        public void ModificarAgencia(Agencia a)
        {
            base.Channel.ModificarAgencia(a);
        }

        public void ModificarCliente(Usuario cli)
        {
            base.Channel.ModificarCliente(cli);
        }

        public void ModificarTrayecto(Trayecto t)
        {
            base.Channel.ModificarTrayecto(t);
        }

        public string NombreAgenciaDePuntoDeControl(int idPdc)
        {
            return base.Channel.NombreAgenciaDePuntoDeControl(idPdc);
        }

        public string[] NombresAgenciasOrigenDestino(int id)
        {
            return base.Channel.NombresAgenciasOrigenDestino(id);
        }

        public bool PuedeCambiarHoraDeEntregaPaquete(int id)
        {
            return base.Channel.PuedeCambiarHoraDeEntregaPaquete(id);
        }

        public List<PuntoDeControl> PuntosDeControlDeTrayecto(int idTrayecto)
        {
            return base.Channel.PuntosDeControlDeTrayecto(idTrayecto);
        }

        public bool RetrocederEstadoPaquete(int id)
        {
            return base.Channel.RetrocederEstadoPaquete(id);
        }

        public bool UpdateHoraDeEntrega(int id, int hora)
        {
            return base.Channel.UpdateHoraDeEntrega(id, hora);
        }
    }
}
