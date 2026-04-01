using MacsBusinessManagementAPI.Data;

namespace MacsBusinessManagementAPI.Infrastructure.Pipeline
{
    public class ExistenceChecker(SQLContext context)
    {
        public bool ValidateEntitiesExist<T>(List<long> entityIDs) where T : class
        {
            foreach (var _ID in entityIDs)
                if (context.Find<T>(_ID) is null)
                    return false;

            return true;
        }

        public bool ValidateEntityExists<T>(long entityID) where T : class
            => context.Find<T>(entityID) is not null;
    }
}
