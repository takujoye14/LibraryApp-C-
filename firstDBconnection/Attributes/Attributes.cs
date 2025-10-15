using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace firstDBconnection.Attributes
{
    public class CurrentYearAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is int year)
            {
                return year <= DateTime.Now.Year;
            }
            return true;
        }
    }

    public class NoSpecialCharactersAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is string str)
            {
                return Regex.IsMatch(str, @"^[a-zA-Z0-9\s]*$");
            }
            return true;
        }
    }

    public class MaximumWordsAttribute : ValidationAttribute
    {
        private readonly int _maxWords;

        public MaximumWordsAttribute(int maxWords)
        {
            _maxWords = maxWords;
        }

        public override bool IsValid(object? value)
        {
            if (value is string str)
            {
                int wordCount = str.Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
                return wordCount <= _maxWords;
            }
            return true;
        }
    }

    public class NotEqualToAttribute : ValidationAttribute
    {
        private readonly string _otherPropertyName;

        public NotEqualToAttribute(string otherPropertyName)
        {
            _otherPropertyName = otherPropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var otherProperty = validationContext.ObjectType.GetProperty(_otherPropertyName);
            if (otherProperty != null)
            {
                var otherValue = otherProperty.GetValue(validationContext.ObjectInstance);
                if (Equals(value, otherValue))
                {
                    return new ValidationResult(ErrorMessage ?? $"This field cannot be the same as {_otherPropertyName}.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
