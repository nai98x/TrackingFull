using Shared.Entities;
using System.Collections.Generic;
using System.ServiceModel;

namespace ServiceLayer
{
    [ServiceContract()]
    public interface IServiceAgencia
    {
        [OperationContract()]
        void AddAgencia(Agencia ag);

        [OperationContract()]
        void DeleteAgencia(int id);

        [OperationContract()]
        void UpdateAgencia(Agencia ag);

        [OperationContract()]
        List<Agencia> GetAllAgencias();

        [OperationContract()]
        Agencia GetAgencia(int id);

        [OperationContract()]
        Agencia GetAgenciaByCodigoExterno(string codigo);
    }
}
