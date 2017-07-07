using AutoMapper.Mappers;
using Microsoft.AspNetCore.Mvc;
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
        #region Private Properties

        private ApplicationDbContext DbContext;

        #endregion Private Properties

        #region Constructor

        public ItemsController(ApplicationDbContext context)
        {
            DbContext = context;
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Item, ItemViewModel>();
                config.CreateMap<ItemViewModel, Item>();
            });
        }

        #endregion Constructor

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
            var item = DbContext.Items.Where(i => i.Id == id).FirstOrDefault();
            return new JsonResult(AutoMapper.Mapper.Map<Item, ItemViewModel>(item), DefaultJsonSettings);
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

            var items = DbContext.Items.OrderByDescending(i => i.CreatedDate).Take(num).ToArray();
            return new JsonResult(ToItemViewModelList(items), DefaultJsonSettings);
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

            var items = DbContext.Items.OrderByDescending(i => i.ViewCount).Take(num).ToArray();
            return new JsonResult(ToItemViewModelList(items), DefaultJsonSettings);
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

            var items = DbContext.Items.OrderBy(i => Guid.NewGuid()).Take(num).ToArray();
            return new JsonResult(ToItemViewModelList(items), DefaultJsonSettings);
        }

        #endregion

        #region Private Members

        private List<ItemViewModel> ToItemViewModelList(IEnumerable<Item> items)
        {
            var list = new List<ItemViewModel>();
            foreach (var item in items)
            {
                list.Add(AutoMapper.Mapper.Map<Item, ItemViewModel>(item));
            }
            return list;
        }

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