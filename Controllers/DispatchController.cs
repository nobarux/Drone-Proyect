using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Drone_Proyect.Models;
using System.Web.Http.Description;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Drone_Proyect.Controllers
{
    public class DispatchController : ApiController
    {
        private drone_proyEntities db = new drone_proyEntities();

        // POST: api/Dispatch/
        //[Route("Dispatch/LoadDrone")]

        public HttpResponseMessage Post([FromBody]DronMed dronemed)
        {
            try
            {
                var message = Request.CreateResponse(HttpStatusCode.Created, dronemed);

                using (drone_proyEntities db = new drone_proyEntities())
                {
                    //Datos que vienen del body
                    var datos = dronemed;
                    var dron = datos.id_Drone;
                    var medi = datos.id_Med;

                    //Pesos respectivos
                    var weightDron = (from drones in db.Drone where drones.id_Drone == dron select drones.weight).SingleOrDefault();
                    var weightMed = (from med in db.Medication where med.id_Med == medi select med.weigth).SingleOrDefault();
                    if (weightMed > weightDron)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "The med is heavier than the max weigth of the drone");
                    }
                    else
                    {
                        db.DronMed.Add(dronemed);
                        return Request.CreateResponse(HttpStatusCode.OK);

                        //message.Headers.Location = new Uri(Request.RequestUri + dronemed.id.ToString());
                        //return message;
                    }

                    
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }


    }
}
