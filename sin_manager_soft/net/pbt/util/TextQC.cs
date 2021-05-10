namespace sin_platform_soft_unit_tests.net.pbt.util
{
    public class TextQC : IInputQC
    {
        public bool IsValid(string str)
        {
            if (str == null)
            {
                return false;
            }

            return str.Trim() != "";
        }
    }
}