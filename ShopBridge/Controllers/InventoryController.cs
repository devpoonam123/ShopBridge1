using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ShopBridge.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Inventory
        public ActionResult Index()
        {
            IEnumerable<Inventory> inventories = null;
            try
            {

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:6068/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("api/Inventory/GetAll").Result;
                inventories = response.Content.ReadAsAsync<IList<Inventory>>().Result;
            }
            catch(Exception ex)
            {

            }
            return View(inventories);

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveImageInFolder()
        {
            string filename = string.Empty;

            try
            {
                if (Request.Files.Count != 0)
                {

                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];

                        filename = Path.GetFileName(file.FileName);

                        var path = Path.Combine(Server.MapPath("~/Images/"), filename);
                        file.SaveAs(path);
                    }
                }


            }
            catch (Exception ex)
            {

            }
            return Json(filename);
        }



        public ActionResult Details(int id)
        {
            Inventory inventory = null;
            try
            {
                using (var client = new HttpClient { BaseAddress = new Uri("http://localhost:6068/") })
                {

                    var inputMessage = new HttpRequestMessage
                    {
                        Content = new StringContent(Convert.ToString(id), Encoding.UTF8, "application/json")
                    };

                    inputMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage message = client.PostAsync("api/Inventory/GetInventoryById", inputMessage.Content).Result;

                    if (!message.IsSuccessStatusCode)
                        throw new ArgumentException(message.ToString());
                    if (message.IsSuccessStatusCode)
                    {
                        var readTask = message.Content.ReadAsAsync<Inventory>();
                        readTask.Wait();
                        inventory = readTask.Result;
                    }
                }
            }

            catch (Exception ex)
            {

            }
            return View(inventory);

        }
    }
}