// NEEDS REFACTORING
// NEEDS REFACTORING
// NEEDS REFACTORING
using System.Collections.Generic;
using System.Diagnostics;

public class NodeExpr {

    public object val;

    public NodeExpr(Token token) {
        if(token.Type == TokenType.IntLit) {
            val = token.Value;
        }
    }

}

public class NodeStatementExit {
    public NodeExpr expression;

    public NodeStatementExit(NodeExpr expr) {
        expression = expr;
    }
}

public class NodeStatementVar {
    public Token ident;
    public NodeExpr expr;

}

public class NodeStatement {
    public object val;

    public NodeStatement(NodeStatementExit exit) {
        val = exit;
    }

    public NodeStatement(NodeStatementVar var) {
        val = var;
    }
}

public class NodeProgram {
    public List<NodeStatement> statements { get; private set; }

    public NodeProgram(List<NodeStatement> stmts) {
        statements = stmts;
    }

}

public class Parser {
    
    private int m_index;
    private List<Token> m_tokens;

    public Parser(List<Token> tokens) { 
        m_tokens = tokens;
    }

    private NodeExpr ParseExpr() {

        NodeExpr expr = null;

        if(Peek().HasValue()) {
            if(Peek().Type == TokenType.IntLit) {
                expr = new NodeExpr(Consume());
            }
        }

        return expr;
    }

    private NodeStatementVar ParseStatementVar() {

        NodeStatementVar var = new NodeStatementVar();

        if(Peek().HasValue() && Peek().Type == TokenType.Ident) {
            var.ident = Consume();
        }
        else {
            Debug.Fail($"Cannot use reserved keyword `{Peek().Type}`");
            Environment.Exit(1);
        }

        if(Peek().HasValue() && Peek().Type == TokenType.Equals) {
            Consume();
        }
        else {
            Debug.Fail($"Expected `=`");
            Environment.Exit(1);
        }
    
        NodeExpr expr = ParseExpr();

        if(expr != null) {
            var.expr = expr;
        } 
        else {
            Debug.Fail("Invalid expression");
            Environment.Exit(1);
        }

        if(Peek().HasValue() && Peek().Type == TokenType.Semicolon) {
            Consume();
        }
        else {
            Debug.Fail($"Expected `;`");
            Environment.Exit(1);
        }

        return var;
    }

    private NodeStatementExit ParseStatementExit() {

        NodeStatementExit exit = null;

        if(Peek().HasValue() && Peek().Type == TokenType.OpenParen) {
            Consume();
        }
        else {
            Debug.Fail("Expected (");
            Environment.Exit(1);
        }

        NodeExpr expr = ParseExpr();

        if(expr != null) {
            exit = new NodeStatementExit(expr);
        }
        else {
            Debug.Fail("Invalid expression");
            Environment.Exit(1);
        }

        if(Peek().HasValue() && Peek().Type == TokenType.CloseParen) {
            Consume();
        }
        else {
            Debug.Fail("Expected `)`");
            Environment.Exit(1);
        }

        if(Peek().HasValue() && Peek().Type == TokenType.Semicolon) {
            Consume();
        } 
        else {
            Debug.Fail("Expected `;`");
            Environment.Exit(1);
        }

        return exit;
    }

    private NodeStatement ParseStatement() {

        NodeStatement stmt = null;

        if(Peek().HasValue()) {

            if(Peek().Type == TokenType.Exit) {

                Consume();

                NodeStatementExit exit = ParseStatementExit();

                if(exit != null) {
                    stmt = new NodeStatement(exit);
                }

            }

            else if(Peek().Type == TokenType.Var) {

                Consume();

                NodeStatementVar var = ParseStatementVar();

                if(var != null) {
                    stmt = new NodeStatement(var);
                }

            }

        }

        return stmt;

    }

    public NodeProgram Parse() {

        var statements = new List<NodeStatement>();

        NodeProgram program;

        while(Peek().HasValue()) {

            NodeStatement stmt = ParseStatement();

            if(stmt != null) {
                
                statements.Add(stmt);

            }

            else {
                Debug.Fail("Invalid statement");
                Environment.Exit(1);
            }
        }

        program = new NodeProgram(statements);

        return program;
    }


    public Token Peek(int offset = 0)
    {
        if(m_index + offset >= m_tokens.Count)
        {
            return new Token(TokenType._null);
        }

        return m_tokens[m_index+offset];
    }

    public Token Consume()
    {
        Token t = m_tokens[m_index];
        m_index++;

        return t;
    }

}