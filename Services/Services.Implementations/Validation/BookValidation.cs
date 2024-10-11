using System.Linq;
using Domain.Entities;
using Services.Contracts;

namespace Services.Implementations;

/// <summary>
/// Проверка правильности заполнения dto книги 
/// </summary>
public class BookValidation : ValidationBase<BookDto>
{
    protected override void CheckErrors(BookDto dto)
    {
        if (DomainConstraints.BookDescriptionIsRequired)
            CheckRequired(dto.Description, $"Поле {nameof(BookDto.Description)} не заполнено");
        if (DomainConstraints.BookTitleIsRequired)
            CheckRequired(dto.Title, $"Поле {nameof(BookDto.Title)} не заполнено");
        if (DomainConstraints.BookMainAuthorIdIsRequired)
            CheckRequired(dto.MainAuthor.Id, $"Поле {nameof(BookDto.MainAuthor.Id)} не заполнено");
        CheckStringLength(dto.Title, DomainConstraints.BookTitleMaxLength,
            $"Превышена длина поля {nameof(Book.Title)}");
        CheckStringLength(dto.Description, DomainConstraints.BookDescriptionMaxLength,
            $"Превышена длина поля {nameof(Book.Description)}");
        if (dto.CoAuthors.Select(x => x.Id).Contains(dto.MainAuthor.Id))
            AddError("Автор указан как соавтор");
    }
}