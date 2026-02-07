using AutoMapper;
using BusinessManagementAPI.Data;

namespace BusinessManagementAPI.UseCases.Base;

public class BaseHandler
{
    public SQLContext m_Context = default!;
    public IMapper m_Mapper = default!;

    public BaseHandler(IMapper mapper, SQLContext context)
    {
        m_Mapper = mapper;
        m_Context = context;
    }

    /// <summary>
    /// Updates the Property Values of an Entity using reflection. Mainly used to set values of an entity from an API Request
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <param name="request"></param>
    /// <param name="ignoredProperties"></param>
    /// <returns name="entity"></returns>
    /// <exception cref="ArgumentException"></exception>
    public T UpdateEntityFromRequest<T>(T entity, object request, List<string> ignoredProperties = null!)
    {
        if (entity is null || request is null)
            throw new ArgumentException("entity or propertyValues cannot be null.");

        var _PropertiesToIgnore = ignoredProperties ?? [];

        var _EntityProperties = entity.GetType().GetProperties();
        var _NewPropertyValues = request.GetType().GetProperties();

        foreach (var property in _NewPropertyValues)
        {
            var targetProperty = _EntityProperties.FirstOrDefault(p =>
                p.Name == property.Name &&
                p.PropertyType == property.PropertyType &&
                p.CanWrite &&
                !_PropertiesToIgnore.Any(i => i == p.Name));

            if (targetProperty is not null)
            {
                var value = property.GetValue(request);
                targetProperty.SetValue(entity, value);
            }
        }

        return entity;
    }
}
