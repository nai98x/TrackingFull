using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IBLUsuarios
    {
        void AddUsuario(Usuario usu);

        void DeleteUsuario(int id);

        void UpdateUsuario(Usuario usu);

        List<Usuario> getAllUsuarios();

        Usuario getUsuario(int id);

        Usuario getUsuarioByEmail(string email);

        void AgregarPaquete(Paquete paq);

        List<Paquete> GetPaquetesEnviados(int id);

        List<Paquete> GetPaquetesRecibidos(int id);

        Usuario GetUsuarioByCodigoExterno(string codigo);
    }
}
