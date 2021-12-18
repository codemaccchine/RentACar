using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants.Messages
{
    public static class ErrorMessages
    {
        public static string CarDailyPriceInvalid = "Araba kiralama fiyatı 0'dan büyük olmalıdır.";
        internal static string ImageLimitExceded;
        internal static string RentalReturnDateNotNull;
        internal static string RentalReturnDateNextTime;
        public static readonly string MaintenanceTime = "Sistem bakımda";
        public static readonly string CarDescriptionInvalid = "Araba model bilgisi geçersiz";
        public static readonly string BrandNameInvalid = "Marka adı geçersiz";
        public static readonly string ColorNameInvalid = "Renk adı geçersiz";
        public static readonly string UserNameInvalid = "Kullanıcı adı geçersiz";
        public static readonly string CustomerNameInvalid = "Müşteri adı geçersiz";
        public static readonly string RentalNameInvalid = "Araç kiralama bilgisi geçersiz";
        public static readonly string CarCountOfBrandCorrect = "Araç eklenemiyor. Markada bulunan araç sayısı limitine ulaşıldı";
        public static readonly string BrandLimitExceded = "Araç eklenemiyor. Marka sayısı limitine ulaşıldı";
        public static readonly string ImageNotFound = "Resim bulunamadı";
        public static readonly string CarImageLimitExceded = "Araba için daha fazla resim yüklenemiyor";
    }
}
