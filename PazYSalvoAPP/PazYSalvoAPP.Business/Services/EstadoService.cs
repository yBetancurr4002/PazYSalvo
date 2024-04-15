using PazYSalvoAPP.Models;
using PazYSalvoAPP.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PazYSalvoAPP.Business.Services
{
    public class EstadoService : IEstadoService
    {
        private readonly IGenericRepository<Estado> _modelRepository;
        public EstadoService(IGenericRepository<Estado> modelRepository)
        {
            _modelRepository = modelRepository;
        }
        public async Task<bool> Actualizar(Estado model)
        {
            return await _modelRepository.Actualizar(model);
        }

        public async Task<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Insertar(Estado model)
        {
            return await _modelRepository.Insertar(model);
        }

        public async Task<Estado> Leer(int id)
        {
            return await _modelRepository.Leer(id);
        }

        public async Task<IQueryable<Estado>> LeerTodos()
        {
            return await _modelRepository.LeerTodos();
        }

        
    }
}
