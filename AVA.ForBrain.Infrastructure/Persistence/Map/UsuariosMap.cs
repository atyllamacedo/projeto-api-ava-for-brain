using AVA.ForBrain.ApplicationCore.Entities;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace AVA.ForBrain.Infrastructure.Persistence.Map
{
    public class UsuariosMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Usuarios>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdMember(x => x.IdUsuario);              
            });
        }
    }
}
