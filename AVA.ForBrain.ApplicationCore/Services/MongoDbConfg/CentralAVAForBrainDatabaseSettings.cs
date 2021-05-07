using System;
using System.Collections.Generic;
using System.Text;

namespace AVA.ForBrain.ApplicationCore.Services.MongoDbConfg
{
    public class CentralAVAForBrainDatabaseSettings
    {
        public List<string> UsuariosCollectionName { get; set; }
        public string ConnectioString { get; set; }
        public string DatabaseName { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
    public interface ICentralAVAForBrainDatabaseSettings
    {
        List<string> UsuariosCollectionName { get; set; }
        string ConnectioString { get; set; }
        string DatabaseName { get; set; }
        string User { get; set; }
        string Password { get; set; }
    }
}
