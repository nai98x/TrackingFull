using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer;
using Shared.Entities;

namespace ServiceLayer
{
    class ServiceCasosDeUso : IServiceCasosDeUso
    {
        private static IBLCasosDeUso blHandler;

        public ServiceCasosDeUso()
        {
            blHandler = Program.blHandler;
        }

        public void AltaAgencia(Agencia a)
        {
            blHandler.AltaAgencia(a);
        }

        public void AltaCliente(Usuario cli)
        {
            blHandler.AltaCliente(cli);
        }

        public void AltaPaquete(Paquete p, Agencia ag)
        {
            blHandler.AltaPaquete(p, ag);
        }

        public void AltaTrayecto(Trayecto t)
        {
            blHandler.AltaTrayecto(t);
        }

        public void AsignarAdministrador(Usuario adm)
        {
            blHandler.AsignarAdministrador(adm);
        }

        public void AsignarEmpleado(Usuario emp)
        {
            blHandler.AsignarEmpleado(emp);
        }

        public void AsignarSistemaExterno(Usuario usu)
        {
            blHandler.AsignarSistemaExterno(usu);
        }

        public bool AvanzarEstadoPaquete(int id)
        {
            return blHandler.AvanzarEstadoPaquete(id);
        }

        public void BajaAgencia(int idAgencia)
        {
            blHandler.BajaAgencia(idAgencia);
        }

        public void BajaCliente(int id)
        {
            blHandler.BajaCliente(id);
        }

        public void BajaTrayecto(int idTrayecto)
        {
            blHandler.BajaTrayecto(idTrayecto);
        }

        public bool CambiarPermisos(int id, string rol)
        {
            return blHandler.CambiarPermisos(id, rol);
        }

        public void CoordinarHoraEntregaPaquete(Paquete p)
        {
            blHandler.CoordinarHoraEntregaPaquete(p);
        }

        public bool FinalizarEntregaPaquete(int id, string codigoVerificacion)
        {
            return blHandler.FinalizarEntregaPaquete(id, codigoVerificacion);
        }

        public List<Usuario> GetClientes()
        {
            return blHandler.GetClientes();
        }

        public string GetEstadoPaquete(int id)
        {
            return blHandler.GetEstadoPaquete(id);
        }

        public int GetHoraDeEntregaPaquete(int id)
        {
            return blHandler.GetHoraDeEntregaPaquete(id);
        }

        public List<Paquete> getPaquetesFiltro(DateTime fechaDesde, DateTime fechaHasta, string estado, int idDestinatario, int idRemitente)
        {
            return blHandler.getPaquetesFiltro(fechaDesde, fechaHasta, estado, idDestinatario, idRemitente);
        }

        public string GetPassByEmail(string email)
        {
            return blHandler.GetPassByEmail(email);
        }

        public int[] GraficasPorEstado()
        {
            return blHandler.GraficasPorEstado();
        }

        public int[] GraficasPorUsuario()
        {
            return blHandler.GraficasPorUsuario();
        }

        public bool IniciarSesion(string email, string pass)
        {
            return blHandler.IniciarSesion(email, pass);
        }

        public void ModificarAgencia(Agencia a)
        {
            blHandler.ModificarAgencia(a);
        }

        public void ModificarCliente(Usuario cli)
        {
            blHandler.ModificarCliente(cli);
        }

        public void ModificarTrayecto(Trayecto t)
        {
            blHandler.ModificarTrayecto(t);
        }

        public string NombreAgenciaDePuntoDeControl(int idPdc)
        {
            return blHandler.NombreAgenciaDePuntoDeControl(idPdc);
        }

        public string[] NombresAgenciasOrigenDestino(int id)
        {
            return blHandler.NombresAgenciasOrigenDestino(id);
        }

        public bool PuedeCambiarHoraDeEntregaPaquete(int id)
        {
            return blHandler.PuedeCambiarHoraDeEntregaPaquete(id);
        }

        public List<PuntoDeControl> PuntosDeControlDeTrayecto(int idTrayecto)
        {
            return blHandler.PuntosDeControlDeTrayecto(idTrayecto);
        }

        public bool RetrocederEstadoPaquete(int id)
        {
            return blHandler.RetrocederEstadoPaquete(id);
        }

        public bool UpdateHoraDeEntrega(int id, int hora)
        {
            return blHandler.UpdateHoraDeEntrega(id, hora);
        }
    }
}
