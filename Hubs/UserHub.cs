using Microsoft.AspNetCore.SignalR;

namespace SignalRDemo.Hubs
{
	public class UserHub : Hub
	{
		public static int TotalViews { get; set; } = 0;
		public static int TotalUsers { get; set; } = 0;

		public override Task OnConnectedAsync()
		{
			// When connect then toal users increase by one
			++TotalUsers;
			Clients.All.SendAsync("updateTotalUsers", TotalUsers).GetAwaiter();
			return base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(Exception? exception)
		{
			// When disconnect then toal users increase by one
			--TotalUsers;
			Clients.All.SendAsync("updateTotalUsers", TotalUsers).GetAwaiter();
			return base.OnDisconnectedAsync(exception);
		}

		public async Task NewWindowLoaded()
		{
			++TotalViews;
			// Send update to all clients that total views have been updated
			await Clients.All.SendAsync("updateTotalViews", TotalViews);
		}

	}
}
