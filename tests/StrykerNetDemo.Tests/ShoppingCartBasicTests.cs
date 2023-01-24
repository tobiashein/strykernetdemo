using AutoFixture.Xunit2;
using FluentAssertions;

namespace StrykerNetDemo.Tests;

[Trait("Category", "basic")]
public class ShoppingCartBasicTests
{
    #region Methods

    #region Public Methods

    [Theory]
    [AutoData]
    public void AddItem_IfItemDoesExist_ShouldReturnTrue(
        Guid productId,
        int amount,
        ShoppingCart sut)
    {
        // Arrange
        sut.AddItem(productId, amount);

        // Act
        var result = sut.AddItem(productId, 1);

        // Assert
        result.Should().BeTrue();
    }

    [Theory]
    [AutoData]
    public void AddItem_IfItemDoesNotExist_ShouldReturnTrue(
        Guid productId,
        int amount,
        ShoppingCart sut)
    {
        // Act
        var result = sut.AddItem(productId, amount);

        // Assert
        result.Should().BeTrue();
    }

    [Theory]
    [AutoData]
    public void ClearItems_ShouldClearItems(
        Guid[] productIds,
        ShoppingCart sut)
    {
        // Arrange
        foreach (var productId in productIds)
            sut.AddItem(productId, 1);

        // Act
        sut.ClearItems();

        // Assert
        sut.Items.Should().BeEmpty();
    }

    [Theory]
    [AutoData]
    public void Ctor_ShouldNotThrow(
        Guid shoppingCartId,
        Guid customerId)
    {
        // Act
        Func<ShoppingCart> act = () => new ShoppingCart(shoppingCartId, customerId);

        // Assert
        act.Should().NotThrow();
    }

    [Theory]
    [AutoData]
    public void GetItemAmount_IfItemDoesExist_ShouldReturnItemAmount(
        Guid productId,
        int amount,
        ShoppingCart sut)
    {
        // Arrange
        sut.AddItem(productId, amount);

        // Act
        var result = sut.GetItemAmount(productId);

        // Assert
        result.Should().Be(amount);
    }

    [Theory]
    [AutoData]
    public void GetItemAmount_IfItemDoesNotExist_ShouldReturnZero(
        Guid productId,
        ShoppingCart sut)
    {
        // Act
        var result = sut.GetItemAmount(productId);

        // Assert
        result.Should().Be(0);
    }

    [Theory]
    [AutoData]
    public void GetTotalPrice_ShouldReturnTotalPriceOfAllItems(
        Guid[] productIds,
        ShoppingCart sut)
    {
        // Arrange
        foreach (var productId in productIds)
            sut.AddItem(productId, 1);

        // Act
        var result = sut.GetTotalPrice();

        // Assert
        result.Should().BeGreaterThan(0);
    }

    [Theory]
    [InlineAutoData(5, false)]
    [InlineAutoData(10, true)]
    public void IsEligibleForDiscount(
        int amount,
        bool expected,
        Guid productId,
        ShoppingCart sut)
    {
        // Arrange
        sut.AddItem(productId, amount);

        // Act
        var result = sut.IsEligibleForDiscount();

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [AutoData]
    public void RemoveItem_IfItemDoesExist_AndAmountIsLessThanZero_ShouldRemoveItem(
        Guid productId,
        int amount,
        ShoppingCart sut)
    {
        // Arrange
        sut.AddItem(productId, amount);

        // Act
        sut.RemoveItem(productId, amount + 1);

        // Assert
        sut.Items.Should().BeEmpty();
    }

    [Theory]
    [AutoData]
    public void RemoveItem_IfItemDoesExist_AndAmountIsZero_ShouldRemoveItem(
        Guid productId,
        int amount,
        ShoppingCart sut)
    {
        // Arrange
        sut.AddItem(productId, amount);

        // Act
        sut.RemoveItem(productId, amount);

        // Assert
        sut.Items.Should().BeEmpty();
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
        sut.Items[productId].Should().NotBe(amount);
    }

    [Theory]
    [AutoData]
    public void RemoveItem_IfItemDoesNotExist_ShouldReturn(
        Guid productId,
        int amount,
        ShoppingCart sut)
    {
        // Act
        sut.RemoveItem(productId, amount);

        // Assert
        sut.Items.Should().BeEmpty();
    }

    #endregion Public Methods

    #endregion Methods
}