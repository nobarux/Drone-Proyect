using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Drone_Proyect.Models;

namespace Drone_Proyect.Controllers
{
    public class DronesController : ApiController
    {
        private drone_proyEntities db = new drone_proyEntities();

        // GET: api/Drones
        public IQueryable<Drone> GetDrone()
        {
            return db.Drone;
        }

        // GET: api/Drones/5
        [ResponseType(typeof(Drone))]
        public IHttpActionResult GetDrone(long id)
        {
            Drone drone = db.Drone.Find(id);
            if (drone == null)
            {
                return NotFound();
            }

            return Ok(drone);
        }

        // PUT: api/Drones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDrone(long id, Drone drone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != drone.id_Drone)
            {
                return BadRequest();
            }

            db.Entry(drone).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DroneExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Drones
        [ResponseType(typeof(Drone))]
        public IHttpActionResult PostDrone(Drone drone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Drone.Add(drone);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = drone.id_Drone }, drone);
        }

        // DELETE: api/Drones/5
        [ResponseType(typeof(Drone))]
        public IHttpActionResult DeleteDrone(long id)
        {
            Drone drone = db.Drone.Find(id);
            if (drone == null)
            {
                return NotFound();
            }

            db.Drone.Remove(drone);
            db.SaveChanges();

            return Ok(drone);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DroneExists(long id)
        {
            return db.Drone.Count(e => e.id_Drone == id) > 0;
        }
    }
}