using Shared.Entities;
using System.Collections.Generic;
using System.ServiceModel;

namespace ServiceLayer
{
    [ServiceContract()]
    public interface IServiceUsuario
    {
        [OperationContract()]
        void AddUsuario(Usuario usu);

        [OperationContract()]
        void DeleteUsuario(int id);

        [OperationContract()]
        void UpdateUsuario(Usuario usu);

        [OperationContract()]
        List<Usuario> getAllUsuarios();

        [OperationContract()]
        Usuario getUsuario(int id);

        [OperationContract()]
        Usuario getUsuarioByEmail(string email);

        [OperationContract()]
        void AgregarPaquete(Paquete paq);

        [OperationContract()]
        List<Paquete> GetPaquetesEnviados(int id);

        [OperationContract()]
        List<Paquete> GetPaquetesRecibidos(int id);

        [OperationContract()]
        Usuario GetUsuarioByCodigoExterno(string codigo);
    }
}
