class AppConfig {
  static const String apiUrl = 'https://scanandgoapi.azurewebsites.net/api';

  static const String userRegistrationEndpoint = '$apiUrl/BuyerAuth/registration';
  static const String userVerificationEndpoint = '$apiUrl/BuyerAuth/verification';
  static const String userResendVerificationEmailEndpoint = '$apiUrl/BuyerAuth/resend-verification-email';
  static const String userCompleteProfileEndpoint = '$apiUrl/BuyerAuth/complete-profile';
  static const String userLoginEndpoint = '$apiUrl/BuyerAuth/login';
  static const String userForgotPasswordEndpoint = '$apiUrl/BuyerAuth/forgot-password';
  static const String userSendResetEmailEndpoint = '$apiUrl/BuyerAuth/send-reset-email';
  static const String userResetPasswordEndpoint = '$apiUrl/BuyerAuth/reset-password';


  static const String storeGetStoresByLocationEndpoint = '$apiUrl/BuyerStore/get-stores-by-location';
  static const String storeGetAllStoresEndpoint = '$apiUrl/BuyerStore/buyer-get-all-stores';


  static const String shoppingCartGetShoppingCartEndpoint = '$apiUrl/BuyerShoppingCart/get_shopping_cart';
  static const String shoppingCartAddToCartEndpoint = '$apiUrl/BuyerShoppingCart/add-to-cart';
  static const String shoppingCartIncreaseQuantityEndpoint = '$apiUrl/BuyerShoppingCart/increase-quantity';
  static const String shoppingCartDecreaseQuantityEndpoint = '$apiUrl/BuyerShoppingCart/decrease-quantity';
  static const String shoppingCartRemoveFromCartEndpoint = '$apiUrl/BuyerShoppingCart/remove-from-cart';


  static const String orderGetAllBuyerOrdersEndpoint = '$apiUrl/BuyerOrder/';
  static const String orderGetOrderByIdEndpoint = '$apiUrl/BuyerOrder/';
  static const String orderPayOrderEndpoint = '$apiUrl/BuyerOrder/pay-order';
  static const String orderPaymentConfirmationEndpoint = '$apiUrl/BuyerOrder/paymentConfirmation';
  static const String orderWeightEndpoint = '$apiUrl/BuyerOrder/weight';

}