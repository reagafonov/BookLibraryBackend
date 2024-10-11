using System;

namespace Services.Abstractions.Exceptions;

public class DtoValidationException(string message) : Exception(message);