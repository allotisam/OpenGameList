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
        #region Attribute-based Routing

        // GET: api/items
        [HttpGet()]
        public IActionResult Get()
        {
            return NotFound(new { Error = "not found" });
        }

        // GET: api/items/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return new JsonResult(GetSampleItems().Where(i => i.Id == id).FirstOrDefault(), DefaultJsonSettings);
        }

        // GET: api/items/GetLatest
        [HttpGet("GetLatest")]
        public IActionResult GetLatest()
        {
            return GetLatest(DefaultNumberOfItems);
        }


        // GET: api/items/GatLatest/5
        [HttpGet("GetLatest/{num}")]
        public IActionResult GetLatest(int num)
        {
            if (num > MaxNumberOfItems)
                num = MaxNumberOfItems;

            var items = GetSampleItems().OrderByDescending(i => i.CreatedDate).Take(num);
            return new JsonResult(items, DefaultJsonSettings);
        }

        // GET: api/items/GetMostViewed
        [HttpGet("GetMostViewed")]
        public IActionResult GetMostViewed()
        {
            return GetMostViewed(DefaultNumberOfItems);
        }

        // GET: api/items/GetMostViewed/{n}
        [HttpGet("GetMostViewed/{num}")]
        public IActionResult GetMostViewed(int num)
        {
            if (num > MaxNumberOfItems)
                num = MaxNumberOfItems;

            var items = GetSampleItems().OrderByDescending(i => i.ViewCount).Take(num);
            return new JsonResult(items, DefaultJsonSettings);
        }

        // GET: api/items/GetRandom
        [HttpGet("GetRandom")]
        public IActionResult GetRandom()
        {
            return GetRandom(DefaultNumberOfItems);
        }

        // GET: api/items/GetRandom/{n}
        [HttpGet("GetRandom/{num}")]
        public IActionResult GetRandom(int num)
        {
            if (num > MaxNumberOfItems)
                num = MaxNumberOfItems;

            var items = GetSampleItems().OrderBy(i => Guid.NewGuid()).Take(num);
            return new JsonResult(items, DefaultJsonSettings);
        }

        #endregion

        #region Private Members

        private List<ItemViewModel> GetSampleItems(int num = 999)
        {
            List<ItemViewModel> list = new List<ItemViewModel>();
            DateTime date = new DateTime(2015, 12, 31).AddDays(-num);
            for (int id=1; id <= num; id++)
            {
                list.Add(new ItemViewModel()
                {
                    Id = id,
                    Title = string.Format("Item {0} Title", id),
                    Description = string.Format("This is a sample description for item {0}: Lorem ipsum dolor sit amet.", id),
                    CreatedDate = date.AddDays(id),
                    LastModifiedDate = date.AddDays(id),
                    ViewCount = num - id
                });
            }

            return list;
        }

        private JsonSerializerSettings DefaultJsonSettings
        {
            get
            {
                return new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                };
            }
        }

        private int DefaultNumberOfItems
        {
            get { return 5; }
        }

        private int MaxNumberOfItems
        {
            get { return 100; }
        }

        #endregion
    }
}