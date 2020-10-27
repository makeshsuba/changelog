using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChangeLogMVC.Models;

namespace ChangeLogMVC.Controllers
{
    public class LogListModelsController : Controller
    {
        private ChangelogEntities3 db = new ChangelogEntities3();

        // GET: LogListModels
        public async Task<ActionResult> Index()
        {
            return View(await db.Logs.Select(a => a)
                .Join(db.LogTypes, logs => logs.LogTypeId, lt => lt.LogTypeId,
                (logs, lt) => new LogListModel
                {
                    Id = logs.Id,
                    LogTitle = logs.LogTitle,
                    LogDescription = logs.LogDescription,
                    LogType = lt.LogType,
                    CreatedDate = logs.CreatedDate,
                    UpdatedDate = logs.UpdatedDate,
                    UserName = logs.UserName
                })
                .OrderByDescending(a => a.Id).ToListAsync());
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
