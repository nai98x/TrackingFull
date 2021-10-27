using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Shared.Entities;

namespace BusinessLogicLayer
{
    public class BLUsuarios : IBLUsuarios
    {
        private IDALUsuarios dal_usu;

        public BLUsuarios()
        {
            dal_usu = new DALUsuarios_EF();
        }

        public void AddUsuario(Usuario usu)
        {
            dal_usu.AddUsuario(usu);
        }

        public void DeleteUsuario(int id)
        {
            dal_usu.DeleteUsuario(id);
        }

        public List<Usuario> getAllUsuarios()
        {
           return dal_usu.getAllUsuarios();
        }

        public Usuario getUsuario(int id)
        {
            return dal_usu.getUsuario(id);
        }

        public void UpdateUsuario(Usuario usu)
        {
            dal_usu.UpdateUsuario(usu);
        }

        public Usuario getUsuarioByEmail(string email)
        {
            return dal_usu.getUsuarioByEmail(email);
        }

        public void AgregarPaquete(Paquete paq)
        {
            dal_usu.AgregarPaquete(paq);
        }

        public List<Paquete> GetPaquetesEnviados(int id)
        {
            return dal_usu.GetPaquetesEnviados(id);
        }

        public List<Paquete> GetPaquetesRecibidos(int id)
        {
            return dal_usu.GetPaquetesRecibidos(id);
        }

        public Usuario GetUsuarioByCodigoExterno(string codigo)
        {
            return dal_usu.getUsuarioByCodigoExterno(codigo);
        }
    }
}
