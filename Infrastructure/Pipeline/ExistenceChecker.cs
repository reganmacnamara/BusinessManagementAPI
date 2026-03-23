using MacsBusinessManagementAPI.Data;

namespace MacsBusinessManagementAPI.Infrastructure.Pipeline
{
    public class ExistenceChecker<T>(SQLContext context) where T : class
    {
        public bool ValidateEntitiesExist(List<long> entityIDs)
        {
            foreach (var _ID in entityIDs)
                if (context.Find<T>(_ID) is null)
                    return false;

            return true;
        }

        public bool ValidateEntityExists(long entityID)
            => context.Find<T>(entityID) is not null;
    }
}
