using NCrontab;
using Semp.Infrastructure.Threading;
using Xunit;

public class SchedulingCompatibilityTests
{
    [Theory]
    [InlineData("2026-11-02T12:00:14", "2026-11-02T12:00:15")]
    [InlineData("2026-11-02T23:59:59", "2026-11-03T00:00:00")]
    public void SixFieldCronPreservesSecondsAndDayRollover(string current, string expected)
    {
        var schedule = CrontabSchedule.Parse("*/15 * * * * *",
            new CrontabSchedule.ParseOptions { IncludingSeconds = true });
        Assert.Equal(DateTime.Parse(expected), schedule.GetNextOccurrence(DateTime.Parse(current)));
    }

    [Fact]
    public void AsyncHelperWaitsForAsynchronousResults()
    {
        Assert.Equal(42, AsyncHelper.RunSync(async () =>
        {
            await Task.Yield();
            return 42;
        }));
        var completed = false;
        AsyncHelper.RunSync(async () =>
        {
            await Task.Yield();
            completed = true;
        });
        Assert.True(completed);
    }

    [Fact]
    public void AsyncHelperPreservesOriginalExceptions()
    {
        var expected = new InvalidOperationException("test failure");
        var actual = Assert.Throws<InvalidOperationException>(() => AsyncHelper.RunSync(async () =>
        {
            await Task.Yield();
            throw expected;
        }));
        Assert.Same(expected, actual);
    }
}
