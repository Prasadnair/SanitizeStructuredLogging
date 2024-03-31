using Destructurama;
using SanitizeLogData;
using Serilog;

Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .Destructure.UsingAttributes()
            //.Destructure.ByTransforming<object>(LogSanitizer.SanitizeSensitiveData)
            .CreateLogger();

var sensitiveData = new SensitiveData
{
    Username = "praveendran",
    Password = "supersecret",
    Address = new Address
    {
        Street = "123 Main St",
        ZipCode = "12345"
    }
};

//sensitiveData =(SensitiveData) LogSanitizer.SanitizeSensitiveData(sensitiveData);

Log.Information("Sensitive data: {@SensitiveData}", sensitiveData);

Log.CloseAndFlush();
