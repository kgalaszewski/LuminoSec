using LuminoSec.Api.Rules;

namespace LuminoSec.Api.Tests.Rules;

public class RulesEngineTests
{
    [Fact]
    public void Evaluate_ReturnsAtLeastOneFinding()
    {
        // Arrange
        var sut = new RulesEngine();

        // Act
        var findings = sut.Evaluate("any architecture description");

        // Assert
        Assert.NotEmpty(findings);
    }
}
