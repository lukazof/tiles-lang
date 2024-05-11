public static class Utils{

    public static bool IsDigit(char? c)
    {

        return c == '0' || c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9';
    }

    public static bool IsAlpha(char? c)
    {
        c = c.ToString().ToLower().ToCharArray()[0];

        return c == 'a' || c == 'b' || c == 'c' || c == 'd' || c == 'e' || c == 'f' || c == 'g' || c == 'h' || c == 'i' || c == 'j'
            || c == 'k' || c == 'l' || c == 'm' || c == 'n' || c == 'o' || c == 'p' || c == 'q' || c == 'r' || c == 's' || c == 't'
            || c == 'u' || c == 'v' || c == 'w' || c == 'x' || c == 'y' || c == 'z';
    }

    public static bool IsAlphaNum(char? c)
    {
        return IsAlpha(c) || IsDigit(c);
    }
   
}