using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Semp.Infrastructure;
using Xunit;

public class CompatibilityTests
{
    [Fact]
    public async Task NotificationsAwaitEachHandlerBeforeStartingTheNext()
    {
        var firstStarted = new TaskCompletionSource<bool>();
        var releaseFirst = new TaskCompletionSource<bool>();
        var events = new List<string>();
        INotificationHandler<Notification>[] handlers =
        {
            new Handler(async () => {
                events.Add("first-start");
                firstStarted.SetResult(true);
                await releaseFirst.Task;
                events.Add("first-end");
            }),
            new Handler(() => { events.Add("second"); return Task.CompletedTask; })
        };
        var mediator = new SequentialMediator(type =>
            type == typeof(IEnumerable<INotificationHandler<Notification>>) ? handlers : null);

        var publication = mediator.Publish(new Notification());
        await firstStarted.Task.WaitAsync(TimeSpan.FromSeconds(5));
        Assert.Equal(new[] { "first-start" }, events);
        releaseFirst.SetResult(true);
        await publication.WaitAsync(TimeSpan.FromSeconds(5));
        Assert.Equal(new[] { "first-start", "first-end", "second" }, events);
    }

    [Fact]
    public void AutofacPopulationPreservesScopedServicesAndDisposal()
    {
        var services = new ServiceCollection();
        services.AddScoped<ScopedService>();
        var builder = new ContainerBuilder();
        builder.Populate(services);
        using var container = builder.Build();
        var provider = container.Resolve<IServiceProvider>();
        ScopedService first;
        using (var scope = provider.CreateScope())
        {
            first = scope.ServiceProvider.GetRequiredService<ScopedService>();
            Assert.Same(first, scope.ServiceProvider.GetRequiredService<ScopedService>());
            using var other = provider.CreateScope();
            Assert.NotSame(first, other.ServiceProvider.GetRequiredService<ScopedService>());
        }
        Assert.True(first.Disposed);
    }

    private sealed class Notification : INotification { }
    private sealed class Handler(Func<Task> callback) : INotificationHandler<Notification>
    {
        public Task Handle(Notification notification, CancellationToken cancellationToken) => callback();
    }
    public sealed class ScopedService : IDisposable
    {
        public bool Disposed { get; private set; }
        public void Dispose() => Disposed = true;
    }
}
