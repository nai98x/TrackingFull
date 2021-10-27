using Shared.Entities;
using System.Collections.Generic;
using System.ServiceModel;

namespace ServiceLayer
{
    [ServiceContract()]
    public interface IServicePuntosDeControl
    {
        [OperationContract()]
        void AddPuntoDeControl(PuntoDeControl p);

        [OperationContract()]
        void DeletePuntoDeControl(int id, int idTr);

        [OperationContract()]
        void UpdatePuntoDeControl(PuntoDeControl p);

        [OperationContract()]
        List<PuntoDeControl> GetAllPuntosDeControl();

        [OperationContract()]
        PuntoDeControl GetPuntoDeControl(int id);

        [OperationContract()]
        List<PuntoDeControl> GetAllPuntosDeControlDeTrayecto(int idTrayecto);

        [OperationContract()]
        PuntoDeControl GetPuntoDeControlByCodigoExterno(string codigo);
    }
}
