using LuminoSec.Api.Ai;

namespace LuminoSec.Api.Tests.Ai;

public class LlmClientTests
{
    [Fact]
    public async Task InvokeAsync_ReturnsNonEmptyMockResponse()
    {
        // Arrange
        var sut = new LlmClient();

        // Act
        var result = await sut.InvokeAsync("any prompt");

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(result));
    }
}
