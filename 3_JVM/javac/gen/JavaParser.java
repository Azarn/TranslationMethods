// Generated from D:/disk/Projects/ifmo_translation_methods/3_JVM/javac\Java.g4 by ANTLR 4.7
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class JavaParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.7", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		VARNAME=1, NUM=2, DIGITS=3, STRING=4, STATIC=5, CLASS=6, PUBLIC=7, TRUE=8, 
		FALSE=9, VOID=10, INT=11, DOUBLE=12, FLOAT=13, FOR=14, ELSE=15, WHILE=16, 
		DO=17, IF=18, PRINT=19, PRINTLN=20, LBRACKET=21, RBRACKET=22, LPAREN=23, 
		RPAREN=24, LBRACE=25, RBRACE=26, SEMI=27, ASSIGN=28, GT=29, LT=30, EQUAL=31, 
		LE=32, GE=33, LA=34, LS=35, LM=36, LD=37, LMOD=38, NOTEQUAL=39, ADD=40, 
		AA=41, SUB=42, SS=43, MUL=44, DIV=45, MOD=46, AND=47, OR=48, WS=49;
	public static final int
		RULE_compileUnit = 0, RULE_main_method = 1, RULE_main_args = 2, RULE_statement = 3, 
		RULE_var_decl = 4, RULE_expression = 5, RULE_expression_value = 6, RULE_cast = 7, 
		RULE_assignment = 8, RULE_while = 9, RULE_do_while = 10, RULE_if = 11, 
		RULE_print_call = 12, RULE_public_or_static = 13, RULE_op = 14, RULE_lop = 15, 
		RULE_var_type = 16, RULE_boolean_val = 17;
	public static final String[] ruleNames = {
		"compileUnit", "main_method", "main_args", "statement", "var_decl", "expression", 
		"expression_value", "cast", "assignment", "while", "do_while", "if", "print_call", 
		"public_or_static", "op", "lop", "var_type", "boolean_val"
	};

	private static final String[] _LITERAL_NAMES = {
		null, null, null, null, "'String'", "'static'", "'class'", "'public'", 
		"'true'", "'false'", "'void'", "'int'", "'double'", "'float'", "'for'", 
		"'else'", "'while'", "'do'", "'if'", "'System.out.print'", "'System.out.println'", 
		"'['", "']'", "'('", "')'", "'{'", "'}'", "';'", "'='", "'>'", "'<'", 
		"'=='", "'<='", "'>='", "'+='", "'-='", "'*='", "'/='", "'%='", "'!='", 
		"'+'", "'++'", "'-'", "'--'", "'*'", "'/'", "'%'", "'&&'", "'||'"
	};
	private static final String[] _SYMBOLIC_NAMES = {
		null, "VARNAME", "NUM", "DIGITS", "STRING", "STATIC", "CLASS", "PUBLIC", 
		"TRUE", "FALSE", "VOID", "INT", "DOUBLE", "FLOAT", "FOR", "ELSE", "WHILE", 
		"DO", "IF", "PRINT", "PRINTLN", "LBRACKET", "RBRACKET", "LPAREN", "RPAREN", 
		"LBRACE", "RBRACE", "SEMI", "ASSIGN", "GT", "LT", "EQUAL", "LE", "GE", 
		"LA", "LS", "LM", "LD", "LMOD", "NOTEQUAL", "ADD", "AA", "SUB", "SS", 
		"MUL", "DIV", "MOD", "AND", "OR", "WS"
	};
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}

	@Override
	public String getGrammarFileName() { return "Java.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public JavaParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}
	public static class CompileUnitContext extends ParserRuleContext {
		public Public_or_staticContext public_or_static() {
			return getRuleContext(Public_or_staticContext.class,0);
		}
		public TerminalNode CLASS() { return getToken(JavaParser.CLASS, 0); }
		public TerminalNode VARNAME() { return getToken(JavaParser.VARNAME, 0); }
		public TerminalNode LBRACE() { return getToken(JavaParser.LBRACE, 0); }
		public Main_methodContext main_method() {
			return getRuleContext(Main_methodContext.class,0);
		}
		public TerminalNode RBRACE() { return getToken(JavaParser.RBRACE, 0); }
		public CompileUnitContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_compileUnit; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).enterCompileUnit(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).exitCompileUnit(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JavaVisitor ) return ((JavaVisitor<? extends T>)visitor).visitCompileUnit(this);
			else return visitor.visitChildren(this);
		}
	}

	public final CompileUnitContext compileUnit() throws RecognitionException {
		CompileUnitContext _localctx = new CompileUnitContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_compileUnit);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(36);
			public_or_static();
			setState(37);
			match(CLASS);
			setState(38);
			match(VARNAME);
			setState(39);
			match(LBRACE);
			setState(40);
			main_method();
			setState(41);
			match(RBRACE);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Main_methodContext extends ParserRuleContext {
		public Public_or_staticContext public_or_static() {
			return getRuleContext(Public_or_staticContext.class,0);
		}
		public TerminalNode VOID() { return getToken(JavaParser.VOID, 0); }
		public TerminalNode VARNAME() { return getToken(JavaParser.VARNAME, 0); }
		public Main_argsContext main_args() {
			return getRuleContext(Main_argsContext.class,0);
		}
		public TerminalNode LBRACE() { return getToken(JavaParser.LBRACE, 0); }
		public TerminalNode RBRACE() { return getToken(JavaParser.RBRACE, 0); }
		public List<StatementContext> statement() {
			return getRuleContexts(StatementContext.class);
		}
		public StatementContext statement(int i) {
			return getRuleContext(StatementContext.class,i);
		}
		public Main_methodContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_main_method; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).enterMain_method(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).exitMain_method(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JavaVisitor ) return ((JavaVisitor<? extends T>)visitor).visitMain_method(this);
			else return visitor.visitChildren(this);
		}
	}

	public final Main_methodContext main_method() throws RecognitionException {
		Main_methodContext _localctx = new Main_methodContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_main_method);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(43);
			public_or_static();
			setState(44);
			match(VOID);
			setState(45);
			match(VARNAME);
			setState(46);
			main_args();
			setState(47);
			match(LBRACE);
			setState(51);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << VARNAME) | (1L << INT) | (1L << DOUBLE) | (1L << FLOAT) | (1L << WHILE) | (1L << DO) | (1L << IF) | (1L << PRINT) | (1L << PRINTLN) | (1L << LBRACE) | (1L << SEMI))) != 0)) {
				{
				{
				setState(48);
				statement();
				}
				}
				setState(53);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(54);
			match(RBRACE);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Main_argsContext extends ParserRuleContext {
		public TerminalNode LPAREN() { return getToken(JavaParser.LPAREN, 0); }
		public TerminalNode STRING() { return getToken(JavaParser.STRING, 0); }
		public TerminalNode LBRACKET() { return getToken(JavaParser.LBRACKET, 0); }
		public TerminalNode RBRACKET() { return getToken(JavaParser.RBRACKET, 0); }
		public TerminalNode VARNAME() { return getToken(JavaParser.VARNAME, 0); }
		public TerminalNode RPAREN() { return getToken(JavaParser.RPAREN, 0); }
		public Main_argsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_main_args; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).enterMain_args(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).exitMain_args(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JavaVisitor ) return ((JavaVisitor<? extends T>)visitor).visitMain_args(this);
			else return visitor.visitChildren(this);
		}
	}

	public final Main_argsContext main_args() throws RecognitionException {
		Main_argsContext _localctx = new Main_argsContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_main_args);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(56);
			match(LPAREN);
			setState(57);
			match(STRING);
			setState(58);
			match(LBRACKET);
			setState(59);
			match(RBRACKET);
			setState(60);
			match(VARNAME);
			setState(61);
			match(RPAREN);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class StatementContext extends ParserRuleContext {
		public TerminalNode LBRACE() { return getToken(JavaParser.LBRACE, 0); }
		public TerminalNode RBRACE() { return getToken(JavaParser.RBRACE, 0); }
		public List<StatementContext> statement() {
			return getRuleContexts(StatementContext.class);
		}
		public StatementContext statement(int i) {
			return getRuleContext(StatementContext.class,i);
		}
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode SEMI() { return getToken(JavaParser.SEMI, 0); }
		public Var_declContext var_decl() {
			return getRuleContext(Var_declContext.class,0);
		}
		public Print_callContext print_call() {
			return getRuleContext(Print_callContext.class,0);
		}
		public WhileContext while() {
			return getRuleContext(WhileContext.class,0);
		}
		public IfContext if() {
			return getRuleContext(IfContext.class,0);
		}
		public Do_whileContext do_while() {
			return getRuleContext(Do_whileContext.class,0);
		}
		public StatementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_statement; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).enterStatement(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).exitStatement(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JavaVisitor ) return ((JavaVisitor<? extends T>)visitor).visitStatement(this);
			else return visitor.visitChildren(this);
		}
	}

	public final StatementContext statement() throws RecognitionException {
		StatementContext _localctx = new StatementContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_statement);
		int _la;
		try {
			setState(86);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case LBRACE:
				enterOuterAlt(_localctx, 1);
				{
				setState(63);
				match(LBRACE);
				setState(67);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << VARNAME) | (1L << INT) | (1L << DOUBLE) | (1L << FLOAT) | (1L << WHILE) | (1L << DO) | (1L << IF) | (1L << PRINT) | (1L << PRINTLN) | (1L << LBRACE) | (1L << SEMI))) != 0)) {
					{
					{
					setState(64);
					statement();
					}
					}
					setState(69);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(70);
				match(RBRACE);
				}
				break;
			case VARNAME:
				enterOuterAlt(_localctx, 2);
				{
				setState(71);
				expression();
				setState(72);
				match(SEMI);
				}
				break;
			case INT:
			case DOUBLE:
			case FLOAT:
				enterOuterAlt(_localctx, 3);
				{
				setState(74);
				var_decl();
				setState(75);
				match(SEMI);
				}
				break;
			case PRINT:
			case PRINTLN:
				enterOuterAlt(_localctx, 4);
				{
				setState(77);
				print_call();
				setState(78);
				match(SEMI);
				}
				break;
			case WHILE:
				enterOuterAlt(_localctx, 5);
				{
				setState(80);
				while();
				}
				break;
			case IF:
				enterOuterAlt(_localctx, 6);
				{
				setState(81);
				if();
				}
				break;
			case DO:
				enterOuterAlt(_localctx, 7);
				{
				setState(82);
				do_while();
				setState(83);
				match(SEMI);
				}
				break;
			case SEMI:
				enterOuterAlt(_localctx, 8);
				{
				setState(85);
				match(SEMI);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Var_declContext extends ParserRuleContext {
		public Var_typeContext var_type() {
			return getRuleContext(Var_typeContext.class,0);
		}
		public TerminalNode VARNAME() { return getToken(JavaParser.VARNAME, 0); }
		public TerminalNode ASSIGN() { return getToken(JavaParser.ASSIGN, 0); }
		public Expression_valueContext expression_value() {
			return getRuleContext(Expression_valueContext.class,0);
		}
		public Var_declContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_var_decl; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).enterVar_decl(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).exitVar_decl(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JavaVisitor ) return ((JavaVisitor<? extends T>)visitor).visitVar_decl(this);
			else return visitor.visitChildren(this);
		}
	}

	public final Var_declContext var_decl() throws RecognitionException {
		Var_declContext _localctx = new Var_declContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_var_decl);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(88);
			var_type();
			setState(89);
			match(VARNAME);
			setState(92);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==ASSIGN) {
				{
				setState(90);
				match(ASSIGN);
				setState(91);
				expression_value(0);
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ExpressionContext extends ParserRuleContext {
		public AssignmentContext assignment() {
			return getRuleContext(AssignmentContext.class,0);
		}
		public ExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expression; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).enterExpression(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).exitExpression(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JavaVisitor ) return ((JavaVisitor<? extends T>)visitor).visitExpression(this);
			else return visitor.visitChildren(this);
		}
	}

	public final ExpressionContext expression() throws RecognitionException {
		ExpressionContext _localctx = new ExpressionContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(94);
			assignment();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Expression_valueContext extends ParserRuleContext {
		public TerminalNode LPAREN() { return getToken(JavaParser.LPAREN, 0); }
		public List<Expression_valueContext> expression_value() {
			return getRuleContexts(Expression_valueContext.class);
		}
		public Expression_valueContext expression_value(int i) {
			return getRuleContext(Expression_valueContext.class,i);
		}
		public TerminalNode RPAREN() { return getToken(JavaParser.RPAREN, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode VARNAME() { return getToken(JavaParser.VARNAME, 0); }
		public TerminalNode NUM() { return getToken(JavaParser.NUM, 0); }
		public Boolean_valContext boolean_val() {
			return getRuleContext(Boolean_valContext.class,0);
		}
		public CastContext cast() {
			return getRuleContext(CastContext.class,0);
		}
		public List<OpContext> op() {
			return getRuleContexts(OpContext.class);
		}
		public OpContext op(int i) {
			return getRuleContext(OpContext.class,i);
		}
		public Expression_valueContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expression_value; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).enterExpression_value(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).exitExpression_value(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JavaVisitor ) return ((JavaVisitor<? extends T>)visitor).visitExpression_value(this);
			else return visitor.visitChildren(this);
		}
	}

	public final Expression_valueContext expression_value() throws RecognitionException {
		return expression_value(0);
	}

	private Expression_valueContext expression_value(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		Expression_valueContext _localctx = new Expression_valueContext(_ctx, _parentState);
		Expression_valueContext _prevctx = _localctx;
		int _startState = 12;
		enterRecursionRule(_localctx, 12, RULE_expression_value, _p);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(110);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,4,_ctx) ) {
			case 1:
				{
				setState(97);
				match(LPAREN);
				setState(98);
				expression_value(0);
				setState(99);
				match(RPAREN);
				}
				break;
			case 2:
				{
				setState(101);
				expression();
				}
				break;
			case 3:
				{
				setState(102);
				match(VARNAME);
				}
				break;
			case 4:
				{
				setState(103);
				match(NUM);
				}
				break;
			case 5:
				{
				setState(104);
				boolean_val();
				}
				break;
			case 6:
				{
				setState(105);
				cast();
				setState(106);
				match(LPAREN);
				setState(107);
				expression_value(0);
				setState(108);
				match(RPAREN);
				}
				break;
			}
			_ctx.stop = _input.LT(-1);
			setState(122);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,6,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					{
					_localctx = new Expression_valueContext(_parentctx, _parentState);
					pushNewRecursionContext(_localctx, _startState, RULE_expression_value);
					setState(112);
					if (!(precpred(_ctx, 2))) throw new FailedPredicateException(this, "precpred(_ctx, 2)");
					setState(116); 
					_errHandler.sync(this);
					_alt = 1;
					do {
						switch (_alt) {
						case 1:
							{
							{
							setState(113);
							op();
							setState(114);
							expression_value(0);
							}
							}
							break;
						default:
							throw new NoViableAltException(this);
						}
						setState(118); 
						_errHandler.sync(this);
						_alt = getInterpreter().adaptivePredict(_input,5,_ctx);
					} while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER );
					}
					} 
				}
				setState(124);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,6,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public static class CastContext extends ParserRuleContext {
		public TerminalNode LPAREN() { return getToken(JavaParser.LPAREN, 0); }
		public TerminalNode RPAREN() { return getToken(JavaParser.RPAREN, 0); }
		public CastContext cast() {
			return getRuleContext(CastContext.class,0);
		}
		public Var_typeContext var_type() {
			return getRuleContext(Var_typeContext.class,0);
		}
		public CastContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_cast; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).enterCast(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).exitCast(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JavaVisitor ) return ((JavaVisitor<? extends T>)visitor).visitCast(this);
			else return visitor.visitChildren(this);
		}
	}

	public final CastContext cast() throws RecognitionException {
		CastContext _localctx = new CastContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_cast);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(125);
			match(LPAREN);
			setState(128);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case LPAREN:
				{
				setState(126);
				cast();
				}
				break;
			case INT:
			case DOUBLE:
			case FLOAT:
				{
				setState(127);
				var_type();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			setState(130);
			match(RPAREN);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class AssignmentContext extends ParserRuleContext {
		public TerminalNode VARNAME() { return getToken(JavaParser.VARNAME, 0); }
		public LopContext lop() {
			return getRuleContext(LopContext.class,0);
		}
		public Expression_valueContext expression_value() {
			return getRuleContext(Expression_valueContext.class,0);
		}
		public AssignmentContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_assignment; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).enterAssignment(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).exitAssignment(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JavaVisitor ) return ((JavaVisitor<? extends T>)visitor).visitAssignment(this);
			else return visitor.visitChildren(this);
		}
	}

	public final AssignmentContext assignment() throws RecognitionException {
		AssignmentContext _localctx = new AssignmentContext(_ctx, getState());
		enterRule(_localctx, 16, RULE_assignment);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(132);
			match(VARNAME);
			setState(133);
			lop();
			setState(134);
			expression_value(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class WhileContext extends ParserRuleContext {
		public TerminalNode WHILE() { return getToken(JavaParser.WHILE, 0); }
		public TerminalNode LPAREN() { return getToken(JavaParser.LPAREN, 0); }
		public Expression_valueContext expression_value() {
			return getRuleContext(Expression_valueContext.class,0);
		}
		public TerminalNode RPAREN() { return getToken(JavaParser.RPAREN, 0); }
		public StatementContext statement() {
			return getRuleContext(StatementContext.class,0);
		}
		public WhileContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_while; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).enterWhile(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).exitWhile(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JavaVisitor ) return ((JavaVisitor<? extends T>)visitor).visitWhile(this);
			else return visitor.visitChildren(this);
		}
	}

	public final WhileContext while() throws RecognitionException {
		WhileContext _localctx = new WhileContext(_ctx, getState());
		enterRule(_localctx, 18, RULE_while);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(136);
			match(WHILE);
			setState(137);
			match(LPAREN);
			setState(138);
			expression_value(0);
			setState(139);
			match(RPAREN);
			setState(140);
			statement();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Do_whileContext extends ParserRuleContext {
		public TerminalNode DO() { return getToken(JavaParser.DO, 0); }
		public StatementContext statement() {
			return getRuleContext(StatementContext.class,0);
		}
		public TerminalNode WHILE() { return getToken(JavaParser.WHILE, 0); }
		public TerminalNode LPAREN() { return getToken(JavaParser.LPAREN, 0); }
		public Expression_valueContext expression_value() {
			return getRuleContext(Expression_valueContext.class,0);
		}
		public TerminalNode RPAREN() { return getToken(JavaParser.RPAREN, 0); }
		public Do_whileContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_do_while; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).enterDo_while(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).exitDo_while(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JavaVisitor ) return ((JavaVisitor<? extends T>)visitor).visitDo_while(this);
			else return visitor.visitChildren(this);
		}
	}

	public final Do_whileContext do_while() throws RecognitionException {
		Do_whileContext _localctx = new Do_whileContext(_ctx, getState());
		enterRule(_localctx, 20, RULE_do_while);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(142);
			match(DO);
			setState(143);
			statement();
			setState(144);
			match(WHILE);
			setState(145);
			match(LPAREN);
			setState(146);
			expression_value(0);
			setState(147);
			match(RPAREN);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class IfContext extends ParserRuleContext {
		public TerminalNode IF() { return getToken(JavaParser.IF, 0); }
		public TerminalNode LPAREN() { return getToken(JavaParser.LPAREN, 0); }
		public Expression_valueContext expression_value() {
			return getRuleContext(Expression_valueContext.class,0);
		}
		public TerminalNode RPAREN() { return getToken(JavaParser.RPAREN, 0); }
		public List<StatementContext> statement() {
			return getRuleContexts(StatementContext.class);
		}
		public StatementContext statement(int i) {
			return getRuleContext(StatementContext.class,i);
		}
		public TerminalNode ELSE() { return getToken(JavaParser.ELSE, 0); }
		public IfContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_if; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).enterIf(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).exitIf(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JavaVisitor ) return ((JavaVisitor<? extends T>)visitor).visitIf(this);
			else return visitor.visitChildren(this);
		}
	}

	public final IfContext if() throws RecognitionException {
		IfContext _localctx = new IfContext(_ctx, getState());
		enterRule(_localctx, 22, RULE_if);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(149);
			match(IF);
			setState(150);
			match(LPAREN);
			setState(151);
			expression_value(0);
			setState(152);
			match(RPAREN);
			setState(153);
			statement();
			setState(156);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,8,_ctx) ) {
			case 1:
				{
				setState(154);
				match(ELSE);
				setState(155);
				statement();
				}
				break;
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Print_callContext extends ParserRuleContext {
		public TerminalNode LPAREN() { return getToken(JavaParser.LPAREN, 0); }
		public Expression_valueContext expression_value() {
			return getRuleContext(Expression_valueContext.class,0);
		}
		public TerminalNode RPAREN() { return getToken(JavaParser.RPAREN, 0); }
		public TerminalNode PRINT() { return getToken(JavaParser.PRINT, 0); }
		public TerminalNode PRINTLN() { return getToken(JavaParser.PRINTLN, 0); }
		public Print_callContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_print_call; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).enterPrint_call(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).exitPrint_call(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JavaVisitor ) return ((JavaVisitor<? extends T>)visitor).visitPrint_call(this);
			else return visitor.visitChildren(this);
		}
	}

	public final Print_callContext print_call() throws RecognitionException {
		Print_callContext _localctx = new Print_callContext(_ctx, getState());
		enterRule(_localctx, 24, RULE_print_call);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(158);
			_la = _input.LA(1);
			if ( !(_la==PRINT || _la==PRINTLN) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			setState(159);
			match(LPAREN);
			setState(160);
			expression_value(0);
			setState(161);
			match(RPAREN);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Public_or_staticContext extends ParserRuleContext {
		public TerminalNode PUBLIC() { return getToken(JavaParser.PUBLIC, 0); }
		public TerminalNode STATIC() { return getToken(JavaParser.STATIC, 0); }
		public Public_or_staticContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_public_or_static; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).enterPublic_or_static(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).exitPublic_or_static(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JavaVisitor ) return ((JavaVisitor<? extends T>)visitor).visitPublic_or_static(this);
			else return visitor.visitChildren(this);
		}
	}

	public final Public_or_staticContext public_or_static() throws RecognitionException {
		Public_or_staticContext _localctx = new Public_or_staticContext(_ctx, getState());
		enterRule(_localctx, 26, RULE_public_or_static);
		int _la;
		try {
			setState(175);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,13,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(164);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==PUBLIC) {
					{
					setState(163);
					match(PUBLIC);
					}
				}

				setState(167);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==STATIC) {
					{
					setState(166);
					match(STATIC);
					}
				}

				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(170);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==STATIC) {
					{
					setState(169);
					match(STATIC);
					}
				}

				setState(173);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==PUBLIC) {
					{
					setState(172);
					match(PUBLIC);
					}
				}

				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class OpContext extends ParserRuleContext {
		public TerminalNode EQUAL() { return getToken(JavaParser.EQUAL, 0); }
		public TerminalNode LE() { return getToken(JavaParser.LE, 0); }
		public TerminalNode GE() { return getToken(JavaParser.GE, 0); }
		public TerminalNode GT() { return getToken(JavaParser.GT, 0); }
		public TerminalNode LT() { return getToken(JavaParser.LT, 0); }
		public TerminalNode NOTEQUAL() { return getToken(JavaParser.NOTEQUAL, 0); }
		public TerminalNode AND() { return getToken(JavaParser.AND, 0); }
		public TerminalNode OR() { return getToken(JavaParser.OR, 0); }
		public TerminalNode ADD() { return getToken(JavaParser.ADD, 0); }
		public TerminalNode SUB() { return getToken(JavaParser.SUB, 0); }
		public TerminalNode MUL() { return getToken(JavaParser.MUL, 0); }
		public TerminalNode DIV() { return getToken(JavaParser.DIV, 0); }
		public TerminalNode MOD() { return getToken(JavaParser.MOD, 0); }
		public OpContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_op; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).enterOp(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).exitOp(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JavaVisitor ) return ((JavaVisitor<? extends T>)visitor).visitOp(this);
			else return visitor.visitChildren(this);
		}
	}

	public final OpContext op() throws RecognitionException {
		OpContext _localctx = new OpContext(_ctx, getState());
		enterRule(_localctx, 28, RULE_op);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(177);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << GT) | (1L << LT) | (1L << EQUAL) | (1L << LE) | (1L << GE) | (1L << NOTEQUAL) | (1L << ADD) | (1L << SUB) | (1L << MUL) | (1L << DIV) | (1L << MOD) | (1L << AND) | (1L << OR))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class LopContext extends ParserRuleContext {
		public TerminalNode LA() { return getToken(JavaParser.LA, 0); }
		public TerminalNode LM() { return getToken(JavaParser.LM, 0); }
		public TerminalNode LD() { return getToken(JavaParser.LD, 0); }
		public TerminalNode LS() { return getToken(JavaParser.LS, 0); }
		public TerminalNode LMOD() { return getToken(JavaParser.LMOD, 0); }
		public TerminalNode ASSIGN() { return getToken(JavaParser.ASSIGN, 0); }
		public LopContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_lop; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).enterLop(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).exitLop(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JavaVisitor ) return ((JavaVisitor<? extends T>)visitor).visitLop(this);
			else return visitor.visitChildren(this);
		}
	}

	public final LopContext lop() throws RecognitionException {
		LopContext _localctx = new LopContext(_ctx, getState());
		enterRule(_localctx, 30, RULE_lop);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(179);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << ASSIGN) | (1L << LA) | (1L << LS) | (1L << LM) | (1L << LD) | (1L << LMOD))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Var_typeContext extends ParserRuleContext {
		public TerminalNode INT() { return getToken(JavaParser.INT, 0); }
		public TerminalNode DOUBLE() { return getToken(JavaParser.DOUBLE, 0); }
		public TerminalNode FLOAT() { return getToken(JavaParser.FLOAT, 0); }
		public Var_typeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_var_type; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).enterVar_type(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).exitVar_type(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JavaVisitor ) return ((JavaVisitor<? extends T>)visitor).visitVar_type(this);
			else return visitor.visitChildren(this);
		}
	}

	public final Var_typeContext var_type() throws RecognitionException {
		Var_typeContext _localctx = new Var_typeContext(_ctx, getState());
		enterRule(_localctx, 32, RULE_var_type);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(181);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << INT) | (1L << DOUBLE) | (1L << FLOAT))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Boolean_valContext extends ParserRuleContext {
		public TerminalNode TRUE() { return getToken(JavaParser.TRUE, 0); }
		public TerminalNode FALSE() { return getToken(JavaParser.FALSE, 0); }
		public Boolean_valContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_boolean_val; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).enterBoolean_val(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JavaListener ) ((JavaListener)listener).exitBoolean_val(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JavaVisitor ) return ((JavaVisitor<? extends T>)visitor).visitBoolean_val(this);
			else return visitor.visitChildren(this);
		}
	}

	public final Boolean_valContext boolean_val() throws RecognitionException {
		Boolean_valContext _localctx = new Boolean_valContext(_ctx, getState());
		enterRule(_localctx, 34, RULE_boolean_val);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(183);
			_la = _input.LA(1);
			if ( !(_la==TRUE || _la==FALSE) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public boolean sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 6:
			return expression_value_sempred((Expression_valueContext)_localctx, predIndex);
		}
		return true;
	}
	private boolean expression_value_sempred(Expression_valueContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0:
			return precpred(_ctx, 2);
		}
		return true;
	}

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3\63\u00bc\4\2\t\2"+
		"\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13"+
		"\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\3\2\3\2\3\2\3\2\3\2\3\2\3\2\3\3\3\3\3\3\3\3\3\3\3\3\7\3\64"+
		"\n\3\f\3\16\3\67\13\3\3\3\3\3\3\4\3\4\3\4\3\4\3\4\3\4\3\4\3\5\3\5\7\5"+
		"D\n\5\f\5\16\5G\13\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3"+
		"\5\3\5\3\5\3\5\5\5Y\n\5\3\6\3\6\3\6\3\6\5\6_\n\6\3\7\3\7\3\b\3\b\3\b\3"+
		"\b\3\b\3\b\3\b\3\b\3\b\3\b\3\b\3\b\3\b\3\b\5\bq\n\b\3\b\3\b\3\b\3\b\6"+
		"\bw\n\b\r\b\16\bx\7\b{\n\b\f\b\16\b~\13\b\3\t\3\t\3\t\5\t\u0083\n\t\3"+
		"\t\3\t\3\n\3\n\3\n\3\n\3\13\3\13\3\13\3\13\3\13\3\13\3\f\3\f\3\f\3\f\3"+
		"\f\3\f\3\f\3\r\3\r\3\r\3\r\3\r\3\r\3\r\5\r\u009f\n\r\3\16\3\16\3\16\3"+
		"\16\3\16\3\17\5\17\u00a7\n\17\3\17\5\17\u00aa\n\17\3\17\5\17\u00ad\n\17"+
		"\3\17\5\17\u00b0\n\17\5\17\u00b2\n\17\3\20\3\20\3\21\3\21\3\22\3\22\3"+
		"\23\3\23\3\23\2\3\16\24\2\4\6\b\n\f\16\20\22\24\26\30\32\34\36 \"$\2\7"+
		"\3\2\25\26\6\2\37#)*,,.\62\4\2\36\36$(\3\2\r\17\3\2\n\13\2\u00c1\2&\3"+
		"\2\2\2\4-\3\2\2\2\6:\3\2\2\2\bX\3\2\2\2\nZ\3\2\2\2\f`\3\2\2\2\16p\3\2"+
		"\2\2\20\177\3\2\2\2\22\u0086\3\2\2\2\24\u008a\3\2\2\2\26\u0090\3\2\2\2"+
		"\30\u0097\3\2\2\2\32\u00a0\3\2\2\2\34\u00b1\3\2\2\2\36\u00b3\3\2\2\2 "+
		"\u00b5\3\2\2\2\"\u00b7\3\2\2\2$\u00b9\3\2\2\2&\'\5\34\17\2\'(\7\b\2\2"+
		"()\7\3\2\2)*\7\33\2\2*+\5\4\3\2+,\7\34\2\2,\3\3\2\2\2-.\5\34\17\2./\7"+
		"\f\2\2/\60\7\3\2\2\60\61\5\6\4\2\61\65\7\33\2\2\62\64\5\b\5\2\63\62\3"+
		"\2\2\2\64\67\3\2\2\2\65\63\3\2\2\2\65\66\3\2\2\2\668\3\2\2\2\67\65\3\2"+
		"\2\289\7\34\2\29\5\3\2\2\2:;\7\31\2\2;<\7\6\2\2<=\7\27\2\2=>\7\30\2\2"+
		">?\7\3\2\2?@\7\32\2\2@\7\3\2\2\2AE\7\33\2\2BD\5\b\5\2CB\3\2\2\2DG\3\2"+
		"\2\2EC\3\2\2\2EF\3\2\2\2FH\3\2\2\2GE\3\2\2\2HY\7\34\2\2IJ\5\f\7\2JK\7"+
		"\35\2\2KY\3\2\2\2LM\5\n\6\2MN\7\35\2\2NY\3\2\2\2OP\5\32\16\2PQ\7\35\2"+
		"\2QY\3\2\2\2RY\5\24\13\2SY\5\30\r\2TU\5\26\f\2UV\7\35\2\2VY\3\2\2\2WY"+
		"\7\35\2\2XA\3\2\2\2XI\3\2\2\2XL\3\2\2\2XO\3\2\2\2XR\3\2\2\2XS\3\2\2\2"+
		"XT\3\2\2\2XW\3\2\2\2Y\t\3\2\2\2Z[\5\"\22\2[^\7\3\2\2\\]\7\36\2\2]_\5\16"+
		"\b\2^\\\3\2\2\2^_\3\2\2\2_\13\3\2\2\2`a\5\22\n\2a\r\3\2\2\2bc\b\b\1\2"+
		"cd\7\31\2\2de\5\16\b\2ef\7\32\2\2fq\3\2\2\2gq\5\f\7\2hq\7\3\2\2iq\7\4"+
		"\2\2jq\5$\23\2kl\5\20\t\2lm\7\31\2\2mn\5\16\b\2no\7\32\2\2oq\3\2\2\2p"+
		"b\3\2\2\2pg\3\2\2\2ph\3\2\2\2pi\3\2\2\2pj\3\2\2\2pk\3\2\2\2q|\3\2\2\2"+
		"rv\f\4\2\2st\5\36\20\2tu\5\16\b\2uw\3\2\2\2vs\3\2\2\2wx\3\2\2\2xv\3\2"+
		"\2\2xy\3\2\2\2y{\3\2\2\2zr\3\2\2\2{~\3\2\2\2|z\3\2\2\2|}\3\2\2\2}\17\3"+
		"\2\2\2~|\3\2\2\2\177\u0082\7\31\2\2\u0080\u0083\5\20\t\2\u0081\u0083\5"+
		"\"\22\2\u0082\u0080\3\2\2\2\u0082\u0081\3\2\2\2\u0083\u0084\3\2\2\2\u0084"+
		"\u0085\7\32\2\2\u0085\21\3\2\2\2\u0086\u0087\7\3\2\2\u0087\u0088\5 \21"+
		"\2\u0088\u0089\5\16\b\2\u0089\23\3\2\2\2\u008a\u008b\7\22\2\2\u008b\u008c"+
		"\7\31\2\2\u008c\u008d\5\16\b\2\u008d\u008e\7\32\2\2\u008e\u008f\5\b\5"+
		"\2\u008f\25\3\2\2\2\u0090\u0091\7\23\2\2\u0091\u0092\5\b\5\2\u0092\u0093"+
		"\7\22\2\2\u0093\u0094\7\31\2\2\u0094\u0095\5\16\b\2\u0095\u0096\7\32\2"+
		"\2\u0096\27\3\2\2\2\u0097\u0098\7\24\2\2\u0098\u0099\7\31\2\2\u0099\u009a"+
		"\5\16\b\2\u009a\u009b\7\32\2\2\u009b\u009e\5\b\5\2\u009c\u009d\7\21\2"+
		"\2\u009d\u009f\5\b\5\2\u009e\u009c\3\2\2\2\u009e\u009f\3\2\2\2\u009f\31"+
		"\3\2\2\2\u00a0\u00a1\t\2\2\2\u00a1\u00a2\7\31\2\2\u00a2\u00a3\5\16\b\2"+
		"\u00a3\u00a4\7\32\2\2\u00a4\33\3\2\2\2\u00a5\u00a7\7\t\2\2\u00a6\u00a5"+
		"\3\2\2\2\u00a6\u00a7\3\2\2\2\u00a7\u00a9\3\2\2\2\u00a8\u00aa\7\7\2\2\u00a9"+
		"\u00a8\3\2\2\2\u00a9\u00aa\3\2\2\2\u00aa\u00b2\3\2\2\2\u00ab\u00ad\7\7"+
		"\2\2\u00ac\u00ab\3\2\2\2\u00ac\u00ad\3\2\2\2\u00ad\u00af\3\2\2\2\u00ae"+
		"\u00b0\7\t\2\2\u00af\u00ae\3\2\2\2\u00af\u00b0\3\2\2\2\u00b0\u00b2\3\2"+
		"\2\2\u00b1\u00a6\3\2\2\2\u00b1\u00ac\3\2\2\2\u00b2\35\3\2\2\2\u00b3\u00b4"+
		"\t\3\2\2\u00b4\37\3\2\2\2\u00b5\u00b6\t\4\2\2\u00b6!\3\2\2\2\u00b7\u00b8"+
		"\t\5\2\2\u00b8#\3\2\2\2\u00b9\u00ba\t\6\2\2\u00ba%\3\2\2\2\20\65EX^px"+
		"|\u0082\u009e\u00a6\u00a9\u00ac\u00af\u00b1";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}