using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StudyDotNet.Application;

namespace StudyDotNet.UnitTests;

public sealed class ApplicationDependencyInjectionTests
{
    [Fact]
    public void AddApplication_RegistersMediator()
    {
        var services = new ServiceCollection();

        services.AddApplication();

        Assert.Contains(services, service => service.ServiceType == typeof(IMediator));
    }
}
