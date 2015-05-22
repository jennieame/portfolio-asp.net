using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Portfolio_uppgift.Models;
using System.IO;


namespace Portfolio_uppgift.Controllers
{
    public class ProjectsController : Controller
    {
        private ProjectDBContext db = new ProjectDBContext();

        // GET: Projects
        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.Link = project.Link.ToString();

            return View(project);
        }

        // GET: Projects/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,Title,About,Image,Link")] Project project, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                var path = String.Empty;

                if (image != null && image.ContentLength > 0)
                {
                    string extension = System.IO.Path.GetExtension(image.FileName);

                    if (extension == ".jpg" || extension == ".png" || extension == ".gif")
                    {
                        var fileName = Path.GetFileName(image.FileName);
                        var filePath = Path.Combine(Server.MapPath("/Content/Images"), fileName);

                        try
                        {
                            image.SaveAs(filePath);
                            path = String.Format("/Content/Images/{0}", fileName);
                        }
                        catch (Exception e)
                        { }

                        project.Image = path;
                        db.Projects.Add(project);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Message = "Only .jpg, .png or .gif files allowed!";
                    }
                }
                else
                {
                    db.Projects.Add(project);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(project);
        }

        // GET: Projects/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,Title,About,Image,Link")] Project project, HttpPostedFileBase imageUpload)
        {
            if (ModelState.IsValid)
            {
                var path = String.Empty;

                if (imageUpload != null && imageUpload.ContentLength > 0)
                {
                    string extension = System.IO.Path.GetExtension(imageUpload.FileName);

                    if (extension == ".jpg" || extension == ".png" || extension == ".gif")
                    {
                        var fileName = Path.GetFileName(imageUpload.FileName);
                        var filePath = Path.Combine(Server.MapPath("/Content/Images"), fileName);

                        try
                        {
                            imageUpload.SaveAs(filePath);
                            path = String.Format("/Content/Images/{0}", fileName);

                            db.Entry(project).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        catch (Exception e)
                        { }
                    }
                    else
                    {
                        ViewBag.Message = "Only .jpg, .png or .gif files allowed!";
                    }
                }
                else 
                { 
                if(!String.IsNullOrEmpty(path))
                    project.Image = path;

                    db.Entry(project).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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
