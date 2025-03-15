using Core.CrossCuttingConcerns.Serilog.ConfigurationModels;
using Core.CrossCuttingConcerns.Serilog.Messages;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Core.CrossCuttingConcerns.Serilog.Loggers;

public class FileLogger : LoggerServiceBase
{
    private readonly IConfiguration _configuration;

    public FileLogger(IConfiguration configuration)
    {
        _configuration = configuration;

        FileLogConfiguration logConfig =
            configuration.GetSection("SeriLogConfigurations:FileLogConfiguration").Get<FileLogConfiguration>()
            ?? throw new Exception(SerilogMessages.NullOptionsMessage);
        // FileLogConfiguration nesnesi appsettings.json içinde okunur.
        // Eğer appsettings.json içinde ilgili ayar bulunmazsa, bir hata (Exception) fırlatılır.

        string logFilePath = string.Format(format: "{0}{1}", arg0: Directory.GetCurrentDirectory() + logConfig.FolderPath, arg1: ".txt");
        // Directory.GetCurrentDirectory() => Uygulamanın çalıştığı dizini alır
        // logConfig.FolderPath → appsettings.json içinden gelen klasör yolu.



        /*
           logFilePath:	Log dosyasının yolu.
           rollingInterval: RollingInterval.Day	Her gün yeni bir log dosyası oluştur.
           retainedFileCountLimit: null	Kaç dosya saklanacağını sınırsız yap.
           fileSizeLimitBytes: 5000000	Maksimum 5MB büyüklüğe ulaştığında yeni dosya oluştur.
           outputTemplate:	Log mesajlarının formatını belirler.
        */
        Logger = new LoggerConfiguration().WriteTo.File(
            logFilePath, rollingInterval: RollingInterval.Day,
            retainedFileCountLimit: null,
            fileSizeLimitBytes: 5000000,
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}"
            ).CreateLogger();

        // RollingInterval => Her gün yeni bir dosya oluştur
        // retainedFileCountLimit => sınır olsun mu
        // fileSizeLimitBytes => dosya boyutu
    }
}