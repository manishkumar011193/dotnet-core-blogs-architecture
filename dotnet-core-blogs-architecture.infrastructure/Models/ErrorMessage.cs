using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_core_blogs_architecture.Data.Models;

public class ErrorMessage
{
	//
	// Summary:
	//     Gets or sets the field for this error.       
	public string Field { get; set; }

	//
	// Summary:
	//     Gets or sets the description for this error.                                                                                   
	public List<string> Description { get; set; }
}
