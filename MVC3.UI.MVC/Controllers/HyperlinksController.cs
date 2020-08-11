using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC3.UI.MVC.Models;//Added for access to the DevLinx models
using System.Net.Http;//Added for access to the HttpClient
using System.Net.Http.Headers;//Added for access to MediaTypeWithQualityHeaderValue
using Newtonsoft.Json;

namespace MVC3.UI.MVC.Controllers
{
    public class HyperlinksController : Controller
    {
        // GET: Hyperlinks
        public ActionResult Index()
        {
            #region Get All Links via WebAPI
            List<HyperlinkViewModel> links = new List<HyperlinkViewModel>();

            using(var client = new HttpClient())
            {
                //Configure the client
                client.BaseAddress = new Uri("http://localhost:64650/");//Number in this string might vary depending on the machine and number of ports open
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Getting the response from the GetHyperlinks API
                HttpResponseMessage response = client.GetAsync("api/Hyperlinks/").Result;

                //Deserialization takes data structured from some format (JSON in this case), and rebuilds it as an object (List<HyperLinkViewModel>)
                //JsonConvert is a class that provides methods for converting between .NET types JSON types
                links = JsonConvert.DeserializeObject<List<HyperlinkViewModel>>(response.Content.ReadAsStringAsync().Result);
            }

            #endregion

            return View(links);
        }

        // GET: Hyperlinks/Details/5
        public ActionResult Details(int id)
        {
            #region Get Links via WebAPI
            HyperlinkViewModel links = new HyperlinkViewModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:64650/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("api/Hyperlinks/" + id).Result;

                links = JsonConvert.DeserializeObject<HyperlinkViewModel>(response.Content.ReadAsStringAsync().Result);
            }
            #endregion

            return View(links);
        }

        // GET: Hyperlinks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hyperlinks/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Hyperlinks/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Hyperlinks/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Hyperlinks/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Hyperlinks/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
