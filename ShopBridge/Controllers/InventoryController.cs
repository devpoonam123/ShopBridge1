using ShopBridge.Models;
using System;
using System.Collections.Generic;
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
            //try
            //{
            //    using (var client = new HttpClient())
            //    {
            //        client.BaseAddress = new Uri("http://localhost:6068/");
            //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //        var result = client.GetAsync("api/Inventory/GetAll").Result;
            //        if (result.IsSuccessStatusCode)
            //        {
            //            var readTask = result.Content.ReadAsAsync<IList<Inventory>>();
            //            readTask.Wait();

            //            inventories = readTask.Result;
            //        }

            //        else
            //        {
            //            inventories = Enumerable.Empty<Inventory>();
            //            ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //}

            //  return View(inventories);


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:6068/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/Inventory/GetAll").Result;
            inventories = response.Content.ReadAsAsync<IList<Inventory>>().Result;
            // return View("~/Views/GetEmploye.cshtml", cd);
            return View(inventories);

        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            Inventory inventory = null;
            //using (var client = new HttpClient())
            //{

            //    client.BaseAddress = new Uri("http://localhost:6068/");
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    //var response = await client.GetAsync(string.format("api/Inventory/GetInventoryById/id={0}&type={1}", param.Id.Value, param.Id.Type));
            //    //var result = client.GetAsync("api/Inventory/GetInventoryById/"+"1").Result;
            //    var result = client.GetAsync(string.Format("api/Inventory/GetInventoryById?id={0}", id)).Result;
                
            //    if (result.IsSuccessStatusCode)
            //    {
            //        var readTask = result.Content.ReadAsAsync<Inventory>();
            //        readTask.Wait();

            //        inventory = readTask.Result;
            //    }

            //    else
            //    {
            //        inventory = new Inventory();
            //        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");

            //    }
            //}
           


            using (var client = new HttpClient { BaseAddress = new Uri("http://localhost:6068/") })
            {
                //string serailizeddto = JsonConvert.SerializeObject(dto);

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
            return View(inventory);

        }
    }
}