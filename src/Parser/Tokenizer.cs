using System.Collections.Generic;

public class Tokenizer
{
    private int m_index = 0;
    private string m_src = "";
    public Tokenizer(string src)
    {
        m_src = src;
    }

    public List<Token> Tokenize()
    {
        var tokens = new List<Token>();

        var buf = "";

        while(Peek() != null)
        {
            #region identifiers
            if(char.IsLetter((char)Peek()))
            {
                buf += (char)Consume();

                while(Peek() != null && Char.IsLetterOrDigit((char)Peek())) 
                {
                    buf += (char)Consume();
                }

                if(buf == "exit")
                {
                    tokens.Add(new Token(TokenType.Exit));
                    buf = "";
                }

                else if (buf == "var")
                {
                    tokens.Add(new Token(TokenType.Var));
                    buf = "";
                }

                else 
                {
                    tokens.Add(new Token(TokenType.Ident, buf));
                    buf = "";
                }
            }

            else if(char.IsDigit((char)Peek()))
            {
                buf += Consume();

                while(Peek() != null && char.IsDigit((char)Peek()))
                {
                    buf += Consume();
                }

                tokens.Add(new Token(TokenType.IntLit, buf));
                buf = "";
            }

            else if (Peek() == ';') {
                Consume();
                tokens.Add(new Token(TokenType.Semicolon));
            }

            else if (Peek() == '(') {
                Consume();
                tokens.Add(new Token(TokenType.OpenParen));
            }

            else if (Peek() == ')') {
                Consume();
                tokens.Add(new Token(TokenType.CloseParen));
            }

            else if (Peek() == '=') {
                Consume();
                tokens.Add(new Token(TokenType.Equals));
            }

            else if(Peek() == ' '){
                Consume();
            }

            else if(Peek() == '/' && Peek(1) == '/') {
                Consume();
                Consume();

                while(Peek() != '\n') {
                    Consume();
                }

                Consume();
            }

            else {
                Console.WriteLine($"Unknown symbol: {Peek()}");
                return null;
            }
            
            #endregion
        }

        m_index = 0;

        return tokens;

    }

    public char? Peek(int offset = 0)
    {
        if(m_index + offset >= m_src.Length)
        {
            return null;
        }

        return m_src[m_index+offset];
    }

    public char? Consume()
    {
        char? c = m_src[m_index];

        m_index++;

        return c;
    }

}