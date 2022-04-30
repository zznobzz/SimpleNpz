using SimpleNpz.Services.Abstractions;

namespace SimpleNpz.WebApi.Backgrounds;

public class TanksUpdaterHostedService : BackgroundService
{
    private Timer _timer;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IHostApplicationLifetime _lifetime;
    private readonly ILogger<TanksUpdaterHostedService> _logger;

    public TanksUpdaterHostedService(IServiceScopeFactory scopeFactory,
        IHostApplicationLifetime lifetime, 
        ILogger<TanksUpdaterHostedService> logger)
    {
        _scopeFactory = scopeFactory;
        _lifetime = lifetime;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Пробуем стартануть фоновую задачу.");
        if (!await WaitForAppStartup(_lifetime, stoppingToken))
            return;
        _timer = new Timer(UpdateTankers, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(60));
    }

    static async Task<bool> WaitForAppStartup(IHostApplicationLifetime lifetime, CancellationToken stoppingToken)
    {
        // 👇 Создаём TaskCompletionSource для ApplicationStarted
        var startedSource = new TaskCompletionSource();
        using var reg1 = lifetime.ApplicationStarted.Register(() => startedSource.SetResult());

        // 👇 Создаём TaskCompletionSource для stoppingToken
        var cancelledSource = new TaskCompletionSource();
        using var reg2 = stoppingToken.Register(() => cancelledSource.SetResult());

        // Ожидаем любое из событий запуска или запроса на остановку
        Task completedTask = await Task.WhenAny(startedSource.Task, cancelledSource.Task).ConfigureAwait(false);

        // Если завершилась задача ApplicationStarted, возвращаем true, иначе false
        return completedTask == startedSource.Task;
    }

    private async void UpdateTankers(object? state)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var serviceManager = scope.ServiceProvider.GetRequiredService<IServiceManager>();
            _logger.LogInformation("Пробуем обновить резервуары");
            await serviceManager.TankService.UpdateTanksVolume();
        }
    }
}