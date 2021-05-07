using AVA.ForBrain.Infrastructure.Persistence.Map;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AVA.ForBrain.Infrastructure.Persistence
{
    public class MongoDbPersistence
    {
        [System.Obsolete]
        public static void Configure()
        {
            UsuariosMap.Configure();
            BsonDefaults.GuidRepresentation = GuidRepresentation.CSharpLegacy;
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.CSharpLegacy));
            // Conventions
            var pack = new ConventionPack
            {
                    new IgnoreExtraElementsConvention(true),
                    new IgnoreIfDefaultConvention(true)
            };
            ConventionRegistry.Register("My Solution Conventions", pack, t => true);
        }
    }
}

