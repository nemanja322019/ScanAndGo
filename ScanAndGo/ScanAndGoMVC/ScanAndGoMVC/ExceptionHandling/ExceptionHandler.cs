using System.Net;
using ModelsLibrary.Exceptions.User;
using ModelsLibrary.Exceptions.Shared;
using ModelsLibrary.Exceptions.Order;
using ModelsLibrary.Exceptions.Store;
using ModelsLibrary.Exceptions.Auth;
using ModelsLibrary.Exceptions.ShoppingCart;
using ModelsLibrary.Exceptions.QRCode;
using ModelsLibrary.Exceptions.Product;

namespace ScanAndGoMVC.ExceptionHandling
{
    public static class ExceptionHandler
    {
        public static int GetStatusCode(this BaseException exception)
        {
            var code = exception switch
            {
                UserNotFoundException or
                ProductNotFoundException or 
                OrderNotFoundException or
                StoreNotFoundException or 
                ShoppingCartNotFoundException or
                ShoppingCartItemNotFoundException => HttpStatusCode.NotFound,
                InvalidVerificationCodeException or
                InvalidResetLinkException or
                ResetPasswordTokenExpiredException => HttpStatusCode.NotFound,
                UserWithSameEmailAlreadyExistsException or 
                ProductWithSameBarcodeAlreadyExistsException => HttpStatusCode.Conflict,
                WrongPasswordException => HttpStatusCode.Unauthorized,
                UserDoesntHaveAccessToTheSystemException or
                TemporaryPasswordExpiredException => HttpStatusCode.Forbidden,
                FieldsRequiredException or
                InvalidQRCodeException => HttpStatusCode.BadRequest,
            };
            return (int)code;
        }
    }
}
