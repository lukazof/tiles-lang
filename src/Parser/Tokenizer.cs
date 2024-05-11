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

                Console.WriteLine(buf);

                if(buf == "exit")
                {
                    tokens.Add(new Token(TokenType.Exit));
                    buf = "";
                    continue;
                }

                else if (buf == "var")
                {
                    tokens.Add(new Token(TokenType.Var));
                    buf = "";
                    continue;
                }

                else 
                {
                    Console.WriteLine($"error: {buf}");
                }
            }

            else if(Utils.IsDigit(Peek()))
            {
                buf += Consume();

                while(Peek() != null && Utils.IsDigit(Peek()))
                {
                    buf += Consume();
                }

                tokens.Add(new Token(TokenType.IntLit, buf));
                buf = "";
                continue;
            }

            else if (Peek() == ';') {
                Consume();
                tokens.Add(new Token(TokenType.Semicolon));
                continue;
            }

            else if (Peek() == '(') {
                Consume();
                tokens.Add(new Token(TokenType.OpenParen));
                continue;
            }

            else if (Peek() == ')') {
                Consume();
                tokens.Add(new Token(TokenType.CloseParen));
                continue;
            }

            else if (Peek() == '=') {
                Consume();
                tokens.Add(new Token(TokenType.Equals));
                continue;
            }

            else if(Peek() == ' '){
                Consume();
                continue;
            }

            else {
                Console.WriteLine("Unknown identifier");
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