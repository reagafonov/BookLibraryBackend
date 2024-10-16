﻿using System.ComponentModel.DataAnnotations;
using WebApi.Validation;

namespace WebApi.Models;

/// <summary>
/// Модель автора
/// </summary>
[AuthorNameLanguageValidation]
public record AuthorInputModel
{
    /// <summary>
    /// Фамилия автора
    /// </summary>
    [MaxLength(500, ErrorMessage = "Длина имени должна быть меньше 500 символов")]
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string? LastName { get; set; }

    /// <summary>
    /// Имя автора
    /// </summary>
    [MaxLength(500, ErrorMessage = "Длина фамилии должна быть меньше 500 символов")]
    [Required]
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string? FirstName { get; set; }
}