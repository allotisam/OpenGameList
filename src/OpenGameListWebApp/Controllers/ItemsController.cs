using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nelibur.ObjectMapper;
using Newtonsoft.Json;
using OpenGameListWebApp.Data;
using OpenGameListWebApp.Data.Items;
using OpenGameListWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace OpenGameListWebApp.Controllers
{
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        #region Private Fields
        private ApplicationDbContext _dbContext;
        #endregion

        #region Constructor
        public ItemsController(ApplicationDbContext dbContext) { _dbContext = dbContext; }
        #endregion

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
            var item = _dbContext.Items.Where(i => i.Id == id).FirstOrDefault();

            if (item != null)
                return new JsonResult(TinyMapper.Map<ItemViewModel>(item), DefaultJsonSettings);
            else
                return NotFound(new { Error = string.Format("Item ID {0} has not been found", id) });            
        }

        /// <summary>
        /// POST: api/items
        /// </summary>
        /// <param name="ivm"></param>
        /// <returns>Creates a new Item and return it accordingly</returns>
        [HttpPost()]
        [Authorize]
        public IActionResult Add([FromBody]ItemViewModel ivm)
        {
            if (ivm != null)
            {
                // Create a new Item with the client-sent json data
                var item = TinyMapper.Map<Item>(ivm);

                // override any property that coculd be wise to set from server-side only
                item.CreatedDate = item.LastModifiedDate = DateTime.Now;
                item.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                _dbContext.Items.Add(item);
                _dbContext.SaveChanges();

                return new JsonResult(TinyMapper.Map<ItemViewModel>(item), DefaultJsonSettings);
            }

            // return a generic HTTP Status 500 (Not Found) if the client payload is invalid.
            return new StatusCodeResult(500);
        }

        /// <summary>
        /// PUT: api/items/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ivm"></param>
        /// <returns>Updates an existing item and return it accordingly</returns>
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int id, [FromBody]ItemViewModel ivm)
        {
            if (ivm != null)
            {
                var item = _dbContext.Items.Where(i => i.Id == id).FirstOrDefault();

                if (item != null)
                {
                    // handle the udpate
                    item.UserId = ivm.UserId;
                    item.Description = ivm.Description;
                    item.Flags = ivm.Flags;
                    item.Notes = ivm.Notes;
                    item.Text = ivm.Text;
                    item.Title = ivm.Title;
                    item.Type = ivm.Type;
                    item.LastModifiedDate = DateTime.Now;

                    _dbContext.SaveChanges();

                    return new JsonResult(TinyMapper.Map<ItemViewModel>(item), DefaultJsonSettings);
                }
            }

            return NotFound(new { Error = string.Format("Item ID {0} has not been found.", id) });
        }

        /// <summary>
        /// DELETE: api/items/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Deletes an Item, returning a HTTP status 200 (ok) when done.</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var item = _dbContext.Items.Where(i => i.Id == id).FirstOrDefault();

            if (item != null)
            {
                _dbContext.Items.Remove(item);
                _dbContext.SaveChanges();

                return new OkResult();
            }

            return NotFound(new { Error = string.Format("Item ID {0} has not been found", id) });
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

            var items = _dbContext.Items.OrderByDescending(i => i.CreatedDate).Take(n).ToArray();

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

            var items = _dbContext.Items.OrderByDescending(i => i.ViewCount).Take(n).ToArray();

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

            var items = _dbContext.Items.OrderBy(i => Guid.NewGuid()).Take(n).ToArray();

            return new JsonResult(ToItemViewModelList(items), DefaultJsonSettings);
        }

        #endregion

        #region Private Members

        /// <summary>
        /// Maps a collection of Item entities into a list of ItemViewModel objects;
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private List<ItemViewModel> ToItemViewModelList(IEnumerable<Item> items)
        {
            var list = new List<ItemViewModel>();
            foreach (var i in items)
                list.Add(TinyMapper.Map<ItemViewModel>(i));

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
