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
    public class Orden_CompraController : Controller
    {
        private ComprasEntities db = new ComprasEntities();

        // GET: Orden_Compra
        public ActionResult Index()
        {
            var orden_Compra = db.Orden_Compra.Include(o => o.Articulos).Include(o => o.Marcas).Include(o => o.Medidas);
            return View(orden_Compra.ToList());
        }

        // GET: Orden_Compra/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orden_Compra orden_Compra = db.Orden_Compra.Find(id);
            if (orden_Compra == null)
            {
                return HttpNotFound();
            }
            return View(orden_Compra);
        }

        // GET: Orden_Compra/Create
        public ActionResult Create()
        {
            ViewBag.Articulo = new SelectList(db.Articulos, "IdArt", "Articulo");
            ViewBag.Marca = new SelectList(db.Marcas, "IdMarca", "Nombre");
            ViewBag.Unidad_Medida = new SelectList(db.Medidas, "IdMedida", "Unidad_de_Medida");
            return View();
        }

        // POST: Orden_Compra/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdOrden,No_Orden,Fecha_Orden,Activo,Articulo,Cantidad,Unidad_Medida,Marca,Costo_Unitario")] Orden_Compra orden_Compra)
        {
            if (ModelState.IsValid)
            {
                db.Orden_Compra.Add(orden_Compra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Articulo = new SelectList(db.Articulos, "IdArt", "Articulo", orden_Compra.Articulo);
            ViewBag.Marca = new SelectList(db.Marcas, "IdMarca", "Nombre", orden_Compra.Marca);
            ViewBag.Unidad_Medida = new SelectList(db.Medidas, "IdMedida", "Unidad_de_Medida", orden_Compra.Unidad_Medida);
            return View(orden_Compra);
        }

        // GET: Orden_Compra/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orden_Compra orden_Compra = db.Orden_Compra.Find(id);
            if (orden_Compra == null)
            {
                return HttpNotFound();
            }
            ViewBag.Articulo = new SelectList(db.Articulos, "IdArt", "Articulo", orden_Compra.Articulo);
            ViewBag.Marca = new SelectList(db.Marcas, "IdMarca", "Nombre", orden_Compra.Marca);
            ViewBag.Unidad_Medida = new SelectList(db.Medidas, "IdMedida", "Unidad_de_Medida", orden_Compra.Unidad_Medida);
            return View(orden_Compra);
        }

        // POST: Orden_Compra/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdOrden,No_Orden,Fecha_Orden,Activo,Articulo,Cantidad,Unidad_Medida,Marca,Costo_Unitario")] Orden_Compra orden_Compra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orden_Compra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Articulo = new SelectList(db.Articulos, "IdArt", "Articulo", orden_Compra.Articulo);
            ViewBag.Marca = new SelectList(db.Marcas, "IdMarca", "Nombre", orden_Compra.Marca);
            ViewBag.Unidad_Medida = new SelectList(db.Medidas, "IdMedida", "Unidad_de_Medida", orden_Compra.Unidad_Medida);
            return View(orden_Compra);
        }

        // GET: Orden_Compra/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orden_Compra orden_Compra = db.Orden_Compra.Find(id);
            if (orden_Compra == null)
            {
                return HttpNotFound();
            }
            return View(orden_Compra);
        }

        // POST: Orden_Compra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orden_Compra orden_Compra = db.Orden_Compra.Find(id);
            db.Orden_Compra.Remove(orden_Compra);
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
