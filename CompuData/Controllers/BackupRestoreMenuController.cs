using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class BackupRestoreMenuController : Controller
    {
        // GET: BackupRestoreMenu
        public ActionResult Index()
        {
            return View();
        }

        private CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
        // GET: Backup
        public ActionResult DoBackup()
        {
            string dbPath = Server.MapPath("~/App_Data/DBBackup.bak"); 
            using (var db = new CodeFirst.CodeFirst()) 
            {
                var cmd = String.Format("BACKUP DATABASE {0} TO DISK='{1}' WITH FORMAT, MEDIANAME='DbBackups', MEDIADESCRIPTION='Media set for {0} database';"
                    , "CompudataSQL", dbPath);
                db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, cmd); 
            }
            return new FilePathResult(dbPath, "application/octet-stream"); 
        }

        public ActionResult DoRestore()
        {
            string dbPath = Server.MapPath("~/App_Data/DBBackup.bak"); 
            using (var db = new CodeFirst.CodeFirst())
            {

             /*   var cmd = String.Format("restore DATABASE {0} from DISK='{1}';"
                    , "CompudataSQL", dbPath); // ?
                db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, cmd);*/
                //---------------------------------------------------------

                //var cmd = String.Format("restore DATABASE {0} from DISK='{1}';"
                //    , "FarmDb", dbPath);
                var cmd = String.Format("USE master restore DATABASE CompudataSQL from DISK='{0}' WITH REPLACE;", dbPath);
                //var cmd1 = String.Format("alter database FarmDb set  with rollback immediate");
                //var cmd2 = String.Format("USE master alter database FarmDb set online");
                var cmd3 = String.Format("ALTER DATABASE CompudataSQL SET SINGLE_USER WITH ROLLBACK IMMEDIATE ");
                var cmd4 = String.Format("ALTER DATABASE CompudataSQL SET MULTI_USER");

                db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, cmd3);
                db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, cmd);
                db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, cmd4);

            }
            return RedirectToAction("Index", "MainMenu");
            // return View();

        }
    }
}