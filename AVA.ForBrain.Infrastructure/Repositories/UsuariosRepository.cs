using AVA.ForBrain.ApplicationCore.Entities;
using AVA.ForBrain.ApplicationCore.Interfaces.Repositories;
using AVA.ForBrain.ApplicationCore.Interfaces.Repositories.Common;
using AVA.ForBrain.Infrastructure.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AVA.ForBrain.Infrastructure.Repositories
{
    public class UsuariosRepository : BaseRepository<Usuarios>, IUsuariosRepository
    {
        public UsuariosRepository(IMongoContext context) : base(context)
        {
        }
    }
}
