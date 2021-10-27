using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer;
using Shared.Entities;
using System.ServiceModel;

namespace ServiceLayer
{
    class ServiceUsuarioProxy : ClientBase<IServiceUsuario>, IServiceUsuario
    {
        public void AddUsuario(Usuario usu)
        {
            base.Channel.AddUsuario(usu);
        }

        public void AgregarPaquete(Paquete paq)
        {
            base.Channel.AgregarPaquete(paq);
        }

        public void DeleteUsuario(int id)
        {
            base.Channel.DeleteUsuario(id);
        }

        public List<Usuario> getAllUsuarios()
        {
            return base.Channel.getAllUsuarios();
        }

        public List<Paquete> GetPaquetesEnviados(int id)
        {
            return base.Channel.GetPaquetesEnviados(id);
        }

        public List<Paquete> GetPaquetesRecibidos(int id)
        {
            return base.Channel.GetPaquetesRecibidos(id);
        }

        public Usuario getUsuario(int id)
        {
            return base.Channel.getUsuario(id);
        }

        public Usuario GetUsuarioByCodigoExterno(string codigo)
        {
            return base.Channel.GetUsuarioByCodigoExterno(codigo);
        }

        public Usuario getUsuarioByEmail(string email)
        {
            return base.Channel.getUsuarioByEmail(email);
        }

        public void UpdateUsuario(Usuario usu)
        {
            base.Channel.UpdateUsuario(usu);
        }
    }
}
