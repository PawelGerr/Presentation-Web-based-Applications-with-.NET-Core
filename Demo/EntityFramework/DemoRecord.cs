using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.EntityFramework
{
	public class DemoRecord
	{
		[Key]
		public Guid Id { get; set; }

		[Required, MaxLength(100)]
		public string Name { get; set; }
	}
}
