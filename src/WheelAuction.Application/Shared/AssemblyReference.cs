using System.Reflection;

namespace WheelAuction.Application.Shared;

public static class AssemblyReference
{
	private static Assembly? assembly;

	public static Assembly Assembly => assembly ??= typeof(AssemblyReference).Assembly;
}