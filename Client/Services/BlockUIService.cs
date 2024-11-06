using System;
using System.Threading.Tasks;

namespace RealWorldApp.Client.Services
{
	public class BlockUIService
	{
		public event Action<string>? OnBlock;
		public event Action<string>? OnRelease;

		public Task Block(string elementId)
		{
			OnBlock?.Invoke(elementId);
			return Task.CompletedTask;
		}

		public Task Release(string elementId)
		{
			OnRelease?.Invoke(elementId);
			return Task.CompletedTask;
		}
	}
}
