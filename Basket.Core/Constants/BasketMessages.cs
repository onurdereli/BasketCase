namespace Basket.Core.Constants
{
    public class BasketMessages
    {
        public const string UserIdValidationMessage = "Lütfen kullanıcı girişi yapınız.";

        public const string BasketItemsValidationMessage = "Lütfen sepetinize ürün ekleyiniz.";

        public const string PriceValidationMessage = "Fiyat alanı boş olamaz.";

        public const string ProductCodeValidationMessage = "Ürün kodu boş olamaz.";

        public const string QuantityValidationMessage = "Ürün adeti boş olamaz.";

        public const string ProductHaveStockMessage = "There is not enough stock.";

        public const string BasketNotFoundMessage = "Basket not found";

        public const string BasketCouldNotUpdateOrSaveMessage = "Basket could not update or save";

        public const string ProductCouldNotRemoveMessage = "Product could not remove from basket";

        public const string NotFoundProductByProductCodeMessage = "No product was found in the basket according to the product code";

        public const string NotFoundBasketForUpdateMessage = "The product to be updated was not found in the basket";

        public const string NotUpdateBasketMessage = "Could not update to basket";

        public const string NotAddBasketMessage = "Could not add to basket";
    }
}
