using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sistema_Compras;

namespace Sistema_Compras.Controllers
{
    public class MarcasController : Controller
    {
        private ComprasEntities db = new ComprasEntities();

        // GET: Marcas
        [Authorize(Roles = "Administrador, Empleado, Consulta")]
        public ActionResult Index(string Criterio = null)
        {
            var marcas = db.Marcas.Include(e => e.Articulos);
            return View(db.Marcas.Where(p => Criterio == null ||
            p.Nombre.StartsWith(Criterio) ||
            p.Activo.ToString().StartsWith(Criterio)).ToList());
        }


        // GET: Marcas/Details/5
        [Authorize(Roles = "Administrador, Empleado, Consulta")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marcas marcas = db.Marcas.Find(id);
            if (marcas == null)
            {
                return HttpNotFound();
            }
            return View(marcas);
        }

        // GET: Marcas/Create
        [Authorize(Roles = "Administrador, Empleado")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Marcas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMarca,Nombre,Activo")] Marcas marcas)
        {
            if (ModelState.IsValid)
            {
                db.Marcas.Add(marcas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(marcas);
        }

        // GET: Marcas/Edit/5
        [Authorize(Roles = "Administrador, Empleado")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marcas marcas = db.Marcas.Find(id);
            if (marcas == null)
            {
                return HttpNotFound();
            }
            return View(marcas);
        }

        // POST: Marcas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMarca,Nombre,Activo")] Marcas marcas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(marcas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(marcas);
        }

        // GET: Marcas/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marcas marcas = db.Marcas.Find(id);
            if (marcas == null)
            {
                return HttpNotFound();
            }
            return View(marcas);
        }

        // POST: Marcas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Marcas marcas = db.Marcas.Find(id);
            db.Marcas.Remove(marcas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
