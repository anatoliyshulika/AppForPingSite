using System;
using System.Data.Entity;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web.Mvc;

namespace Ping_Site
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public int Post(string path)
        {
            try
            {
                using (Ping p = new Ping())
                {
                    return ((int)p.Send(path).RoundtripTime);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
        [HttpPost]
        public string PostSave(string path, string maximum, string minimum, string mid)
        {
            using (PingContext pingDb = new PingContext())
            {
                bool existe = false;
                int id = 0;
                foreach (DataSite d in pingDb.DataSites.ToList())
                {
                    if (path == d.Path)
                    {
                        existe = true;
                        id = d.Id;
                    }
                }
                if (existe)
                {
                    DataSite ds = pingDb.DataSites.Find(id);
                    ds.ResponseTimeMax = Convert.ToInt32(maximum);
                    ds.ResponseTimeMin = Convert.ToInt32(minimum);
                    ds.MidlResponseTime = Convert.ToInt32(mid);
                    try
                    {
                        pingDb.Entry(ds).State = EntityState.Modified;
                        pingDb.SaveChanges();
                        return "The data saved successfully.";
                    }
                    catch (Exception)
                    {
                        return "The data could not be saved, please try again later.";
                    }
                }
                else
                {
                    DataSite dataSite = new DataSite();
                    dataSite.Path = path;
                    dataSite.ResponseTimeMax = Convert.ToInt32(maximum);
                    dataSite.ResponseTimeMin = Convert.ToInt32(minimum);
                    dataSite.MidlResponseTime = Convert.ToInt32(mid);
                    try
                    {
                        pingDb.Entry(dataSite).State = EntityState.Added;
                        pingDb.SaveChanges();
                        return "The data saved successfully.";
                    }
                    catch (Exception)
                    {
                        return "The data could not be saved, please try again later.";
                    }
                }
            }
        }
        [HttpGet]
        public string ClearDb()
        {
            using (PingContext pingDb = new PingContext())
            {
                try
                {
                    foreach (DataSite d in pingDb.DataSites.ToList())
                    {
                        DataSite ds = pingDb.DataSites.Find(d.Id);
                        pingDb.DataSites.Remove(ds);
                        pingDb.SaveChanges();
                    }
                    return "The data deleted successfully.";
                }
                catch (Exception)
                {
                    return "The data could not be deleted, please try again later.";
                }
            }
        }
    }
}