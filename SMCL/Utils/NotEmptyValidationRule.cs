﻿using System.Globalization;
using System.Windows.Controls;

namespace SMCL.Utils
{
    public class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return string.IsNullOrWhiteSpace((value ?? "").ToString())
                ? new ValidationResult(false, null)
                : ValidationResult.ValidResult;
        }
    }
}