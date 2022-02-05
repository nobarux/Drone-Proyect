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
        // GET: api/Dispatch/GetDron
        [Route("api/Dispatch/GetDron")]
        public IQueryable<Drone> GetDron()
        {
            return db.Drone;
        }

        // POST: api/Dispatch/PostDrone
        [Route("api/Dispatch/PostDrone")]
        public HttpResponseMessage Post([FromBody] Drone drone)
        {
            try
            {
                using (drone_proyEntities dron = new drone_proyEntities())
                {
                    dron.Drone.Add(drone);
                    dron.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, drone);
                    message.Headers.Location = new Uri(Request.RequestUri + drone.id_Drone.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        // GET: api/Dispatch/DronesLoad
        [Route("api/Dispatch/DronesLoad")]
        public HttpResponseMessage GetDroneforLoading()
        {
            using (drone_proyEntities db = new drone_proyEntities())
            {
                var droneToload = (from droload in db.Drone
                                   where droload.state == "LOADING"
                                   select new { Serial_Number = droload.serial_number, Model = droload.model, Battery = droload.battery, State = droload.state }).ToList();
                return Request.CreateResponse(HttpStatusCode.Found, droneToload);
            }
        }

        // GET: api/Dispatch/BatteryDrone/5
        [Route("api/Dispatch/BatteryDrone/{id}")]
        public HttpResponseMessage GetDroneBattery(long id)
        {
            using (drone_proyEntities db = new drone_proyEntities())
            {

                Drone drone = db.Drone.Find(id);
                if (drone == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                else
                {
                    var droneToload = (from droBat in db.Drone
                                       where droBat.id_Drone == id
                                       select droBat.battery).SingleOrDefault();
                    return Request.CreateResponse(HttpStatusCode.Found, droneToload);
                }
                
            }
        }

        // GET: api/Dispatch/MedicationDrone/5
        [Route("api/Dispatch/MedicationDrone/{id}")]
        public HttpResponseMessage GetDroneMedication(long id)
        {
            using (drone_proyEntities db = new drone_proyEntities())
            {

                Drone drone = db.Drone.Find(id);
                if (drone == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                else
                {
                    var droneToload = (from droBat in db.Drone
                                       where droBat.id_Drone == id
                                       select droBat.battery).SingleOrDefault();
                    return Request.CreateResponse(HttpStatusCode.Found, droneToload);
                }

            }
        }



        // POST: api/Dispatch/LoadDrone
        [Route("api/Dispatch/LoadDrone")]
        public HttpResponseMessage Post([FromBody]DronMed dronemed)
        {
            try
            {
                var message = Request.CreateResponse(HttpStatusCode.Created, dronemed);
                #region drone validation
                using (drone_proyEntities db = new drone_proyEntities())
                {
                    //Datos que vienen del body
                    var datos = dronemed;
                    var dron = datos.id_Drone;
                    var medi = datos.id_Med;
                    //--//

                    //Data from 
                    var especificDron = (from drones in db.DronMed where drones.id_Drone == dron select drones).ToList();
                    double pesoAcumulado = 0;
                    foreach (var item in especificDron)
                    {
                        var weightMed = (from med in db.Medication where med.id_Med == item.id_Med select med.weigth).SingleOrDefault();
                        pesoAcumulado += Convert.ToDouble(weightMed);
                    }
                    //--//
                    //Weights
                    var weightDron = (from drones in db.Drone where drones.id_Drone == dron select drones.weight).SingleOrDefault();
                    var sendingweightMed = (from med in db.Medication where med.id_Med == medi select med.weigth).SingleOrDefault();
                    pesoAcumulado += Convert.ToDouble(sendingweightMed);

                    if (pesoAcumulado > weightDron)
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "This drone can't load this much weight");
                    }
                    else
                    {
                        var loadingDrone = (from loadDrone in db.Drone where loadDrone.id_Drone == dron select loadDrone).SingleOrDefault();

                        if (loadingDrone.battery < 25)
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest, "This drone have a low battery to load any medication,please charge it");
                        }
                        else
                        {
                            //Change the status of the drone from idle to loading
                            loadingDrone.state = "LOADING";
                            db.SaveChanges();
                            ///----///
                            /// 
                            db.DronMed.Add(dronemed);
                            db.SaveChanges();
                            return Request.CreateResponse(HttpStatusCode.OK, "Drone loaded succefully");
                        }
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }


    }
}
