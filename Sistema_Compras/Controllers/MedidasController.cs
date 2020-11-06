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
    public class MedidasController : Controller
    {
        private ComprasEntities db = new ComprasEntities();

        // GET: Medidas
        [Authorize(Roles = "Administrador, Empleado")]
        public ActionResult Index(string Criterio = null)
        {
            var empleados = db.Medidas.Include(e => e.Unidad_de_Medida);
            return View(db.Medidas.Where(p => Criterio == null ||
            p.Unidad_de_Medida.StartsWith(Criterio) ||
            p.Activo.ToString().StartsWith(Criterio)).ToList());
        }


        // GET: Medidas/Details/5
        [Authorize(Roles = "Administrador, Empleado")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medidas medidas = db.Medidas.Find(id);
            if (medidas == null)
            {
                return HttpNotFound();
            }
            return View(medidas);
        }

        // GET: Medidas/Create
        [Authorize(Roles = "Administrador, Empleado")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Medidas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMedida,Unidad_de_Medida,Activo")] Medidas medidas)
        {
            if (ModelState.IsValid)
            {
                db.Medidas.Add(medidas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(medidas);
        }

        // GET: Medidas/Edit/5
        [Authorize(Roles = "Administrador, Empleado")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medidas medidas = db.Medidas.Find(id);
            if (medidas == null)
            {
                return HttpNotFound();
            }
            return View(medidas);
        }

        // POST: Medidas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMedida,Unidad_de_Medida,Activo")] Medidas medidas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medidas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medidas);
        }

        // GET: Medidas/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medidas medidas = db.Medidas.Find(id);
            if (medidas == null)
            {
                return HttpNotFound();
            }
            return View(medidas);
        }

        // POST: Medidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medidas medidas = db.Medidas.Find(id);
            db.Medidas.Remove(medidas);
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
