//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Drone_Proyect.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Drone
    {
        public long id_drone { get; set; }
        public string serial_number { get; set; }
        public string model { get; set; }
        public Nullable<double> weight { get; set; }
        public Nullable<long> battery { get; set; }
        public string state { get; set; }
    }
}