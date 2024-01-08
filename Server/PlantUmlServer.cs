//using System.Net;
//using System.Text;
//using PlantUmlICheerpJServer.Server.Extensions;
//using WireMock;
//using WireMock.RequestBuilders;
//using WireMock.ResponseBuilders;
//using WireMock.Server;
//using WireMock.Settings;
//using WireMock.Types;
//using WireMock.Util;
//using Response = WireMock.ResponseBuilders.Response;

//namespace PlantUmlICheerpJServer.Server;

//public class PlantUmlServer
//{
//    private static WireMockServer? _sInstance;

//    public static void Start()
//    {
//        _sInstance = WireMockServer.Start(new WireMockServerSettings { Urls = new[] { "http://localhost:5017" }, });
//        SetupGetPlantUmlJar();
//        SetupGetPlantUmlJarJs();
//        SetupGetPlantUml();
//        SetupPlantUmlUi();
//    }

//    private static void SetupPlantUmlUi()
//    {
//        _sInstance!
//            .Given(Request.Create().UsingGet().WithPath(ignoreCase: true, "/plantumlui"))
//            .RespondWith(Response.Create()
//                .WithCallback(requestMessage =>
//                {
//                    var html = File.ReadAllText(@"Server/plantumlRenderer.html");
//                    return new ResponseMessage
//                    {
//                        StatusCode = HttpStatusCode.OK,
//                        BodyDestination = BodyDestinationFormat.String,
//                        BodyData = new BodyData
//                        {
//                            DetectedBodyType = BodyType.String,
//                            BodyAsString = html
//                        }
//                    };
//                }));
//    }

//    private static void SetupGetPlantUml()
//    {
//        _sInstance!
//            .Given(Request.Create().UsingGet().WithPath(ignoreCase: true, "/plantuml"))
//            .RespondWith(Response.Create()
//                .WithCallback(requestMessage =>
//                {
//                    var plantUml = requestMessage.Body;
//                    //string response = null;

//                    var engine = new Jurassic.ScriptEngine();

//                    var cheerpJ = File.ReadAllText(@"Server/loader.js");

//                    var jscript = @"function wrapper() {
//        let self = this;
//        let location = requestMessage
//        " + cheerpJ + @"
//    };
 
//    wrapper();

//";
    

//                    var response = engine.Evaluate(jscript);

//                    return new ResponseMessage
//                    {
//                        StatusCode = HttpStatusCode.OK,
//                        BodyDestination = BodyDestinationFormat.String,
//                        BodyData = new BodyData
//                        {
//                            DetectedBodyType = BodyType.String,
//                            BodyAsString = response.ToString()
//                        }
//                    };
//                }));
//    }

//    private static void SetupGetPlantUmlJar()
//    {
//        _sInstance!
//            .Given(Request.Create().UsingGet().WithPath(ignoreCase: true, "/plantuml-core.jar"))
//            .RespondWith(Response.Create()
//                .WithCallback(requestMessage =>
//                {
//                    return GetJar();
//                }));

//        _sInstance!
//            .Given(Request.Create().UsingHead().WithPath(ignoreCase: true, "/plantuml-core.jar"))
//            .RespondWith(Response.Create()
//                .WithCallback(requestMessage =>
//                {
//                    return GetJar();
//                }));
//    }

//    private static void SetupGetPlantUmlJarJs()
//    {
//        _sInstance!
//            .Given(Request.Create().UsingGet().WithPath(ignoreCase: true, "/plantuml-core.jar.js"))
//            .RespondWith(Response.Create()
//                .WithCallback(requestMessage =>
//                {
//                    return GetJarJs();
//                }));

//        _sInstance!
//            .Given(Request.Create().UsingHead().WithPath(ignoreCase: true, "/plantuml-core.jar.js"))
//            .RespondWith(Response.Create()
//                .WithCallback(requestMessage =>
//                {
//                    return GetJarJs();
//                }));
//    }

//    public static ResponseMessage GetJarJs()
//    {
//        return new ResponseMessage
//        {
//            StatusCode = HttpStatusCode.OK,
//            BodyDestination = BodyDestinationFormat.String,
//            Headers = new Dictionary<string, WireMockList<string>> { { "Content-Type", new WireMockList<string> { "text/javascript" } } },
//            BodyData = new BodyData
//            {
//                DetectedBodyType = BodyType.String,
//                BodyAsString = File.ReadAllText(@"Server/plantuml-core.jar.js")
//            }
//        };
//    }

//    public static ResponseMessage GetJar()
//    {
//        return new ResponseMessage
//        {
//            StatusCode = HttpStatusCode.OK,
//            BodyDestination = BodyDestinationFormat.Bytes,
//            Headers = new Dictionary<string, WireMockList<string>> { { "Content-Type", new WireMockList<string> { "application/octet-stream" } } },
//            BodyData = new BodyData
//            {
//                DetectedBodyType = BodyType.Bytes,
//                BodyAsFile = @"Server/plantuml-core.jar"
//            }
//        };
//    }

//    public static void Dispose() => _sInstance?.Dispose();
//}