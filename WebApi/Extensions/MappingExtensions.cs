using System;
using System.Linq.Expressions;
using AutoMapper;

namespace WebApi.Extensions;

public static class MappingExtensions
{
    public static IMappingExpression<TFrom, TTo> ForMemberTrim<TFrom, TTo>(
        this IMappingExpression<TFrom, TTo> expression,
        Expression<Func<TTo, string>> from,
        Expression<Func<TFrom, string>> to)
    {
        var methodCallExpression =
            Expression.Lambda<Func<TFrom, string>>(Expression.Call(to.Body, typeof(string).GetMethod("Trim", [])!),
                to.Parameters);
        return expression.ForMember(from,
            configurationExpression => configurationExpression.MapFrom(methodCallExpression));
    }
}