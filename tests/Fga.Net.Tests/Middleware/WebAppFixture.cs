﻿using System.Threading;
using System.Threading.Tasks;
using Alba;
using Fga.Net.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using OpenFga.Sdk.Client.Model;
using OpenFga.Sdk.Model;
using Xunit;

namespace Fga.Net.Tests.Middleware;

public class WebAppFixture : IAsyncLifetime
{
    public IAlbaHost AlbaHost = null!;

    public async Task InitializeAsync()
    {
        var authorizationClientMock = new Mock<IFgaCheckDecorator>();

        authorizationClientMock.Setup(c =>
            c.Check(It.IsAny<ClientCheckRequest>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((ClientCheckRequest res, CancellationToken _) => 
                res.User == MockJwtConfiguration.DefaultUser 
                    ? new CheckResponse() { Allowed = true } 
                    : new CheckResponse() { Allowed = false });


        AlbaHost = await Alba.AlbaHost.For<Program>(builder =>
        {
            builder.ConfigureServices(s =>
            {
                s.Replace(ServiceDescriptor.Scoped<IFgaCheckDecorator>(_ => authorizationClientMock.Object));
            });

        }, MockJwtConfiguration.GetDefaultStubConfiguration());
    }

    public async Task DisposeAsync()
    {
        if (AlbaHost is not null)
            await AlbaHost.DisposeAsync();
    }
}

[CollectionDefinition(nameof(WebAppCollection))]
public class WebAppCollection : ICollectionFixture<WebAppFixture>
{
}