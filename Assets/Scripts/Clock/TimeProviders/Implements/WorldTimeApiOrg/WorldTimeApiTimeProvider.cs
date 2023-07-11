using System;
using Unity.Plastic.Newtonsoft.Json;

namespace DDX.Clock.TimeProviders.Implements
{
    public class WorldTimeApiOrgTimeProvider : HttpTimeProviderBase<WorldTimeApiOrgResponse>
    {
        protected override string Uri => "http://worldtimeapi.org/api/ip";

        protected override DateTime GetDateTimeFromResponse(WorldTimeApiOrgResponse response)
            => response.Datetime;

        protected override WorldTimeApiOrgResponse ParseTextResponse(string textResponse)
            => JsonConvert.DeserializeObject<WorldTimeApiOrgResponse>(textResponse);
    }
}
