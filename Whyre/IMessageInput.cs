using System;
namespace Whyre
{
	public interface IMessageInput
	{
		IMessage WriteTo(IMessage message);
	}
}

