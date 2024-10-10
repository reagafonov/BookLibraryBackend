using System.Collections.Generic;
using System.Linq;
using Services.Abstractions;
using Services.Abstractions.Exceptions;

namespace Services.Implementations;

public abstract class ValidationBase<TDto> : IValidateDto<TDto>
{
    private readonly ICollection<string> _errors = new HashSet<string>();

    public bool HasErrors => _errors.Any();

    public IEnumerable<string> Errors => _errors;

    public void Validate(TDto dto)
    {
        CheckErrors(dto);
        if (HasErrors)
            throw new DtoValidationException(string.Join('\n', _errors));
    }

    private static string GetRequiredErrorString(string fieldName)
        => $"Поле {fieldName} не установлено";

    private static string GetMaxLengthErrorString(string fieldName, int maxLength)
        => $"Превышена максимальная длина {maxLength} поля {fieldName}";

    void AddError(string error) => _errors.Add(error);

    protected void CheckRequired<TData>(TData data, string fieldName)
    {
        if (data == null)
            AddError(GetRequiredErrorString(fieldName));
        if (data is string s && string.IsNullOrWhiteSpace(s))
            AddError(GetRequiredErrorString(fieldName));
    }

    protected void CheckStringLength(string data, int maxLength, string fieldName)
    {
        if (data.Length > maxLength)
            AddError(GetMaxLengthErrorString(fieldName, maxLength));
    }

    protected abstract void CheckErrors(TDto dto);
}