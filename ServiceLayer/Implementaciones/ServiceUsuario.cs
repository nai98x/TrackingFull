using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer;
using Shared.Entities;

namespace ServiceLayer
{
    class ServiceUsuario : IServiceUsuario
    {
        private static IBLUsuarios blHandler;

        public ServiceUsuario()
        {
            blHandler = Program.blHandlerUsu;
        }

        public void AddUsuario(Usuario usu)
        {
            blHandler.AddUsuario(usu);
        }

        public void AgregarPaquete(Paquete paq)
        {
            blHandler.AgregarPaquete(paq);
        }

        public void DeleteUsuario(int id)
        {
            blHandler.DeleteUsuario(id);
        }

        public List<Usuario> getAllUsuarios()
        {
            return blHandler.getAllUsuarios();
        }

        public List<Paquete> GetPaquetesEnviados(int id)
        {
            return blHandler.GetPaquetesEnviados(id);
        }

        public List<Paquete> GetPaquetesRecibidos(int id)
        {
            return blHandler.GetPaquetesRecibidos(id);
        }

        public Usuario getUsuario(int id)
        {
            return blHandler.getUsuario(id);
        }

        public Usuario GetUsuarioByCodigoExterno(string codigo)
        {
            return blHandler.GetUsuarioByCodigoExterno(codigo);
        }

        public Usuario getUsuarioByEmail(string email)
        {
            return blHandler.getUsuarioByEmail(email);
        }

        public void UpdateUsuario(Usuario usu)
        {
            blHandler.UpdateUsuario(usu);
        }
    }
}
