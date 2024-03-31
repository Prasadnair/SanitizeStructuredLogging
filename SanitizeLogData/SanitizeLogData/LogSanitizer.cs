using System.Collections;

namespace SanitizeLogData
{
    public static class LogSanitizer
    {
        public static object SanitizeSensitiveData(object input)
        {
            if (input == null)
                return input;

            // Check if the input is a string and sanitize it
            if (input is string str)
            {
                return SanitizeString(str);
            }

            // Check if the input is a primitive type and return as is
            if (input.GetType().IsPrimitive || input is decimal || input is Enum)
            {
                return input;
            }

            // Check if the input is a collection and sanitize each element
            if (input is IEnumerable enumerable)
            {
                var sanitizedCollection = enumerable.Cast<object>().Select(SanitizeSensitiveData).ToList();
                return sanitizedCollection;
            }

            // For complex objects, recursively sanitize each property
            var objectType = input.GetType();
            var sanitizedObject = Activator.CreateInstance(objectType);

            foreach (var property in objectType.GetProperties())
            {
                var value = property.GetValue(input);
                var sanitizedValue = SanitizeSensitiveData(value);
                property.SetValue(sanitizedObject, sanitizedValue);
            }

            return sanitizedObject;
        }

        private static string SanitizeString(string input)
        {
            // Replace sensitive data with asterisks
            return input
                .Replace("password", "*****")
                .Replace("token", "*****")
                .Replace("ssn", "*****");
        }
    }
}
