using Shared.Entities;
using System.Collections.Generic;
using System.ServiceModel;

namespace ServiceLayer
{
    [ServiceContract()]
    public interface IServiceTrayecto
    {
        [OperationContract()]
        void AddTrayecto(Trayecto tr);

        [OperationContract()]
        void DeleteTrayecto(int id);

        [OperationContract()]
        void UpdateTrayecto(Trayecto tr);

        [OperationContract()]
        List<Trayecto> getAllTrayectos();

        [OperationContract()]
        Trayecto getTrayecto(int id);

        [OperationContract()]
        Trayecto GetTrayectoByCodigoExterno(string codigo);
    }
}
