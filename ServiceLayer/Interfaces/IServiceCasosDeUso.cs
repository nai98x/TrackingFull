using Shared.Entities;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ServiceLayer
{
    [ServiceContract()]
    public interface IServiceCasosDeUso
    {
        [OperationContract()]
        bool IniciarSesion(string email, string pass);

        // Superadministrador
        // Administrador
        [OperationContract()]
        void AsignarAdministrador(Usuario adm);

        [OperationContract()]
        void AsignarEmpleado(Usuario emp);

        [OperationContract()]
        void AltaAgencia(Agencia a);

        [OperationContract()]
        void BajaAgencia(int idAgencia);

        [OperationContract()]
        void ModificarAgencia(Agencia a);

        [OperationContract()]
        void AltaTrayecto(Trayecto t);

        [OperationContract()]
        void BajaTrayecto(int idTrayecto);

        [OperationContract()]
        void ModificarTrayecto(Trayecto t);

        // Funcionario
        [OperationContract()]
        void AltaCliente(Usuario cli);

        [OperationContract()]
        void BajaCliente(int id);

        [OperationContract()]
        void ModificarCliente(Usuario cli);

        // Cliente
        [OperationContract()]
        void CoordinarHoraEntregaPaquete(Paquete p);

        [OperationContract()]
        List<Usuario> GetClientes();
        string GetPassByEmail(string email);

        // Puntos de Control
        [OperationContract()]
        List<PuntoDeControl> PuntosDeControlDeTrayecto(int idTrayecto);

        [OperationContract()]
        string NombreAgenciaDePuntoDeControl(int idPdc);

        // Paquetes
        [OperationContract()]
        string GetEstadoPaquete(int id);

        [OperationContract()]
        void AltaPaquete(Paquete p, Agencia ag);

        [OperationContract()]
        bool AvanzarEstadoPaquete(int id);

        [OperationContract()]
        bool RetrocederEstadoPaquete(int id);

        [OperationContract()]
        bool FinalizarEntregaPaquete(int id, string codigoVerificacion);

        [OperationContract()]
        string[] NombresAgenciasOrigenDestino(int id);

        [OperationContract()]
        List<Paquete> getPaquetesFiltro(DateTime fechaDesde, DateTime fechaHasta, string estado, int idDestinatario, int idRemitente);

        [OperationContract()]
        bool UpdateHoraDeEntrega(int id, int hora);

        [OperationContract()]
        int GetHoraDeEntregaPaquete(int id);

        [OperationContract()]
        bool PuedeCambiarHoraDeEntregaPaquete(int id);

        [OperationContract()]
        bool CambiarPermisos(int id, string rol);

        [OperationContract()]
        int[] GraficasPorEstado();

        [OperationContract()]
        void AsignarSistemaExterno(Usuario usu);

        [OperationContract()]
        int[] GraficasPorUsuario();
    }
}
