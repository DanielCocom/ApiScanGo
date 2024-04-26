using System.Threading.Tasks;
using api_scango.Domain.Dtos;
using api_scango.Domain.Entities;
using api_scango.Infrastructure.Data.Repositories;

namespace api_scango.Domain.Services
{

    public class ClienteService
    {
        private readonly ClienteRepository _clienteRepository;

        public ClienteService(ClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<Cliente> ObtenerClientePorTelefonoAsync(string numerodetelefono)
        {
            return await _clienteRepository.GetById(numerodetelefono);
        }

        public async Task RegistrarNuevoClienteAsync(Cliente nuevoCliente)
        {
            await _clienteRepository.AgregarClienteAsync(nuevoCliente);
        }
        public async Task<Cliente> InicioSesion(string telefono, string contraseña)
        {
            var cliente = await _clienteRepository.GetById(telefono);

            if (cliente.Contrasena == contraseña)
            {
                // Éxito en la autenticación.
                return cliente;
            }
            else
            {
                // Contraseña incorrecta u otra lógica de autenticación fallida.
                throw new Exception("Credenciales incorrectas");
            }
        }
        public async Task UpdateUser(ClienteCreateDTO cliente)
        {
            await _clienteRepository.Update(cliente);
        }
    }
}
