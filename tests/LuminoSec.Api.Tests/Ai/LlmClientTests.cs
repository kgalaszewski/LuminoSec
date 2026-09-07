using System.Text;
using Amazon.BedrockRuntime;
using Amazon.BedrockRuntime.Model;
using LuminoSec.Api.Ai;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace LuminoSec.Api.Tests.Ai;

public class LlmClientTests
{
    [Fact]
    public async Task InvokeAsync_ReturnsTextFromBedrockResponse()
    {
        // Arrange
        var bedrockRuntime = Substitute.For<IAmazonBedrockRuntime>();
        var responseJson = """{"content":[{"type":"text","text":"mocked bedrock response"}]}""";
        bedrockRuntime
            .InvokeModelAsync(Arg.Any<InvokeModelRequest>(), Arg.Any<CancellationToken>())
            .Returns(new InvokeModelResponse
            {
                Body = new MemoryStream(Encoding.UTF8.GetBytes(responseJson))
            });
        var sut = new LlmClient(bedrockRuntime, new BedrockOptions(), NullLogger<LlmClient>.Instance);

        // Act
        var result = await sut.InvokeAsync("any prompt");

        // Assert
        Assert.Equal("mocked bedrock response", result);
    }

    [Fact]
    public async Task InvokeAsync_LogsAndRethrowsWhenBedrockCallFails()
    {
        // Arrange
        var bedrockRuntime = Substitute.For<IAmazonBedrockRuntime>();
        bedrockRuntime
            .InvokeModelAsync(Arg.Any<InvokeModelRequest>(), Arg.Any<CancellationToken>())
            .ThrowsAsync(new InvalidOperationException("boom"));
        var sut = new LlmClient(bedrockRuntime, new BedrockOptions(), NullLogger<LlmClient>.Instance);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => sut.InvokeAsync("any prompt"));
    }
}
