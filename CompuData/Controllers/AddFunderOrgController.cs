﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddFunderOrgController : Controller
    {
        // GET: AddFunderOrg
        public ActionResult Index()
        {
            var db = new CodeFirst.CodeFirst();
            var FunderOrg = new Models.Funder_Org();
            FunderOrg.TypeID = db.Funder_Type.ToList();
            FunderOrg.ProjectName = db.Projects.ToList();
            return View(FunderOrg);
        }

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "")]Models.Funder_Org model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                if (db.Funder_Org.Count() > 0)
                {
                    var item = db.Funder_Org.OrderByDescending(a => a.FunderOrgID).FirstOrDefault();

                    db.Funder_Org.Add(new CodeFirst.Funder_Org
                    {
                        FunderOrgID = item.FunderOrgID + 1,
                        OrgName = model.OrgName,
                        ContactNumber = model.ContactNumber,
                        EmailAddress = model.EmailAddress,
                        Bank = model.Bank,
                        AccountNumber = model.AccountNumber,
                        BranchCode = model.BranchCode,
                        StreetAddress = model.StreetAddress,
                        City = model.City,
                        AreaCode = model.AreaCode,
                        Thanked = model.Thanked,
                        TypeID = model.TypeID,
                        ProjectID = model.ProjectID,
                    //ProjectID = item.ProjectID + 1,
                    //ProjectName = model.ProjectName,
                    //StartDate = DateTime.Parse(model.StartDate.ToString("yyyy-MM-dd")),
                    //ExpectedFinishDate = DateTime.Parse(model.ExpectedFinishDate.ToString("yyyy-MM-dd")),
                    //Finished = false,
                    //ProjectDescription = model.ProjectDescription,
                    //TypeID = model.TypeID,
                    //UserID = model.UserID,
                });
                }
                else
                {
                    db.Funder_Org.Add(new CodeFirst.Funder_Org
                    {
                        FunderOrgID = 1,
                        OrgName = model.OrgName,
                        ContactNumber = model.ContactNumber,
                        EmailAddress = model.EmailAddress,
                        Bank = model.Bank,
                        AccountNumber = model.AccountNumber,
                        BranchCode = model.BranchCode,
                        StreetAddress = model.StreetAddress,
                        City = model.City,
                        AreaCode = model.AreaCode,
                        Thanked = model.Thanked,
                        TypeID = model.TypeID,
                        ProjectID = model.ProjectID,
                    });
                }

                db.SaveChanges();
                model.JavaScriptToRun = "mySuccess()";
                TempData["model"] = model;
                return RedirectToAction("Index", "FunderOrg");

            }

            model.TypeID = db.Funder_Type.ToList();
            model.ProjectName = db.Projects.ToList();
            return View("Index", model);
        }
    }
}