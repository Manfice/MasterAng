using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Model;
using Web.Model.Items;
using Web.ViewModels;

namespace WebApplicationBasic.Controllers
{
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemsController(ApplicationDbContext context)
        {
            _context = context;
        }
        private static JsonSerializerSettings Settings => new JsonSerializerSettings() { Formatting = Formatting.Indented };
        private static int DefaultNumberOfItems => 5;
        private static int MaxNumberOfItems => 100;

        [HttpGet("GetLatest/{n}")]
        public IActionResult GetLatest(int n)
        {
            if (n > MaxNumberOfItems) n = MaxNumberOfItems;
            var arr = _context.Items.OrderByDescending(item => item.CreatedDate).Take(n);
            return new JsonResult(ToItemVmFromItem(arr), Settings);
        }

        [HttpGet("GetMostViewed/{n}")]
        public IActionResult GetMostViewed(int n)
        {
            if (n > MaxNumberOfItems) n = MaxNumberOfItems;
            var items = _context.Items.OrderByDescending(model => model.ViewCount).Take(n);
            return new JsonResult(ToItemVmFromItem(items), Settings);
        }

        [HttpGet("GetRandom/{n}")]
        public IActionResult GetRandom(int n)
        {
            if (n > MaxNumberOfItems) n = MaxNumberOfItems;
            var arr = _context.Items.OrderBy(model => Guid.NewGuid()).Take(n);
            return Json(ToItemVmFromItem(arr), Settings);
        }

        #region CRUD with item

        [HttpGet()]
        public IActionResult Get()
        {
            return NotFound(new { Error = "Not found" });
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = _context.Items.FirstOrDefault(i => i.Id == id);

            return item != null ? new JsonResult(Mapper.Map<ItemViewModel>(item), Settings) : (IActionResult)NotFound(new { Error = $"Item with id = {id} was not found" });
        }

        [HttpPost()]
        public IActionResult Add(ItemViewModel ivm)
        {
            if (ivm == null) return StatusCode(500);
            var item = Mapper.Map<Item>(ivm);
            item.CreatedDate = item.LastModifiedDate = DateTime.Now;
            item.UserId =
                _context.Users.FirstOrDefault(
                    u => u.UserName.Equals("admin", StringComparison.CurrentCultureIgnoreCase))?.Id;
            _context.Items.Add(item);
            return new JsonResult(Mapper.Map<ItemViewModel>(item), Settings);
        }

        [HttpPut("{id}")]
        public IActionResult Upadte(int id, ItemViewModel ivm)
        {
            if (ivm == null) return StatusCode(500, new { Error = $"Nothing to update" });
            var item = _context.Items.FirstOrDefault(i => i.Id == id);
            if (item == null) return NotFound(new { Error = $"Item with id ={ id } was not found" });
            item.UserId = ivm.UserId;
            item.Description = ivm.Description;
            item.Flags = ivm.Flags;
            item.Notes = ivm.Notes;
            item.Text = ivm.Text;
            item.Title = ivm.Title;
            item.Type = ivm.Type;
            item.LastModifiedDate = DateTime.Now;
            _context.SaveChanges();
            return new JsonResult(Mapper.Map<ItemViewModel>(item), Settings);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.Items.Find(id);
            if (item == null) return NotFound(new { Error = $"Item with id: {id} was not found" });
            _context.Items.Remove(item);
            _context.SaveChanges();
            return Ok();
        }

        #endregion

        [HttpGet("GetLatest")]
        public IActionResult GetLatest()
        {
            return GetLatest(DefaultNumberOfItems);
        }
        [HttpGet("GetMostViewed")]
        public IActionResult GetMostViewed()
        {
            return GetMostViewed(DefaultNumberOfItems);
        }
        [HttpGet("GetRandom")]
        public IActionResult GetRandom()
        {
            return GetRandom(DefaultNumberOfItems);
        }
        private List<ItemViewModel> GetItemsSample(int n = 999)
        {
            var list = new List<ItemViewModel>();
            var date = new DateTime(2017, 12, 31).AddDays(-n);
            for (int id = 0; id < n; id++)
            {
                list.Add(new ItemViewModel()
                {
                    Id = id,
                    Title = $"Item {id} title.",
                    Description = $"Description number {id} for...",
                    CreatedDate = date.AddDays(id),
                    LastModifiedDate = date.AddDays(id),
                    ViewcCount = n - id
                });
            }
            return list;
        }

        private List<ItemViewModel> ToItemVmFromItem(IEnumerable<Item> items)
        {
            return items.Select(Mapper.Map<ItemViewModel>).ToList();
        }
    }
}
//private static string[] Summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//[HttpGet("[action]")]
//public IEnumerable<WeatherForecast> WeatherForecasts()
//{
//    var rng = new Random();
//    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
//    {
//        DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
//        TemperatureC = rng.Next(-20, 55),
//        Summary = Summaries[rng.Next(Summaries.Length)]
//    });
//}

//public class WeatherForecast
//{
//    public string DateFormatted { get; set; }
//    public int TemperatureC { get; set; }
//    public string Summary { get; set; }

//    public int TemperatureF
//    {
//        get
//        {
//            return 32 + (int)(TemperatureC / 0.5556);
//        }
//    }
//}