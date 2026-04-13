using MacsBusinessManagementAPI.Data;

namespace MacsBusinessManagementAPI.Infrastructure.EntityValidator
{
    public class EntityValidator(SQLContext context)
    {
        public List<long> ValidateEntitiesExist<T>(List<long> entityIDs) where T : class
        {
            var _InvalidIDs = new List<long>();

            foreach (var _ID in entityIDs)
                if (context.Find<T>(_ID) is null)
                    _InvalidIDs.Add(_ID);

            return _InvalidIDs;
        }

        public bool ValidateEntityExists<T>(long entityID) where T : class
            => context.Find<T>(entityID) is not null;
    }
}
