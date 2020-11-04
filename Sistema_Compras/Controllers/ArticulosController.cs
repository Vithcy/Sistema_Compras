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
    public class ArticulosController : Controller
    {
        private ComprasEntities db = new ComprasEntities();

        // GET: Articulos
        // BARRA DE BUSQUEDA DE ARTICULOS
        [Authorize(Roles = "Administrador, Empleado, Consulta")]
        public ActionResult Index(string Criterio = null)
        {
            var articulos = db.Articulos.Include(a => a.Marcas).Include(a => a.Medidas);
            return View(db.Articulos.Where(p => Criterio == null ||
            p.Articulo.StartsWith(Criterio) ||
            p.Marca.ToString().StartsWith(Criterio) ||
            p.Unidad_Medida.ToString().StartsWith(Criterio) ||
            p.Existencia.ToString().StartsWith(Criterio) ||
            p.Activo.ToString().StartsWith(Criterio)).ToList());
        }

        // GET: Articulos/Details/5
        [Authorize(Roles = "Administrador, Empleado, Consulta")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulos articulos = db.Articulos.Find(id);
            if (articulos == null)
            {
                return HttpNotFound();
            }
            return View(articulos);
        }

        // GET: Articulos/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            ViewBag.Marca = new SelectList(db.Marcas, "IdMarca", "Nombre");
            ViewBag.Unidad_Medida = new SelectList(db.Medidas, "IdMedida", "Unidad_de_Medida");
            return View();
        }

        // POST: Articulos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdArt,Articulo,Marca,Unidad_Medida,Existencia,Activo")] Articulos articulos)
        {
            if (ModelState.IsValid)
            {
                db.Articulos.Add(articulos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Marca = new SelectList(db.Marcas, "IdMarca", "Nombre", articulos.Marca);
            ViewBag.Unidad_Medida = new SelectList(db.Medidas, "IdMedida", "Unidad_de_Medida", articulos.Unidad_Medida);
            return View(articulos);
        }

        // GET: Articulos/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulos articulos = db.Articulos.Find(id);
            if (articulos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Marca = new SelectList(db.Marcas, "IdMarca", "Nombre", articulos.Marca);
            ViewBag.Unidad_Medida = new SelectList(db.Medidas, "IdMedida", "Unidad_de_Medida", articulos.Unidad_Medida);
            return View(articulos);
        }

        // POST: Articulos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdArt,Articulo,Marca,Unidad_Medida,Existencia,Activo")] Articulos articulos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articulos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Marca = new SelectList(db.Marcas, "IdMarca", "Nombre", articulos.Marca);
            ViewBag.Unidad_Medida = new SelectList(db.Medidas, "IdMedida", "Unidad_de_Medida", articulos.Unidad_Medida);
            return View(articulos);
        }

        // GET: Articulos/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulos articulos = db.Articulos.Find(id);
            if (articulos == null)
            {
                return HttpNotFound();
            }
            return View(articulos);
        }

        // POST: Articulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Articulos articulos = db.Articulos.Find(id);
            db.Articulos.Remove(articulos);
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
