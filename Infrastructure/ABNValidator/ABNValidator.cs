namespace MacsBusinessManagementAPI.Infrastructure.ABNValidator
{

    public static class ABNValidator
    {
        public static bool IsValidABN(string abn)
        {
            abn = abn.Replace(" ", "");

            if (abn.Length != 11 || !long.TryParse(abn, out _))
                return false;

            int[] weights = [10, 1, 3, 5, 7, 9, 11, 13, 15, 17, 19];

            var sum = 0;
            for (var i = 0; i < 11; i++)
            {
                var digit = abn[i] - '0';
                if (i == 0) digit--;
                sum += digit * weights[i];
            }

            return sum % 89 == 0;
        }

    }
}
