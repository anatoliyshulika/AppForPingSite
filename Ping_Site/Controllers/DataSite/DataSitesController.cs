using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Ping_Site
{
    public class DataSitesController : Controller
    {
        public List<DataSite> listDataSite;
        public List<DataSite> topListDataSite;
        // GET: DataSites
        [HttpGet]
        public ActionResult DataSitesView()
        {
            using (PingContext pingDb = new PingContext())
            {
                listDataSite = new List<DataSite>();
                topListDataSite = new List<DataSite>();
                listDataSite = pingDb.DataSites.ToList();
                var sortListDataSite = listDataSite.OrderByDescending(d => d.MidlResponseTime);
                int i = 0;
                foreach (DataSite d in sortListDataSite)
                {
                    if (i < 10)
                    {
                        topListDataSite.Add(d);
                        i++;
                    }
                    else
                    {
                        break;
                    }
                }
                ViewBag.topList = topListDataSite;
                return View();
            }
        }
    }
}