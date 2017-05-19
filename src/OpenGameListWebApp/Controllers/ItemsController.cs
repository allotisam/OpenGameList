using Microsoft.AspNetCore.Mvc;
using Nelibur.ObjectMapper;
using Newtonsoft.Json;
using OpenGameListWebApp.Data;
using OpenGameListWebApp.Data.Items;
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
        #region Private Fields

        private ApplicationDbContext DbContext;

        #endregion Private Fields

        #region Constructor

        public ItemsController(ApplicationDbContext context) { DbContext = context; }

        #endregion Constructor

        #region RESTful Conventions

        /// <summary>
        /// GET: api/items
        /// </summary>
        /// <returns>Nothing: this method will raise a HttpNotFound HTTP exception, since we're not supporting this API call</returns>
        [HttpGet()]
        public IActionResult Get()
        {
            return NotFound(new { Error = "not found" });
        }

        /// <summary>
        /// GET: api/items/{id}
        /// ROUTING TYPE: attribute-based
        /// </summary>
        /// <param name="i"></param>
        /// <returns>a Json-serialized object representing a single item.</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = DbContext.Items.Where(i => i.Id == id).FirstOrDefault();
            return new JsonResult(TinyMapper.Map<ItemViewModel>(item), DefaultJsonSettings);
        }

        #endregion

        #region Attribute-based Routing

        /// <summary>
        /// GET: api/items/GetLatest
        /// ROUTING TYPE: attribute-based
        /// </summary>
        /// <returns>an array of a default number of Json-serialized objects representing the last inserted items.</returns>
        [HttpGet("GetLatest")]
        public IActionResult GetLatest()
        {
            return GetLatest(DefaultNumberOfItems);
        }

        /// <summary>
        /// GET: api/items/GetLatest/{n}
        /// ROUTING TYPE: attribute-based
        /// </summary>
        /// <param name="num"></param>
        /// <returns>An array of {n} Json-serialized objects representing the last inserted items</returns>
        [HttpGet("GetLatest/{n}")]
        public IActionResult GetLatest(int n)
        {
            if (n > MaxNumberOfItems)
            {
                n = MaxNumberOfItems;
            }

            var items = DbContext.Items.OrderByDescending(i => i.CreatedDate).Take(n).ToArray();

            return new JsonResult(ToItemViewModelList(items), DefaultJsonSettings);
        }

        /// <summary>
        /// GET: api/items/GetMostViewed
        /// ROUTING TYPE: attribute-based
        /// </summary>
        /// <returns>an array of default number of Json-serialized object representing the items with most user views.</returns>
        [HttpGet("GetMostViewed")]
        public IActionResult GetMostViewed()
        {
            return GetMostViewed(DefaultNumberOfItems);
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
            if (n > MaxNumberOfItems)
            {
                n = MaxNumberOfItems;
            }

            var items = DbContext.Items.OrderByDescending(i => i.ViewCount).Take(n).ToArray();
            return new JsonResult(ToItemViewModelList(items), DefaultJsonSettings);
        }

        /// <summary>
        /// api/items/GetRandom
        /// ROUTING TYPE: attribute-based
        /// </summary>
        /// <returns>an array of a default number of Json-serialized objects representing some randomly-picked items</returns>
        [HttpGet("GetRandom")]
        public IActionResult GetRandon()
        {
            return GetRandom(DefaultNumberOfItems);
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
            if (n > MaxNumberOfItems)
            {
                n = MaxNumberOfItems;
            }

            var items = DbContext.Items.OrderBy(i => Guid.NewGuid()).Take(n).ToArray();
            return new JsonResult(ToItemViewModelList(items), DefaultJsonSettings);
        }

        #endregion

        #region Private Members

        /// <summary>
        /// Maps a collection of Item entities into a list of ItemViewModel objects
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private List<ItemViewModel> ToItemViewModelList(IEnumerable<Item> items)
        {
            var list = new List<ItemViewModel>();
            foreach (var item in items)
            {
                list.Add(TinyMapper.Map<ItemViewModel>(item));
            }
            return list;
        }

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

        private int DefaultNumberOfItems
        {
            get
            {
                return 5;
            }
        }

        private int MaxNumberOfItems
        {
            get { return 100; }
        }

        #endregion

    }
}