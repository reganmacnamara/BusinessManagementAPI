using AutoMapper;
using BusinessManagementAPI.Data;

namespace BusinessManagementAPI.UseCases.Base;

public class BaseHandler(IMapper mapper, SQLContext context)
{
    public SQLContext m_Context = context;
    public IMapper m_Mapper = mapper;

    /// <summary>
    /// Updates the Property Values of an Entity using reflection. Mainly used to set values of an entity from an API Request
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="Exception"></exception>
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
                //p.PropertyType == property.PropertyType &&
                p.CanWrite &&
                !_PropertiesToIgnore.Any(i => i == p.Name));

            if (targetProperty is not null)
            {
                if (targetProperty.PropertyType != property.PropertyType)
                    throw new Exception($"The Property {targetProperty.Name} in {nameof(entity)} and {nameof(request)} do not match.");

                var value = property.GetValue(request);
                targetProperty.SetValue(entity, value);
            }
        }

        return entity;
    }
}
