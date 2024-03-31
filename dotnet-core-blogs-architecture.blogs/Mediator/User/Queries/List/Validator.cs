using DT.Identity.Core.Common.Validators;
using FluentValidation;
using DT.Identity.Repository.Common;

namespace DT.Identity.Core.User.Queries.List;
public class Validator : PageQueryValidator<QueryModel>
{
	public Validator() : base()
	{
		RuleFor(x => x.LocFilter).MaximumLength(100);
		RuleFor(x => x.DesFilter).MaximumLength(100);
		RuleFor(x => x.DepFilter).MaximumLength(100);
		RuleFor(x => x.SortCol)
			.Must(x => new[] { "Name", "Location", "Department", "Designation", "Status" }.Contains(x))
			.WithMessage("Sorting is applicable for 'Name', 'Location', 'Department', 'Designation' and 'Status'")
			.MaximumLength(50)
			.When(x => !string.IsNullOrWhiteSpace(x.SortCol));
		RuleFor(x => x.TypeFilter)
			.Must(x => new[] { "Employee", "Agent", "Vendor", "Customer" }.Contains(x))
			.WithMessage("Type can be only 'Employee', 'Agent', 'Vendor' and 'Customer'").MaximumLength(100)
			.When(x => !string.IsNullOrWhiteSpace(x.TypeFilter)); 
	}
}
