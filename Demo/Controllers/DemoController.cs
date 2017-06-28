using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
	[Route("[controller]")]
	public class DemoController : Controller
	{
		[HttpGet("GetTime")]
		public IActionResult GetTime()
		{
			return Json(DateTime.Now);
		}
	}
}