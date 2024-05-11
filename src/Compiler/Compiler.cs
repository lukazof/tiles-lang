public class Compiler
{
    private string m_output = "";
    private NodeProgram m_program;
    public Compiler(NodeProgram program) {
        m_program = program;
    }

    public string GenerateAssembly()
    {
        GenerateStartProgram();

        var stmts = m_program.statements;

        for (int i = 0; i < m_program.statements.Count; i++) {

            var stmt = stmts[i];

            if(stmt.val.GetType() == typeof(NodeStatementExit)) {
                
                var exitCode = 0;

                NodeStatementExit exit = (NodeStatementExit)stmt.val;

                exitCode = int.Parse(exit.expression.val.ToString());

                GenerateExitProgram(exitCode);

                // we are quitting anyway
                return m_output;
            }

        }

        GenerateExitProgram();

        return m_output;
    }


    private void GenerateStartProgram() {

        m_output += "global _start\n";
        m_output += "_start:\n";
    }

    private void GenerateExitProgram(int exitCode = 0) {

        m_output += $"    mov rax, 60\n";
        m_output += $"    mov rdi, {exitCode}\n";
        m_output += $"    syscall\n";
    }
}
