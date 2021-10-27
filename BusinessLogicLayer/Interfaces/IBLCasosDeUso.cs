using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Entities;

namespace BusinessLogicLayer
{
    public interface IBLCasosDeUso
    {
        // Funcionalidades comunes
        bool IniciarSesion(string email, string pass);

        // Superadministrador
        // Administrador
        void AsignarAdministrador(Usuario adm);

        void AsignarEmpleado(Usuario emp);
        void AltaAgencia(Agencia a);
        void BajaAgencia(int idAgencia);
        void ModificarAgencia(Agencia a);
        void AltaTrayecto(Trayecto t);
        void BajaTrayecto(int idTrayecto);
        void ModificarTrayecto(Trayecto t);

        // Funcionario
        void AltaCliente(Usuario cli);
        void BajaCliente(int id);
        void ModificarCliente(Usuario cli);

        // Cliente
        void CoordinarHoraEntregaPaquete(Paquete p);
        List<Usuario> GetClientes();
        string GetPassByEmail(string email);

        // Puntos de Control
        List<PuntoDeControl> PuntosDeControlDeTrayecto(int idTrayecto);
        string NombreAgenciaDePuntoDeControl(int idPdc);

        // Paquetes
        string GetEstadoPaquete(int id);
        void AltaPaquete(Paquete p, Agencia ag);
        bool AvanzarEstadoPaquete(int id);
        bool RetrocederEstadoPaquete(int id);
        bool FinalizarEntregaPaquete(int id, string codigoVerificacion);
        string[] NombresAgenciasOrigenDestino(int id);
        List<Paquete> getPaquetesFiltro(DateTime fechaDesde, DateTime fechaHasta, string estado, int idDestinatario, int idRemitente);
        bool UpdateHoraDeEntrega(int id, int hora);
        int GetHoraDeEntregaPaquete(int id);
        bool PuedeCambiarHoraDeEntregaPaquete(int id);
        bool CambiarPermisos(int id, string rol);
        int[] GraficasPorEstado();
        void AsignarSistemaExterno(Usuario usu);
        int[] GraficasPorUsuario();
    }
}
