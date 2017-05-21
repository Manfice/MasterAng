using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.ViewModels;

namespace WebApplicationBasic.Controllers
{
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        private JsonSerializerSettings Settings
        {
            get
            {
                return new JsonSerializerSettings(){ Formatting = Formatting.Indented};
            }
        }
        private int DefaultNumberOfItems => 5;
        private int MaxNumberOfItems => 100;

        [HttpGet("GetLatest/{n}")]
        public IActionResult GetLatest(int n)
        {
            if (n > MaxNumberOfItems) n = MaxNumberOfItems;
            var arr = GetItemsSample().OrderByDescending(model => model.CreatedDate).Take(n);
            return new JsonResult(arr, Settings);
        }

        [HttpGet("GetMostViewed/{n}")]
        public IActionResult GetMostViewed(int n)
        {
            if (n > MaxNumberOfItems) n = MaxNumberOfItems;
            var items = GetItemsSample().OrderByDescending(model => model.ViewcCount).Take(n);
            return new JsonResult(items, Settings);
        }

        [HttpGet("GetRandom/{n}")]
        public IActionResult GetRandom(int n)
        {
            if (n > MaxNumberOfItems) n = MaxNumberOfItems;
            var arr = GetItemsSample().OrderBy(model => Guid.NewGuid()).Take(n);
            return Json(arr, Settings);
        }

        [HttpGet()]
        public IActionResult Get()
        {
            return NotFound(new {Error = "Not found"});
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var i = GetItemsSample().FirstOrDefault(model => model.Id == id);
            return new JsonResult(i, Settings);
        }
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
            var date = new DateTime(2017,12,31).AddDays(-n);
            for (int id = 0; id < n; id++)
            {
                list.Add(new ItemViewModel()
                {
                    Id = id,
                    Title = $"Item {id} title.",
                    Description = $"Description number {id} for...",
                    CreatedDate = date.AddDays(id),
                    LastModifiedDate = date.AddDays(id),
                    ViewcCount = n-id
                });
            }
            return list;
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