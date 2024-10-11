using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Domain.Entities;
using Services.Repositories.Abstractions;
using WebApi.Models;

namespace WebApi.Validation;

/// <summary>
/// Проверяет, что фамилия и имя автора заполнены на одном языке.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public partial class AuthorNameLanguageValidationAttribute : ValidationAttribute
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
    private static bool IsEnglish(string data) => IsEnglishRegex().Match(data).Success;

    /// <summary>
    /// Проверяет является ли строка текстом на русском языке
    /// </summary>
    /// <param name="data">текст</param>
    /// <returns>Истина, если текст на русском</returns>
    private static bool IsRussian(string data) => IsRussianRegex().Match(data).Success;

    private static LanguageEnum? GetLanguage(string? data)
    {
        if (string.IsNullOrWhiteSpace(data))
            return null;
       
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
    public override bool IsValid(object? value)
    {
        if (!DomainConstraints.IsCheckForSameLanguageFirstNameAndLastName)
            return true;
        
        if (value is not AuthorInputModel model) return base.IsValid(value);
        
        var firstName = model.FirstName;
        var lastName = model.LastName;
        var firstNameLanguage = GetLanguage(firstName);
        if (firstNameLanguage is null)
        {
            return Error("Не распознан язык имени");
        }

        if (!string.IsNullOrWhiteSpace(lastName))
        {
            var lastNameLanguage = GetLanguage(lastName);
            if (lastNameLanguage is null)
            {
                return Error("Не распознан язык фамилии");
            }

            if (firstNameLanguage != lastNameLanguage)
            {
                return Error($"Разные языки имени ({firstNameLanguage}) и фамилии ({lastNameLanguage})");
            }
        }

        return true;

    }

    [GeneratedRegex(@"^[\p{IsBasicLatin}\p{P}\d\s ]+$")]
    private static partial Regex IsEnglishRegex();
    [GeneratedRegex(@"^[\p{IsCyrillic}\p{P}\d\s ]+$")]
    private static partial Regex IsRussianRegex();
}