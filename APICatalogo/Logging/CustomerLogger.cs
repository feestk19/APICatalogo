
namespace APICatalogo.Logging;

public class CustomerLogger : ILogger
{
    readonly string loggerName;

    readonly CustomerLoggerProviderConfiguration loggerConfig;

    public CustomerLogger(string name, CustomerLoggerProviderConfiguration config)
    {
        loggerName = name;
        loggerConfig = config;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == loggerConfig.LogLevel;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        string mensagem = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";

        EscreverTextoNoArquivo(mensagem);
    }

    private void EscreverTextoNoArquivo(string mensagem)
    {
        string caminhoArquivoLog = @"C:\DEV\APICatalogo\Log\Strombeck_log.txt";

        using(StreamWriter streamWriter = new StreamWriter(caminhoArquivoLog, true))
        {
            try
            {
                streamWriter.WriteLine(mensagem);
                streamWriter.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
