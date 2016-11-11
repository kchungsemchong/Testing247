using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMapsConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Marker marker = new Marker();
            //marker.ConvertPostCodeToMarker("Avenue Des Jacarandas, Quatre Bornes, Mauritius");
            marker.RetrieveRestrictedCoordinates("Avenue Des Jacarandas, Quatre Bornes, Mauritius");
        }
    }
}
