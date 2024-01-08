using System.Net;
using System.Text;
using WireMock;
using WireMock.ResponseBuilders;
using WireMock.Types;
using WireMock.Util;

namespace PlantUmlICheerpJServer.Server.Extensions;

public static class HttpStatusCodeExtensions
{
    public static ResponseMessage CreateResponse(this HttpStatusCode statusCode, string body)
    {
        var response = new ResponseMessage
        {
            StatusCode = statusCode,
            BodyDestination = BodyDestinationFormat.SameAsSource,
            BodyData = new BodyData
            {
                Encoding = Encoding.UTF8,
                DetectedBodyType = BodyType.String,
                BodyAsString = body,
            }
        };
        return response;
    }
}