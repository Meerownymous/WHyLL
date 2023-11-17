using System;
namespace WHyLL
{
	public interface IMessageInput
	{
		IMessage WriteTo(IMessage message);
	}
}

