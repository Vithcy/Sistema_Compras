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
    public class Solicitud_ArticulosController : Controller
    {
        private ComprasEntities db = new ComprasEntities();

        // GET: Solicitud_Articulos
        public ActionResult Index()
        {
            var solicitud_Articulos = db.Solicitud_Articulos.Include(s => s.Articulos).Include(s => s.Empleados).Include(s => s.Medidas);
            return View(solicitud_Articulos.ToList());
        }

        // GET: Solicitud_Articulos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitud_Articulos solicitud_Articulos = db.Solicitud_Articulos.Find(id);
            if (solicitud_Articulos == null)
            {
                return HttpNotFound();
            }
            return View(solicitud_Articulos);
        }

        // GET: Solicitud_Articulos/Create
        public ActionResult Create()
        {
            ViewBag.Articulo = new SelectList(db.Articulos, "IdArt", "Articulo");
            ViewBag.Empleado = new SelectList(db.Empleados, "IdEmp", "Cedula");
            ViewBag.Unidad_Medida = new SelectList(db.Medidas, "IdMedida", "Unidad_de_Medida");
            return View();
        }

        // POST: Solicitud_Articulos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSol,Empleado,Fecha_Solicitud,Articulo,Cantidad,Unidad_Medida,Activo")] Solicitud_Articulos solicitud_Articulos)
        {
            if (ModelState.IsValid)
            {
                db.Solicitud_Articulos.Add(solicitud_Articulos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Articulo = new SelectList(db.Articulos, "IdArt", "Articulo", solicitud_Articulos.Articulo);
            ViewBag.Empleado = new SelectList(db.Empleados, "IdEmp", "Cedula", solicitud_Articulos.Empleado);
            ViewBag.Unidad_Medida = new SelectList(db.Medidas, "IdMedida", "Unidad_de_Medida", solicitud_Articulos.Unidad_Medida);
            return View(solicitud_Articulos);
        }

        // GET: Solicitud_Articulos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitud_Articulos solicitud_Articulos = db.Solicitud_Articulos.Find(id);
            if (solicitud_Articulos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Articulo = new SelectList(db.Articulos, "IdArt", "Articulo", solicitud_Articulos.Articulo);
            ViewBag.Empleado = new SelectList(db.Empleados, "IdEmp", "Cedula", solicitud_Articulos.Empleado);
            ViewBag.Unidad_Medida = new SelectList(db.Medidas, "IdMedida", "Unidad_de_Medida", solicitud_Articulos.Unidad_Medida);
            return View(solicitud_Articulos);
        }

        // POST: Solicitud_Articulos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSol,Empleado,Fecha_Solicitud,Articulo,Cantidad,Unidad_Medida,Activo")] Solicitud_Articulos solicitud_Articulos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitud_Articulos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Articulo = new SelectList(db.Articulos, "IdArt", "Articulo", solicitud_Articulos.Articulo);
            ViewBag.Empleado = new SelectList(db.Empleados, "IdEmp", "Cedula", solicitud_Articulos.Empleado);
            ViewBag.Unidad_Medida = new SelectList(db.Medidas, "IdMedida", "Unidad_de_Medida", solicitud_Articulos.Unidad_Medida);
            return View(solicitud_Articulos);
        }

        // GET: Solicitud_Articulos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitud_Articulos solicitud_Articulos = db.Solicitud_Articulos.Find(id);
            if (solicitud_Articulos == null)
            {
                return HttpNotFound();
            }
            return View(solicitud_Articulos);
        }

        // POST: Solicitud_Articulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Solicitud_Articulos solicitud_Articulos = db.Solicitud_Articulos.Find(id);
            db.Solicitud_Articulos.Remove(solicitud_Articulos);
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
