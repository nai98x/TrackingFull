using Shared.Entities;
using System.Collections.Generic;
using System.ServiceModel;

namespace ServiceLayer
{
    [ServiceContract()]
    public interface IServicePaquete
    {
        [OperationContract()]
        void AddPaquete(Paquete paq);

        [OperationContract()]
        void DeletePaquete(int id);

        [OperationContract()]
        void UpdatePaquete(Paquete paq);

        [OperationContract()]
        List<Paquete> GetAllPaquetes();

        [OperationContract()]
        Paquete GetPaquete(int id);

        [OperationContract()]
        Paquete GetPaqueteByCodigoExterno(string codigo);
    }
}
