using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenGameListWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenGameListWebApp.Controllers
{
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        // GET api/items/GetLatest/5
        [HttpGet("GetLatest/{num}")]
        public JsonResult GetLatest(int num)
        {
            var arr = new List<ItemViewModel>();

            for (int i=1; i <= num; i++)
            {
                arr.Add(new ItemViewModel()
                {
                    Id = 1,
                    Title = string.Format("Item {0} Title", i),
                    Description = string.Format("Item {0} Description", i)
                });
            }

            var settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            };

            return new JsonResult(arr, settings);
        }
    }
}
