Startup.cs

public void ConfigureServices(IServiceCollection services)
{
    //..Other code..

    // Register Types in IoC container for DI

    //MLContext created as singleton for the whole ASP.NET Core app
    services.AddSingleton();

    //ML Model (ITransformed) created as singleton for the whole ASP.NET Core app. Loads from .zip file here.
    services.AddSingleton> ((ctx) =>
                        {
                            MLContext mlContext = ctx.GetRequiredService();
                            string modelFilePathName = Configuration["MLModel:MLModelFilePath"];

                            ITransformer mlModel;
                            using (var fileStream = File.OpenRead(modelFilePathName))
                                mlModel = mlContext.Model.Load(fileStream);

                            return (TransformerChain) mlModel;
                        });
    //..Other code..
}

MyController.cs

[Route("api/[controller]")]
[ApiController]
public class MyController : ControllerBase
{
    private readonly MLContext _mlContext;
    private readonly ITransformer _mlModel;
    
    public MyController(MLContext mlContext, ITransformer mlModel)
    {
        // Get the injected objects
        _mlContext = mlContext;
        _mlModel = mlModel;
    }

    //...Other code using the ML model (ITransformer) object...
}