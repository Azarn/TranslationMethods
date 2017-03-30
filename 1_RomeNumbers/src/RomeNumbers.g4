grammar RomeNumbers;

parse
    : expr { System.out.println(RomeNumbers.arabicToRome($expr.val)); }
    ;

expr returns [int val]
    : number exprP[$number.val] { $val = $exprP.val; }
    ;

exprP[int i] returns [int val]
    : { $val = $i; }
    | PLUS expr { $val = $i + $expr.val; }
    ;

number returns [int val]
    : ROMENUMBER { $val = RomeNumbers.romeToArabic($ROMENUMBER.text); }
    ;

ROMENUMBER
    : [IVXLCDM]+
    ;

PLUS
    : '+'
    ;

WS
    : [ \t\r\n]+ -> skip
    ;