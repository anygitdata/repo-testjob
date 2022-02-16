using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestJob.Models.ModelViews
{
	public class ModelProjectMenu
	{
		public int key {get; set;}
		public Guid id { get; set; }
		public string projectName { get; set; }

		public DateTime createDate { get; set; }
		public DateTime updateDate { get; set; }
		public string disabled { get; set; }

		public string lineThrough { get; set; }
	}
}
