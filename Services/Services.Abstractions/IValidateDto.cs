namespace Services.Abstractions;

public interface IValidateDto<TDto>
{
    void Validate(TDto dto);
}