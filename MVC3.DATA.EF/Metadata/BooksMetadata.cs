using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;//added for metadata

namespace MVC3.DATA.EF//.Metadata
{

	#region ---Book Metadata---
	[MetadataType(typeof(BookMetadata))]
	public partial class Book
	{
	}

	public class BookMetadata
	{
		[StringLength(17, MinimumLength = 13, ErrorMessage = "* ISBN numbers are 10 digits with 3 dashes or 13 digits with 4 dashes")]
		public string ISBN { get; set; }

		[Display(Name = "Title")]
		[Required(ErrorMessage = "* REQUIRED")]
		[StringLength(100)]
		public string BookTitle { get; set; }

		[UIHint("MultilineText")]
		public string Description { get; set; }

		[Display(Name = "Genre")]
		public Nullable<int> GenreID { get; set; }

		[DisplayFormat(DataFormatString = "{0:c}")]
		public Nullable<decimal> Price { get; set; }

		[Display(Name = "Units Sold")]
		[DisplayFormat(DataFormatString = "{0:n0}")]
		public Nullable<int> UnitsSold { get; set; }

		[DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
		[Display(Name = "Publish Date")]
		public Nullable<System.DateTime> PublishDate { get; set; }

		[Display(Name = "Publisher")]
		public int PublisherID { get; set; }

		[Display(Name = "Cover")]
		public string BookImage { get; set; }

		[Display(Name = "Site Feature")]
		public bool IsSiteFeature { get; set; }

		[Display(Name = "Genre Feature")]
		public bool IsGenreFeature { get; set; }

		[Display(Name = "Book Status")]
		public int BookStatusID { get; set; }
	}
	#endregion

	#region ---Publisher Metadata---
	[MetadataType(typeof(PublisherMetadata))]
	public partial class Publisher
	{
	}

	public class PublisherMetadata
	{
		[Display(Name = "Publisher")]
		public string PublisherName { get; set; }

		public string City { get; set; }
		public string State { get; set; }

		[Display(Name = "Active")]
		public bool IsActive { get; set; }
	}
	#endregion

	#region ---Genre Metadata---
	[MetadataType(typeof(GenreMetadata))]
	public partial class Genre
	{
	}

	public class GenreMetadata
	{
		[Display(Name = "Genre")]
		public string GenreName { get; set; }
	}
	#endregion

	#region ---BookStatus Metadata---
	[MetadataType(typeof(BookStatusMetadata))]
	public partial class BookStatus
	{
	}

	public class BookStatusMetadata
	{
		[Display(Name = "Status")]
		public string BookStatusName { get; set; }

		[UIHint("MultilineText")]
		public string Notes { get; set; }
	}
	#endregion
}
