using Microsoft.AspNetCore.Http.HttpResults;
using SDKServer.Models.BaseConfig;

namespace SDKServer.Handlers;

internal static class ConfigHandler
{
    public static JsonHttpResult<BaseConfigModel> GetBaseConfig()
    {
        return TypedResults.Json(new BaseConfigModel
        {
            CdnUrl = [
                new CdnUrlEntry
                {
                    Url = "http://127.0.0.1:5500/dev/client/",
                    Weight = "100"
                },
                new CdnUrlEntry
                {
                    Url = "http://127.0.0.1:5500/dev/client/",
                    Weight = "100"
                }
            ],
            SecondaryUrl = [],
            GmOpen = false,
            PayUrl = "http://114.132.150.182:12281/ReceiptNotify/PayNotify",
            TDCfg = new TDConfig
            {
                Url = "https://ali-sh-datareceiver.kurogame.xyz",
                AppID = "KEY", // replace to key on readme.md
            },
            LogReport = new LogReportConfig
            {
                Ak = "KEY", // replace to key on readme.md
                Sk = "KEY", // replace to key on readme.md
                Name = "KEY", // replace to key on readme.md
                Region = "ap-shanghai"
            },
            NoticUrl = "https://prod-alicdn-gmserver-static.kurogame.com",
            LoginServers = [
                new LoginServerEntry
                {
                    Id = "1074",
                    Name = "ReversedRooms",
                    Ip = "127.0.0.1"
                }
            ],
            PrivateServers = new PrivateServersConfig
            {
                Enable = false,
                ServerUrl = ""
            }
        });
    }
}
