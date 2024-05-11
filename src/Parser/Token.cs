using System.Diagnostics;

public enum TokenType
{
    _null, Exit, Ret, IntLit, Str, Semicolon, OpenParen, CloseParen, Ident, Var, Const,
    Equals

}

public class Token
{
    public TokenType? Type {get; private set;}
    public string? Value {get; private set;}

    public Token(TokenType type, string value = null)
    {
        if(type == null) { 
            Debug.WriteLine("Initializing Null Token!");
            return;
        }

        Type = type;
        Value = value;
    }

    public bool HasValue() {
        return Type != TokenType._null;
    }
}