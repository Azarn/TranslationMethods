grammar Java;

/*
 * Parser Rules
 */

compileUnit
    : PUBLIC CLASS VARNAME LBRACE main_method RBRACE
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
    : LPAREN expression_value RPAREN                                        # ParenExpr
    | boolean_val                                                           # Boolean
    | name=VARNAME                                                          # VarName
    | number=number_val                                                     # Number
    | LOGICAL_NOT expression_value                                          # LogicalNot
    | cast LPAREN expression_value RPAREN                                   # ExprCast
    | (ADD | SUB) expression_value                                          # UnaryPlusMinus
    | first=expression_value op=(MUL | DIV | REM) second=expression_value   # MulDivRem
    | first=expression_value op=(ADD | SUB) second=expression_value         # AddSub
    | first=expression_value op=(LT | LE | GT | GE) second=expression_value # LtLeGtGe
    | first=expression_value op=(EQUAL | NOTEQUAL) second=expression_value  # EqualNotEqual
    | first=expression_value LOGICAL_AND second=expression_value            # LogicalAnd
    | first=expression_value LOGICAL_OR second=expression_value             # LogicalOr
    | expression                                                            # PureExpr
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
    : IF LPAREN expression_value RPAREN true_branch=statement (ELSE false_branch=statement)?
    ;

print_call
    : (PRINT | PRINTLN) LPAREN expression_value RPAREN
    ;

public_maybe_static
    : PUBLIC STATIC?
    | STATIC? PUBLIC
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

number_val
    : INTEGER_NUM
    | DOUBLE_NUM
    ;

/*
 * Lexer Rules
 */

DOUBLE_NUM
    : '-'? DIGITS+ '.' DIGITS+
    ;
INTEGER_NUM
    : '-'? DIGITS+
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
ASSIGN
    : '='
    ;
GT
    : '>'
    ;
LT
    : '<'
    ;
AA
    : '++'
    ;
ADD
    : '+'
    ;
SS
    : '--'
    ;
SUB
    : '-'
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
LOGICAL_NOT
    : '!'
    ;
LOGICAL_AND
    : '&&'
    ;
LOGICAL_OR
    : '||'
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
