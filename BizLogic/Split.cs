namespace BizLogic
{
    public static class Split
    {
        //This is used to split a tweet above 100 in character lenght into chunks with a specified lenght 
        public static IEnumerable<string> SplitByLength(this string str, int maxLength)
        {
            for (int index = 0; index < str.Length; index += maxLength)
            {
                yield return str.Substring(index, Math.Min(maxLength, str.Length - index));
            }
        }
    }
}