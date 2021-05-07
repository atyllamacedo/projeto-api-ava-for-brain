using System;
using System.Collections.Generic;
using System.Text;

namespace AVA.ForBrain.ApplicationCore.AppSetting
{
    public class ConstSetting
    {
        private static string _MongoConnectionString;
        private static string _MongoDatabase;
        private static string _Ambiente;
        public static string Ambiente
        {
            get
            {
                return _Ambiente;
            }
            set
            {
                _Ambiente = value;
            }
        }
        public static string MongoConnectionString
        {
            get
            {
                _MongoConnectionString = "mongodb+srv://dbUser:l5ayaykjzKRGmRfr@projetointegrado.skaip.mongodb.net/AVAForBrain?retryWrites=true&w=majority";
                return _MongoConnectionString;
            }
        }
        public string MongoContainer
        {
            get
            {
                switch (Ambiente)
                {
                    case "DEV":
                        _MongoDatabase = "AVAForBrain";
                        break;
                    default:
                        _MongoDatabase = null;
                        break;
                }
                return _MongoDatabase;
            }

        }
    }
}
