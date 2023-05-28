using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace EuroGUI
{
    internal class Dal
    {

        public int Ev { get; set; }
        public string Eloado { get; set; }
        public string Cim { get; set; }
        public int Helyezes { get; set; }
        public int Pontszam { get; set; }
        
        public Dal(MySqlDataReader olvaso)
        {
            Ev = olvaso.GetInt16(0);
            Eloado = olvaso.GetString(1);
            Cim = olvaso.GetString(2);
            Helyezes = olvaso.GetInt16(3);
            Pontszam = olvaso.GetInt16(4);
        }
    }
}
