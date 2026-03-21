namespace MacsBusinessManagementAPI.Extensions
{

    public static class ClassExtensions
    {
        /// <summary>
        /// Method used to update one Entity's values from those of another Entity of a different type based on Property Names
        /// </summary>
        /// <typeparam name="T">Type of Entity being Updated</typeparam>
        /// <param name="updatingEntity">The Entity used to update the Entity</param>
        /// <param name="ignoredProperties">Names of any properties to be ignored</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public static void UpdateFromEntity<T>(this T entity, object updatingEntity, List<string> ignoredProperties = null!) where T : class
        {
            if (entity is null || updatingEntity is null)
                throw new ArgumentException("entity or propertyValues cannot be null.");

            var _PropertiesToIgnore = ignoredProperties ?? [];

            var _EntityProperties = entity.GetType().GetProperties();
            var _NewPropertyValues = updatingEntity.GetType().GetProperties();

            foreach (var property in _NewPropertyValues.Where(p => !_PropertiesToIgnore.Contains(p.Name)))
            {
                var targetProperty = _EntityProperties
                    .FirstOrDefault(p => p.Name == property.Name &&
                        p.PropertyType == property.PropertyType &&
                        p.CanWrite);

                if (targetProperty is not null)
                {
                    if (targetProperty.PropertyType != property.PropertyType)
                    {
                        var _EntityType = entity.GetType();
                        var _RequestType = updatingEntity.GetType();
                        throw new Exception($"The Types of Property {targetProperty.Name} in {_EntityType} and {_RequestType} do not match.");
                    }

                    var value = property.GetValue(updatingEntity);
                    targetProperty.SetValue(entity, value);
                }
            }
        }
    }

}
