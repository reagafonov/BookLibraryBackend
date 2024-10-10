using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using WebApi.Models;

namespace WebApi.Validation;

/// <summary>
/// Проверяет, что фамилия и имя автора заполнены на одном языке.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class AuthorNameLanguageValidationAttribute : ValidationAttribute
{
    /// <summary>
    /// Часто повторяемая операция возврата ошибки
    /// Устанавливает текст ошибки и возвращает ложь
    /// </summary>
    /// <param name="errorString">Уникальная строка текста ошибки для шаблона</param>
    /// <returns>Ложь</returns>
    private bool Error(string errorString)
    {
        ErrorMessage = $"Ошибка. {errorString}";
        return false;
    }

    /// <summary>
    /// Проверяет является ли строка текстом на английском языке
    /// </summary>
    /// <param name="data">текст</param>
    /// <returns>Истина, если текст на английском</returns>
    private bool IsEnglish(string data) => Regex.Match(data, @"^[\p{IsBasicLatin}\p{P}\s]+$").Success;

    /// <summary>
    /// Проверяет является ли строка текстом на русском языке
    /// </summary>
    /// <param name="data">текст</param>
    /// <returns>Истина, если текст на русском</returns>
    private bool IsRussian(string data) => Regex.Match(data, @"^[\p{IsCyrillic}\p{P}\s]+$").Success;

    private LanguageEnum? GetLanguage(string data)
    {
        if (IsEnglish(data))
            return LanguageEnum.English;
        if (IsRussian(data))
            return LanguageEnum.Russian;
        return null;
    }

    /// <summary>
    /// Перегруженная операция валидации
    /// </summary>
    /// <param name="value">Переданный объект класса, для которого установлен атрибут</param>
    /// <returns>Истина, если объект валиден, для этого атрибута</returns>
    public override bool IsValid(object value)
    {
        if (value is AuthorInputModel model)
        {
            var firstName = model.FirstName;
            var lastName = model.LastName;
            var firstNameLanguage = GetLanguage(firstName);
            if (firstNameLanguage is null)
            {
                return Error("Не распознан язык имени");
            }

            var lastNameLanguage = GetLanguage(lastName);
            if (lastNameLanguage is null)
            {
                return Error("Не распознан язык фамилии");
            }

            if (firstNameLanguage != lastNameLanguage)
            {
                return Error($"Ошибка. Разные языки имени ({firstNameLanguage}) и фамилии ({lastNameLanguage})");
            }

            return true;
        }

        return base.IsValid(value);
    }
}