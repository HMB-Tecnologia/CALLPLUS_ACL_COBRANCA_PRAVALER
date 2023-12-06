using System;
using System.Collections.Generic;
using System.Text;
//using v1Tabulare_z13.wenvpabx2;

namespace ws
{
    public class PlanetFone
    {
        private static PlanetFone instancia;

        private PlanetFone()
        {
        }

        public static PlanetFone getInstancia()
        {
            if (instancia == null)
                instancia = new PlanetFone();

            return instancia;
        }

        //public wenvpabx2 getWS()
        //{
        //    return new wenvpabx2();
        //}
    }
}
