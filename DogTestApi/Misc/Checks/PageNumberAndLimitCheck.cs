namespace DogTestApi.Misc.Checks
{
    public class PageNumberAndLimitCheck
    {
        public static string pageNumberLimitcheck(int pageNumber, int pageLimit)
        {
            if (pageNumber < 0)
            {
                return ("ERROR: page number cannot be <0");
            }
            if (pageLimit < 1)
            {
                return ("ERROR: page limit cannot be <1");
            }
            return null;
        }
    }
}
