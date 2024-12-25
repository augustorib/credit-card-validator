using System.Text.RegularExpressions;

namespace credit_card_validator.utils
{
    public static class CreditCardValidator
    {
        public static string GetCardBrand(string cardNumber)
        {
            if (!IsValidCreditCard(cardNumber))
                return "Invalid card number";

            // Remove any non-digit characters
            cardNumber = new string(cardNumber.Where(char.IsDigit).ToArray());

            // Define regex patterns for known card brands
            var patterns = new (string Brand, string Pattern)[]
            {
                ("Visa", @"^4[0-9]{12}(?:[0-9]{3})?$"),
                ("MasterCard", @"^5[1-5][0-9]{14}$"),
                ("American Express", @"^3[47][0-9]{13}$"),
                ("Discover", @"^6(?:011|5[0-9]{2})[0-9]{12}$"),
                ("JCB", @"^(?:2131|1800|35\d{3})\d{11}$"),
                ("Voyager", @"^8699[0-9]{11}$"),
                ("Diners Club", @"^3(?:0[0-5]|[68][0-9])[0-9]{11}$"),
                ("EnRoute", @"^(2014|2149)\d{11}$"),
                ("Aura", @"^50[0-9]{14,17}$")
            };

            // Match card number with patterns
            foreach (var (Brand, Pattern) in patterns)
            {
                if (Regex.IsMatch(cardNumber, Pattern))
                {
                    return Brand;
                }
            }

            return "Unknown";
        }


        //Validate credot card using Luhn algorithm
        private static bool IsValidCreditCard(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber))
                return false;

            // Remove any non-digit characters
            cardNumber = new string(cardNumber.Where(char.IsDigit).ToArray());

            int sum = 0;
            bool alternate = false;

            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int n = int.Parse(cardNumber[i].ToString());

                if (alternate)
                {
                    n *= 2;
                    if (n > 9)
                    {
                        n -= 9;
                    }
                }

                sum += n;
                alternate = !alternate;
            }

            return sum % 10 == 0;
        }
    }
}