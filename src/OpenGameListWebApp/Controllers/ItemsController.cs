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

        /// <summary>
        /// GET: api/items/GetLatest/{n}
        /// ROUTING TYPE: attribute-based
        /// </summary>
        /// <param name="num"></param>
        /// <returns>An array of {n} Json-serialized objects representing the last inserted items</returns>
        [HttpGet("GetLatest/{n}")]
        public IActionResult GetLatest(int n)
        {
            var items = GetSampleItems().OrderByDescending(i => i.CreatedDate).Take(n);

            return new JsonResult(items, DefaultJsonSettings);            
        }

        /// <summary>
        /// GET: api/items/GetMostViweed/{n}
        /// ROUTING TYPE: attribute-based
        /// </summary>
        /// <param name="n"></param>
        /// <returns>an array of {n} Json-serialized objects representing the items with the most user views</returns>
        [HttpGet("GetMostViewed/{n}")]
        public IActionResult GetMostViewed(int n)
        {
            var items = GetSampleItems().OrderByDescending(i => i.ViewCount).Take(n);

            return new JsonResult(items, DefaultJsonSettings);
        }

        /// <summary>
        /// GET: api/items/GetRandom/{n}
        /// ROUTING TYPE: attribute-based
        /// </summary>
        /// <param name="n"></param>
        /// <returns>An array of {n} Json-Serialized object representing some randomly-picked items</returns>
        [HttpGet("GetRandom/{n}")]
        public IActionResult GetRandom(int n)
        {
            var items = GetSampleItems().OrderBy(i => Guid.NewGuid()).Take(n);

            return new JsonResult(items, DefaultJsonSettings);
        }

        #endregion

        #region Private Members

        /// <summary>
        /// Generate a sample array of source Items to emulate a database (for testing purpose only)
        /// </summary>
        /// <param name="num"></param>
        /// <returns>a defined number of mock items (for testing purposes only)</returns>
        private List<ItemViewModel> GetSampleItems(int num = 999)
        {
            List<ItemViewModel> list = new List<ItemViewModel>();
            DateTime date = new DateTime(2015, 12, 31).AddDays(-num);

            for (int id = 1; id <= num; id++)
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

        /// <summary>
        /// returns a suitable JsonSerializerSettings object that can be used to generate the JsonResult return value for this controller's methods
        /// </summary>
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

        #endregion

    }
}
