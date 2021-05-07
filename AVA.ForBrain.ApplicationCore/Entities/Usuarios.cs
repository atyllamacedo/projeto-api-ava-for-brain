using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AVA.ForBrain.ApplicationCore.Entities
{
    public class Usuarios
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdUsuario { get; set; }

        [BsonElement("Nome")]
        public string User { get; set; }

        [BsonElement("Sobrenome")]
        public string Sobrenome { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Senha")]
        public string Password { get; set; }

        [BsonElement("DataNascimento")]
        public Nullable<DateTime> DtNascimento { get; set; }

        [BsonElement("Genero")]
        public string Genero { get; set; }

        [BsonElement("Perfil")]
        public string PerfilDescricao { get; set; }

        [BsonElement("Ativo")]
        public Int32 Ativo { get; set; }

        [BsonElement("Role")]
        public string Role { get; set; }
    }
}
