using Domain.Entities;
using Services.Contracts;

namespace Services.Implementations.Validation;

public class AuthorValidate : ValidationBase<AuthorDto>
{
    protected override void CheckErrors(AuthorDto dto)
    {
        if (DomainConstraints.AuthorFirstNameIsRequired)
            CheckRequired(dto.FirstName, nameof(dto.FirstName));
        if (DomainConstraints.AuthorLastNameIsRequired)
            CheckRequired(dto.LastName, nameof(dto.LastName));
        CheckStringLength(dto.FirstName, DomainConstraints.AuthorFirstNameMaxLength, nameof(dto.FirstName));
        CheckStringLength(dto.LastName, DomainConstraints.AuthorLastNameMaxLength, nameof(dto.LastName));
    }
}