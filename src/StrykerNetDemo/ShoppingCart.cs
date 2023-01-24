namespace StrykerNetDemo
{
    public class ShoppingCart
    {
        #region Constructors

        public ShoppingCart(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
        }

        #endregion Constructors

        #region Fields

        private readonly Dictionary<Guid, int> _items = new();

        #endregion Fields

        #region Properties

        public Guid CustomerId { get; init; }

        public Guid Id { get; init; }

        public IReadOnlyDictionary<Guid, int> Items => _items;

        #endregion Properties

        #region Methods

        #region Public Methods

        public bool AddItem(Guid productId, int amount)
        {
            if (_items.ContainsKey(productId))
            {
                _items[productId] += amount;
            }
            else
            {
                _items.Add(productId, amount);
            }

            return true;
        }

        public void ClearItems() => _items.Clear();

        public int GetItemAmount(Guid productId) => _items.TryGetValue(productId, out int itemAmount)
            ? itemAmount
            : 0;

        public float GetTotalPrice()
        {
            float totalPrice = 0f;

            foreach (var itemAmount in _items.Values)
                totalPrice += itemAmount * Random.Shared.Next(1, 100);

            return totalPrice;
        }

        public bool IsEligibleForDiscount() => _items.Any(item => item.Value >= 10);

        public void RemoveItem(Guid productId, int amount)
        {
            if (!_items.TryGetValue(productId, out var itemAmount))
                return;

            if (itemAmount > amount)
            {
                _items[productId] -= amount;
            }
            else
            {
                _items.Remove(productId);
            }
        }

        #endregion Public Methods

        #endregion Methods
    }
}