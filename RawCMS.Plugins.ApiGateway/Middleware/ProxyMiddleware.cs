﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RawCMS.Library.Core.Extension;
using RawCMS.Plugins.ApiGateway.Classes;
using RawCMS.Plugins.ApiGateway.Classes.Balancer.Handles;
using RawCMS.Plugins.ApiGateway.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RawCMS.Plugins.ApiGateway.Middleware
{
    [MiddlewarePriority(Order = 2)]
    public class ProxyMiddleware : GatewayMiddleware
    {
        public ProxyMiddleware(RequestDelegate requestDelegate, ILogger logger, ApiGatewayConfig config, IEnumerable<RawHandler> handlers)
            : base(requestDelegate, logger, config, handlers)
        {
        }

        public override string Name => "ProxyMiddleware";

        public override string Description => "Enable Proxy capability";

        public async override Task InvokeAsync(HttpContext context)
        {
            logger.LogDebug("Invoke ProxyMiddleware start");
            var host = context.Request.Host.Value;
            var scheme = context.Request.Scheme;
            var port = context.Request.Host.Port;
            var path = context.Request.Path;

            var vhosts = pluginConfig?.Proxy?.Where(x => x.Host.Equals(host, StringComparison.InvariantCultureIgnoreCase) &&
                                         x.Scheme.Equals(scheme, StringComparison.InvariantCultureIgnoreCase) &&
                                         new Regex(x.Path).Match(path).Success &&
                                         x.Port == port && x.Enable).ToList();
            if (vhosts?.Count > 0)
            {
                // TODO: get regex that not contains other regex
                var vhost = vhosts?.OrderByDescending(x => x.Path?.Length).FirstOrDefault();
                if(vhost.Node?.Enable ?? true)
                {
                    context.Request.Headers["X-Forwarded-For"] = context.Connection.RemoteIpAddress.ToString();
                    context.Request.Headers["X-Forwarded-Proto"] = context.Request.Protocol.ToString();
                    int portDest = context.Request.Host.Port ?? (context.Request.IsHttps ? 443 : 80);
                    context.Request.Headers["X-Forwarded-Port"] = portDest.ToString();
                    var handlerType = context.WebSockets.IsWebSocketRequest ? HandlerProtocolType.Socket : HandlerProtocolType.Http;
                    var handler = handlers.First(x => x.HandlerRequestType == handlerType);
                    await handler.HandleRequest(context, vhost.Node);
                }else
                {
                    await next(context);
                }
            }
            else
            {
                await next(context);
            }
        }
    }
}
