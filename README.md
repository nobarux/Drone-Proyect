# Drone-Proyect.md


**Table of Contents**

[TOCM]

[TOC]

#PROJECT DESCRIPTION
** This is a simple rest web api made in C#. This API allows to know some information about drones and communicate with them.
 In this particular case the drones might be loaded by medication. **
 #DATATABLE AND PROJECT CONTENT:
 The datatable contains only three tables: Medication, Drone and DroneMed. The later one its a many-to-many table created to know how many medication a given drone may have.
 The database name is  **drone_proy** and its included with the proyect.
 This DB was made in **SQLite** and you can use any Database Manager at your dispose to connect with it.
 
 ####Example get method

```c#
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
```

#HOW TO USE IT
You just need to download the project run the solution named **Drone-Proyect** and load the database for a proper connection.
No password required for using this project just open it and run it.
#CREDITS
if you have any doubts about the project you can contact me in darrietaval@gmail.com

-------------

 
 
 