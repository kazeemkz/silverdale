using SilverDaleSchools.DAL;
using SilverDaleSchools.Model;
using SilverDalesSchools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.Security;
using SilverDaleSchools.Models;

namespace SilverDaleSchools.Controllers
{
    [Authorize]
    [DynamicAuthorize]
    public class StaffController : Controller
    {
        UnitOfWork work = new UnitOfWork();
        //
        // GET: /Staff/
        //   public ActionResult Index()
        public ViewResult Index(string sortOrder, string currentFilter, string ApprovedString, string searchString, string SexString, string role, string StudentIDString, int? page)
        {
            List<SelectListItem> theItem3 = new List<SelectListItem>();
            string[] theRoles = Roles.GetAllRoles();
            theItem3.Add(new SelectListItem() { Text = "None", Value = "" });
            foreach (var theRole in theRoles)
            {
                theItem3.Add(new SelectListItem() { Text = theRole, Value = theRole });
            }
            ViewData["Role"] = theItem3;



            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date desc" : "Date";
            ViewBag.ClassSortParm = sortOrder == "class" ? "class desc" : "class";
            ViewBag.GenderSortParm = sortOrder == "gender" ? "gender desc" : "gender";

            if (Request.HttpMethod == "GET")
            {
                // searchString = currentFilter;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter = searchString;

            var students = from s in work.StaffRepository.Get()
                           select s;

          students =  students.Where(a => a.UserID != "5000001");
          students = students.Where(a => a.UserID != "5000002");
            //   students = students.Where(s => s.UserID != 5000001);
           //  students = students.Where(s => s.UserID != 5000052);
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.ToUpper().Contains(searchString.ToUpper())
                                       || s.FirstName.ToUpper().Contains(searchString.ToUpper()) || s.Middle.ToUpper().Contains(searchString.ToUpper()));
            }

            if (!String.IsNullOrEmpty(SexString))
            {
                students = students.Where(s => s.Sex.Equals(SexString));
            }

            if (!String.IsNullOrEmpty(role))
            {
                students = students.Where(s => s.Role.Equals(role));
            }

            if (!String.IsNullOrEmpty(StudentIDString))
            {
              //  int theID = Convert.ToInt32(StudentIDString);
                students = students.Where(s => s.UserID == StudentIDString);
            }

            if (!String.IsNullOrEmpty(SexString))
            {
                students = students.Where(s => s.Sex.Equals(SexString));
            }

            //if (!String.IsNullOrEmpty(ApprovedString))
            //{
            //    bool theValue = Convert.ToBoolean(ApprovedString);
            //    students = students.Where(s => s.IsApproved == theValue);
            //}
            switch (sortOrder)
            {
                case "Name desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "Date desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;

                //case "class":
                //    students = students.OrderBy(s => s.ClassTeacherOf);
                //    break;
                //case "class desc":
                //    students = students.OrderByDescending(s => s.ClassTeacherOf);
                //    break;

                case "gender":
                    students = students.OrderBy(s => s.Sex);
                    break;
                case "gender desc":
                    students = students.OrderByDescending(s => s.Sex);
                    break;
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 40;
            int pageNumber = (page ?? 1);
            ViewBag.Count = students.Count();

            //if (!(User.IsInRole("SuperAdmin")))
            //{

            //    int UserName = Convert.ToInt32(User.Identity.Name);
            //    List<Staff> theStaff = work.StaffRepository.Get(a => a.UserID == UserName).ToList();
            //    Staff theStaf = theStaff[0];
            //   // students = students.Where(a => a.UserID == UserName && a.IsApproved == true);
            //    ViewBag.Count = students.Count();
            //    return View(students.ToPagedList(pageNumber, pageSize));

            //}
            // else
            // {
            return View(students.ToPagedList(pageNumber, pageSize));
            //  }
        }

        //
        // GET: /Staff/Details/5
        public ActionResult Details(int id)
        {
         Staff theStaff =  work.StaffRepository.GetByID(id);
         return View(theStaff);
        }

        //
        // GET: /Staff/Create
        public ActionResult Create()
        {
            List<SelectListItem> theItem3 = new List<SelectListItem>();
            string[] theRoles = Roles.GetAllRoles();

            theItem3.Add(new SelectListItem() { Text = "None", Value = "" });
            foreach (var role in theRoles)
            {

                theItem3.Add(new SelectListItem() { Text = role, Value = role });

            }
            ViewData["Role"] = theItem3;
            return View();
        }

        //
        // POST: /Staff/Create
        [HttpPost]
        public ActionResult Create(Staff model)
        {
            try
            {
                model.EnrollmentDate = DateTime.Now;
                if (ModelState.IsValid)
                {
                    if (Membership.GetUser(model.UserID.ToString()) == null)
                    {
                        Membership.CreateUser(model.UserID.ToString(), PaddPassword.Padd(model.LastName), model.Email);
                        Roles.AddUserToRole(model.UserID.ToString(), model.Role);
                        work.StaffRepository.Insert(model);
                        work.Save();
                    }
                    else
                    {

                        ModelState.AddModelError("", "Staff ID Already Exist, Please user another ID!");
                        List<SelectListItem> theItem3 = new List<SelectListItem>();
                        string[] theRoles = Roles.GetAllRoles();

                        theItem3.Add(new SelectListItem() { Text = "None", Value = "" });
                        foreach (var role in theRoles)
                        {

                            theItem3.Add(new SelectListItem() { Text = role, Value = role });

                        }
                        ViewData["Role"] = theItem3;
                        return View();
                    }
                }
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Staff/Edit/5
        public ActionResult Edit(int id)
        {
            List<SelectListItem> theItem3 = new List<SelectListItem>();
            string[] theRoles = Roles.GetAllRoles();
           // theItem3.Add(new SelectListItem() { Text = "None", Value = "" });
            foreach (var theRole in theRoles)
            {
                theItem3.Add(new SelectListItem() { Text = theRole, Value = theRole });
            }
            ViewData["Role"] = theItem3;
            Staff theStaff = work.StaffRepository.GetByID(id);
            return View(theStaff);
        }

        //
        // POST: /Staff/Edit/5
        [HttpPost]
        public ActionResult Edit(Staff model)
        {
            try
            {
                // TODO: Add update logic here
                UnitOfWork work2 = new UnitOfWork();
                Staff staff = work2.StaffRepository.GetByID(model.StaffID);
                string[] RoleList = Roles.GetAllRoles();
                //  Roles.RemoveUserFromRoles(model.UserID.ToString(), RoleList);
                foreach (var role in RoleList)
                {
                    if (Roles.IsUserInRole(model.UserID.ToString(), role))
                    {
                        Roles.RemoveUserFromRole(model.UserID.ToString(), role);
                    }
                }
                Roles.AddUserToRole(model.UserID.ToString(), model.Role);
               // work.StaffRepository.Update(model);

                SilverDaleSchools.Models.Tweaker.AdjustTimer(model.UserID.ToString());
                TryUpdateModel(model);
                if(ModelState.IsValid)
                {
                    work.StaffRepository.Update(model);
                    work.Save();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Staff/Delete/5
        public ActionResult Delete(int id)
        {
            Staff theStaff = work.StaffRepository.GetByID(id);
            return View(theStaff);
        }

        //
        // POST: /Staff/Delete/5
        [HttpPost]
        public ActionResult Delete(Staff model)
        {
            try
            {
                // TODO: Add delete logic here
                Staff theStaff = work.StaffRepository.GetByID(model.StaffID);
                work.StaffRepository.Delete(theStaff);
                work.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
