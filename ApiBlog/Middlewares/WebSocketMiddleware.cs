using System.Net.WebSockets;
using System.Text;

namespace ApiBlog.Middlewares
{
    public class WebSocketMiddleware
    {
        private readonly RequestDelegate _next;

        public WebSocketMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                await HandleWebSocketCommunication(context, webSocket);
            }
            else
            {
                await _next(context);
            }
        }

        private async Task HandleWebSocketCommunication(HttpContext context, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), context.RequestAborted);

            while (!result.CloseStatus.HasValue)
            {
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                var serverMsg = Encoding.UTF8.GetBytes($"Server: {message}");

                await webSocket.SendAsync(new ArraySegment<byte>(serverMsg, 0, serverMsg.Length), result.MessageType, result.EndOfMessage, context.RequestAborted);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), context.RequestAborted);
            }

            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, context.RequestAborted);
        }
    }
}
