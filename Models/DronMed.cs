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
    
    public partial class DronMed
    {
        public long id { get; set; }
        public Nullable<long> id_Med { get; set; }
        public Nullable<long> id_Drone { get; set; }
    
        public  Drone Drone { get; set; }
        public  Medication Medication { get; set; }
    }
}
