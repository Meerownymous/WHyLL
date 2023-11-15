using System;
namespace Whyre
{
	/// <summary>
	/// Can be used to package multiple request parts together.
	/// </summary>
	public interface IRequestInput
	{
		public IRequest WriteTo(IRequest request);
	}
}