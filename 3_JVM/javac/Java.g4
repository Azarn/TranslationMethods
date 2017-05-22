grammar Java;

/*
 * Parser Rules
 */

compileUnit
    : public_maybe_static CLASS VARNAME LBRACE main_method RBRACE
    ;

main_method
    : PUBLIC STATIC VOID 'main' main_args LBRACE statement* RBRACE
    ;

main_args
    : LPAREN 'String' LBRACKET RBRACKET argName=VARNAME RPAREN
    ;

statement
    : LBRACE statement* RBRACE                      # NewBlock
    | expression SEMI                               # ExpressionStatement
    | var_decl SEMI                                 # ignoreStatement
    | print_call SEMI                               # ignoreStatement
    | while                                         # ignoreStatement
    | if                                            # ignoreStatement
    | do_while SEMI                                 # ignoreStatement
    | SEMI                                          # ignoreStatement
    ;

var_decl
    : var_type name=VARNAME (ASSIGN expr=expression_value)?
    ;

expression
    : assignment
    ;

expression_value
    : LPAREN expression_value RPAREN                # ParenExpr
    | expression                                    # PureExpr
    | name=VARNAME                                  # VarName
    | number=NUM                                    # Number
    | boolean_val                                   # Boolean
    | expression_value (op expression_value)+       # ExpressionChain
    | cast LPAREN expression_value RPAREN           # ExprCast
    ;

cast
    : LPAREN (cast | var_type) RPAREN
    ;

assignment
    : name=VARNAME lop expression_value
    ;

while
    : WHILE LPAREN expression_value RPAREN statement
    ;

do_while
    : DO statement WHILE LPAREN expression_value RPAREN
    ;

if
    : IF LPAREN expression_value RPAREN statement (ELSE statement)?
    ;

print_call
    : (PRINT | PRINTLN) LPAREN expression_value RPAREN
    ;

public_maybe_static
    : PUBLIC STATIC?
    | STATIC? PUBLIC
    ;

op
    : EQUAL
    | LE
    | GE
    | GT
    | LT
    | NOTEQUAL
    | AND
    | OR
    | ADD
    | SUB
    | MUL
    | DIV
    | MOD
    ;

lop
    : LA
    | LM
    | LD
    | LS
    | LMOD
    | ASSIGN
    ;

var_type
    : type=(INT | DOUBLE | FLOAT)
    ;

boolean_val
    : TRUE
    | FALSE
    ;

/*
 * Lexer Rules
 */

NUM
    : '-'?DIGITS+
    ;
DIGITS
    : [0123456789]
    ;
STATIC
    : 'static'
    ;
CLASS
    : 'class'
    ;
PUBLIC
    : 'public'
    ;
TRUE
    : 'true'
    ;
FALSE
    : 'false'
    ;
VOID
    : 'void'
    ;
INT
    : 'int'
    ;
DOUBLE
    : 'double'
    ;
FLOAT
    : 'float'
    ;
FOR
    : 'for'
    ;
ELSE
    : 'else'
    ;
WHILE
    : 'while'
    ;
DO
    : 'do'
    ;
IF
    : 'if'
    ;
PRINT
    : 'System.out.print'
    ;
PRINTLN
    : 'System.out.println'
    ;
LBRACKET
    : '['
    ;
RBRACKET
    : ']'
    ;
LPAREN
    : '('
    ;
RPAREN
    : ')'
    ;
LBRACE
    : '{'
    ;
RBRACE
    : '}'
    ;
SEMI
    : ';'
    ;
ASSIGN
    : '='
    ;
GT
    : '>'
    ;
LT
    : '<'
    ;
EQUAL
    : '=='
    ;
LE
    : '<='
    ;
GE
    : '>='
    ;
LA
    : '+='
    ;
LS
    : '-='
    ;
LM
    : '*='
    ;
LD
    : '/='
    ;
LMOD
    : '%='
    ;
NOTEQUAL
    : '!='
    ;
ADD
    : '+'
    ;
AA
    : '++'
    ;
SUB
    : '-'
    ;
SS
    : '--'
    ;
MUL
    : '*'
    ;
DIV
    : '/'
    ;
MOD
    : '%'
    ;
AND
    : '&&'
    ;
OR
    : '||'
    ;
VARNAME
    : [a-zA-Z] [a-zA-Z0-9_]*
    ;
WS
    : [ \t\r\n]+ -> skip
    ;
