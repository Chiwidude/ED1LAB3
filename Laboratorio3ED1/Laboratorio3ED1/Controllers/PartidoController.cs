using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Laboratorio3ED1.Singleton;
using Laboratorio3ED1.Models;
using System.Net;

namespace Laboratorio3ED1.Controllers
{
    public class PartidoController : Controller
    {
        readonly DBconnection data = DBconnection.getInstance;

        // GET: Partido
        public ActionResult IndexFecha()
        {
            try
            {
                if (data.Orden == 0)
                {
                    return View(data.AVlFecha.Infijo());
                }
                else if (data.Orden == 1)
                {
                    return View(data.AVlFecha.Prefijo());
                }
                else
                {
                    return View(data.AVlFecha.PostFijo());
                }
            }
            catch
            {
                return View();
            }


        }
        public ActionResult IndexNpartido()
        {
            try
            {
                if (data.Orden == 0)
                {
                    return View(data.AVLNpartido.Infijo());
                }
                else if (data.Orden == 1)
                {
                    return View(data.AVLNpartido.Prefijo());
                }
                else
                {
                    return View(data.AVLNpartido.PostFijo());
                }
            }
            catch
            {
                return View();
            }
        }

        
        // GET: Partido/CreateFecha
        public ActionResult CreateFecha()
        {
            return View();
        }

        // Get: Partido/CreateNpartido
        public ActionResult CreateNpartido()
        {
            return View();
        }
        // POST: Partido/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFecha([Bind(Include = "Fecha,Equipo1_,Equipo2_,Grupo,Estadio, Npartido_")] Partido Partido_)
        {
            if (ModelState.IsValid)
            {
                data.AVlFecha.Insertar(Partido_);
                return RedirectToAction(nameof(IndexFecha));
            }
            return View(Partido_);
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNPartido([Bind(Include = "Fecha,Equipo1_,Equipo2_,Grupo,Estadio, Npartido_")] Partido Partido_)
        {
            if (ModelState.IsValid)
            {
                data.AVLNpartido.Insertar(Partido_);
                return RedirectToAction(nameof(IndexNpartido));
            }
            return View(Partido_);

        }


        // GET: Partido/Delete/5
        public ActionResult DeleteFecha(DateTime fecha_, string equipo1, string equipo2)
        {
            var DelFecha = new Partido(fecha_, equipo1, equipo2, "", "", 0);
            if(DelFecha.Fecha == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var match = data.AVlFecha.Buscar(DelFecha);
            if(match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        // POST: Partido/Delete/5
        [HttpPost,ActionName(nameof(DeleteFecha))]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFechaConfirmed(DateTime fecha_, string equipo1, string equipo2)
        {
            try
            {
                var DelFecha = new Partido(fecha_, equipo1, equipo2, "", "", 0);
                data.AVlFecha.Eliminar(DelFecha);
                return RedirectToAction(nameof(IndexFecha));
            }
#pragma warning disable CC0003 // Your catch should include an Exception
            catch
            {
                return View();
            }
#pragma warning restore CC0003 // Your catch should include an Exception
        }

        public ActionResult DeleteNpartido(int Npartido)
        {
            var PartidoEliminar = new Partido(default(DateTime) , "", " ", " ", " ", Npartido);

            if(PartidoEliminar.Npartido_ == default(int))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var match = data.AVLNpartido.Buscar(PartidoEliminar);
            if(match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }
        [HttpPost, ActionName(nameof(DeleteNpartido))]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteNpartidoConfirmed(int Npartido)
        {
            try
            {
                var DelFecha = new Partido(default(DateTime),"", "", "", "", Npartido);
                data.AVlFecha.Eliminar(DelFecha);
                return RedirectToAction(nameof(IndexFecha));
            }
#pragma warning disable CC0003 // Your catch should include an Exception
            catch
            {
                return View();
            }
#pragma warning restore CC0003 // Your catch should include an Exception
        }
    }
}
