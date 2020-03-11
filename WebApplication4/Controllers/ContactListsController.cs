using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
using Newtonsoft.Json;
using WebApplication4;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Controllers
{
    public class ContactListsController : Controller
    {

        private Modeldb db = new Modeldb();

        // GET: ContactLists
        public ActionResult Index()
        {
            return View(db.ContactLists.ToList());
        }

        // GET: ContactLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactList contactList = db.ContactLists.Find(id);
            if (contactList == null)
            {
                return HttpNotFound();
            }
            return View(contactList);
        }

        // GET: ContactLists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactLists/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*[Bind(Include = "Id,LastName,FirstName,MiddleName,Organization,Position")]*/ ContactList contactList, IEnumerable<ContactInfo> contactInfo)
        {
            SaveDataFromContactInfo saveDataFromContactInfo = new SaveDataFromContactInfo(contactList, db);
            var validAll = saveDataFromContactInfo.AllValid(Request.Params["JsonFile"].Replace("item.", ""));
            if (!string.IsNullOrWhiteSpace(validAll))
            {
                ModelState.AddModelError("", validAll);
                contactList = saveDataFromContactInfo.GetContactList(Request.Params["JsonFile"].Replace("item.", ""));
                return View(contactList);
            }
            if (ModelState.IsValid)
            {
                db.ContactLists.Add(contactList);
                db.SaveChanges();
                saveDataFromContactInfo.SaveData(Request.Params["JsonFile"].Replace("item.", ""));
                return RedirectToAction("Index");
            }
            contactList = saveDataFromContactInfo.GetContactList(Request.Params["JsonFile"].Replace("item.", ""));
            return View(contactList);
        }

        // GET: ContactLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactList contactList = db.ContactLists.Find(id);
            if (contactList == null)
            {
                return HttpNotFound();
            }
            //ViewBag.ContactInfoes = contactList.ContactInfoes;
            /*var json= new ContentResult
            {
                Content = JsonConvert.SerializeObject(contactList, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                ContentType = "application/json"
            };*/
            //var json1 = JsonConvert.SerializeObject(contactList, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return View(contactList);
        }

        // POST: ContactLists/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(/*[Bind(Include = "Id,LastName,FirstName,MiddleName,Organization,Position, ContactInfoes")]*/ ContactList contactList, /*[Bind(Include = "Id,ContactList,ContactListId,Phone,Email,Skype,Other ")]*/IEnumerable<ContactInfo> contactInfo)
        {
            SaveDataFromContactInfo saveDataFromContactInfo = new SaveDataFromContactInfo(contactList, db);
            var validAll = saveDataFromContactInfo.AllValid(Request.Params["JsonFile"].Replace("item.", ""));
            if (!string.IsNullOrWhiteSpace(validAll))
            {
                ModelState.AddModelError("", validAll);
                contactList = saveDataFromContactInfo.GetContactList(Request.Params["JsonFile"].Replace("item.", ""));
                return View(contactList);
            }
            if (ModelState.IsValid)
            {
                db.Entry(contactList).State = EntityState.Modified;
                db.SaveChanges();
                saveDataFromContactInfo.SaveData(Request.Params["JsonFile"].Replace("item.", ""));
                return RedirectToAction("Index");
            }
            contactList = saveDataFromContactInfo.GetContactList(Request.Params["JsonFile"].Replace("item.", ""));
            return View(contactList);
        }

        // GET: ContactLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactList contactList = db.ContactLists.Find(id);
            if (contactList == null)
            {
                return HttpNotFound();
            }
            return View(contactList);
        }

        // POST: ContactLists/Delete/5
        [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactList contactList = db.ContactLists.Find(id);
            var contactInfo = db.ContactInfoes.Where(c => c.ContactListId == id).ToList();
            foreach (var item in contactInfo)
            {
                db.ContactInfoes.Remove(item);
            }
            db.ContactLists.Remove(contactList);
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


        public ActionResult Search(string search)
        {
            if (!string.IsNullOrWhiteSpace(search))
            {
                var list = db.ContactLists.Where(i => i.FirstName.Contains(search) || i.LastName.Contains(search) || i.MiddleName.Contains(search)
                             || i.Organization.Contains(search) || i.Position.Contains(search)).ToList();
                var contactinfo = db.ContactInfoes.Where(i => i.Phone.Contains(search) || i.Skype.Contains(search)
                || i.Other.Contains(search) || i.Email.Contains(search)).Select(i => i.ContactList).ToList();
                foreach (var item in contactinfo)
                {
                    list.Add(item);
                }
                return PartialView("List", list.Distinct());
            }
            else return PartialView("List", db.ContactLists.ToList());

        }

    }
}
