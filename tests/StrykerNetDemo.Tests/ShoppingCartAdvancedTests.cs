using AutoFixture.Xunit2;
using FluentAssertions;

namespace StrykerNetDemo.Tests;

[Trait("Category", "advanced")]
public class ShoppingCartAdvancedTests
{
    #region Methods

    #region Public Methods

    [Theory]
    [AutoData]
    public void AddItem_IfItemDoesExist_ShouldIncreaseAmount(
        Guid productId,
        int amount,
        ShoppingCart sut)
    {
        // Arrange
        sut.AddItem(productId, amount);

        // Act
        sut.AddItem(productId, 1);

        // Assert
        sut.Items[productId].Should().Be(amount + 1);
    }

    [Theory]
    [AutoData]
    public void AddItem_IfItemDoesNotExist_ShouldAddItem(
        Guid productId,
        int amount,
        ShoppingCart sut)
    {
        // Act
        sut.AddItem(productId, amount);

        // Assert
        sut.Items[productId].Should().Be(amount);
    }

    [Theory]
    [AutoData]
    public void Ctor_ShouldReturnNewInstance(
        Guid shoppingCartId,
        Guid customerId)
    {
        // Act
        var result = new ShoppingCart(shoppingCartId, customerId);

        // Assert
        result.Id.Should().NotBeEmpty();
        result.CustomerId.Should().NotBeEmpty();
    }

    [Theory]
    [InlineAutoData(5, false)]
    [InlineAutoData(10, true)]
    public void IsEligibleForDiscount_IfShoppingCartContainsMoreThanOneItem(
    int amount,
    bool expected,
    Guid productId,
    Guid otherProductId,
    ShoppingCart sut)
    {
        // Arrange
        sut.AddItem(productId, amount);
        sut.AddItem(otherProductId, 1);

        // Act
        var result = sut.IsEligibleForDiscount();

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [AutoData]
    public void RemoveItem_IfItemDoesExist_ShouldUpdateAmount(
        Guid productId,
        int amount,
        ShoppingCart sut)
    {
        // Arrange
        sut.AddItem(productId, amount);

        // Act
        sut.RemoveItem(productId, 1);

        // Assert
        sut.Items[productId].Should().Be(amount - 1);
    }

    #endregion Public Methods

    #endregion Methods
}