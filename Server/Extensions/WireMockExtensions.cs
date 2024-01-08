using WireMock.RequestBuilders;

namespace PlantUmlICheerpJServer.Server.Extensions;

public static class WireMockExtensions
{
    public static IRequestBuilder WithPath(this IRequestBuilder builder, bool ignoreCase, params string[] allowedPaths)
    {
        return builder.WithPath(
            path => allowedPaths.Any(allowedPath => string.Equals(path, allowedPath, StringComparison.OrdinalIgnoreCase)));
    }
}