using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestJob.Models.ModelViews
{
	public class ModelProjectMenu
	{
		public int Key {get; set;}
		public Guid Id { get; set; }
		public string ProjectName { get; set; }

		public DateTime CreateDate { get; set; }
		public DateTime? UpdateDate { get; set; }

		public string Disabled { get; set; }	// css style
		public string LineThrough { get; set; }	// css style
	}
}
