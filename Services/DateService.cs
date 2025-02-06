using System.Globalization;

namespace lab1_nour_kassem.Services
{
    public class DateService
    {
        public string getFormattedDate(string language)
        {
            if (string.IsNullOrEmpty(language) || language == "*")
            {
                language = "en-US"; // Default language
            }

            //however if i leave it empty, it autofills with a bunch of languages and q's and so my code will
            //think it is ivalid. so we try to split and take the first language here:
            else
            {
                language = language.Split(',')[0].Trim();
            }

            bool isValid = CultureInfo.GetCultures(CultureTypes.AllCultures)
                .Any(c => c.Name.Equals(language, StringComparison.Ordinal));
            //OH SO THAT'S HOW YOU MAKE IT CASE INSENSITIVE! nice :)

            if (!isValid)
            {
                throw new CultureNotFoundException($"Invalid language code");
            }

            var culture = new CultureInfo(language);
            var formattedDate = DateTime.Now.ToString("D", culture);

            return formattedDate;
        }
    }
}