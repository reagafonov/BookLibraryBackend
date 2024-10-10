using System;

namespace Domain.Entities.Exceptions;

public class CRUDUpdateException(string message) : Exception(message);