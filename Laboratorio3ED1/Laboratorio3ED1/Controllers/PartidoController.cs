﻿using System;
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
        public DBconnection data = DBconnection.getInstance;

        // GET: Partido
        public ActionResult IndexFecha()
        {



            return View(data.AVlFecha.Infijo());
        }
        public ActionResult IndexNpartido()
        {

            return View(data.AVLNpartido.Infijo());

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
        public ActionResult DeleteFecha(long id, string equipo1, string equipo2)

        {
            var fecha_ = DateTime.FromBinary(id);
            var DelFecha = new Partido(fecha_, equipo1, equipo2, "", "", 0);
            if (DelFecha.Fecha == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var match = data.AVlFecha.Buscar(DelFecha);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match.value);
        }

        // POST: Partido/Delete/5
        [HttpPost, ActionName(nameof(DeleteFecha))]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFechaConfirmed(long id, string equipo1, string equipo2)
        {
            try
            {
                var fecha_ = DateTime.FromBinary(id);
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

        public ActionResult DeleteNpartido(int id)
        {
            var PartidoEliminar = new Partido(default(DateTime), "", " ", " ", " ", id);

            if (PartidoEliminar.Npartido_ == default(int))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var match = data.AVLNpartido.Buscar(PartidoEliminar);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match.value);
        }
        [HttpPost, ActionName(nameof(DeleteNpartido))]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteNpartidoConfirmed(int id)
        {
            try
            {
                var DelFecha = new Partido(default(DateTime), "", "", "", "", id);
                data.AVLNpartido.Eliminar(DelFecha);
                return RedirectToAction(nameof(IndexNpartido));
            }
#pragma warning disable CC0003 // Your catch should include an Exception
            catch
            {
                return View();
            }
#pragma warning restore CC0003 // Your catch should include an Exception
        }

        public ActionResult SearchF()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchF(Partido Match)
        {
            data.Fechas.Clear();
            try
            {
                
                data.Fechas = data.AVlFecha.Infijo().FindAll(x => x.Fecha == Match.Fecha);
                return RedirectToAction(nameof(SearchResult));
            }
#pragma warning disable CC0003 // Your catch should include an Exception
            catch
            {
                TempData["msg"] = "<script>alert('DATO NO ENCONTRADO');</script>";
                return View();
            }
#pragma warning restore CC0003 // Your catch should include an Exception
        }

        public ActionResult SearchResult()
        {
            return View(data.Fechas);
        }
        public ActionResult SearchN()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult SearchN(Partido Match)
        {
            try
            {
                var partidob = data.AVLNpartido.Buscar(Match);
                return RedirectToAction(nameof(NSearchResult), new { id = partidob.value });
            }
#pragma warning disable CC0003 // Your catch should include an Exception
            catch
            {
                TempData["msg"] = "<script>alert('DATO NO ENCONTRADO');</script>";
                return View();
            }
#pragma warning restore CC0003 // Your catch should include an Exception
        }
        public ActionResult NSearchResult(Partido id)
        {
            return View(id);
        }
    }
}
