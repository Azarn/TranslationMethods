// Generated from D:/disk/Projects/ifmo_translation_methods/3_JVM/javac\Java.g4 by ANTLR 4.7
import org.antlr.v4.runtime.tree.ParseTreeListener;

/**
 * This interface defines a complete listener for a parse tree produced by
 * {@link JavaParser}.
 */
public interface JavaListener extends ParseTreeListener {
	/**
	 * Enter a parse tree produced by {@link JavaParser#compileUnit}.
	 * @param ctx the parse tree
	 */
	void enterCompileUnit(JavaParser.CompileUnitContext ctx);
	/**
	 * Exit a parse tree produced by {@link JavaParser#compileUnit}.
	 * @param ctx the parse tree
	 */
	void exitCompileUnit(JavaParser.CompileUnitContext ctx);
	/**
	 * Enter a parse tree produced by {@link JavaParser#main_method}.
	 * @param ctx the parse tree
	 */
	void enterMain_method(JavaParser.Main_methodContext ctx);
	/**
	 * Exit a parse tree produced by {@link JavaParser#main_method}.
	 * @param ctx the parse tree
	 */
	void exitMain_method(JavaParser.Main_methodContext ctx);
	/**
	 * Enter a parse tree produced by {@link JavaParser#main_args}.
	 * @param ctx the parse tree
	 */
	void enterMain_args(JavaParser.Main_argsContext ctx);
	/**
	 * Exit a parse tree produced by {@link JavaParser#main_args}.
	 * @param ctx the parse tree
	 */
	void exitMain_args(JavaParser.Main_argsContext ctx);
	/**
	 * Enter a parse tree produced by {@link JavaParser#statement}.
	 * @param ctx the parse tree
	 */
	void enterStatement(JavaParser.StatementContext ctx);
	/**
	 * Exit a parse tree produced by {@link JavaParser#statement}.
	 * @param ctx the parse tree
	 */
	void exitStatement(JavaParser.StatementContext ctx);
	/**
	 * Enter a parse tree produced by {@link JavaParser#var_decl}.
	 * @param ctx the parse tree
	 */
	void enterVar_decl(JavaParser.Var_declContext ctx);
	/**
	 * Exit a parse tree produced by {@link JavaParser#var_decl}.
	 * @param ctx the parse tree
	 */
	void exitVar_decl(JavaParser.Var_declContext ctx);
	/**
	 * Enter a parse tree produced by {@link JavaParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterExpression(JavaParser.ExpressionContext ctx);
	/**
	 * Exit a parse tree produced by {@link JavaParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitExpression(JavaParser.ExpressionContext ctx);
	/**
	 * Enter a parse tree produced by {@link JavaParser#expression_value}.
	 * @param ctx the parse tree
	 */
	void enterExpression_value(JavaParser.Expression_valueContext ctx);
	/**
	 * Exit a parse tree produced by {@link JavaParser#expression_value}.
	 * @param ctx the parse tree
	 */
	void exitExpression_value(JavaParser.Expression_valueContext ctx);
	/**
	 * Enter a parse tree produced by {@link JavaParser#cast}.
	 * @param ctx the parse tree
	 */
	void enterCast(JavaParser.CastContext ctx);
	/**
	 * Exit a parse tree produced by {@link JavaParser#cast}.
	 * @param ctx the parse tree
	 */
	void exitCast(JavaParser.CastContext ctx);
	/**
	 * Enter a parse tree produced by {@link JavaParser#assignment}.
	 * @param ctx the parse tree
	 */
	void enterAssignment(JavaParser.AssignmentContext ctx);
	/**
	 * Exit a parse tree produced by {@link JavaParser#assignment}.
	 * @param ctx the parse tree
	 */
	void exitAssignment(JavaParser.AssignmentContext ctx);
	/**
	 * Enter a parse tree produced by {@link JavaParser#while}.
	 * @param ctx the parse tree
	 */
	void enterWhile(JavaParser.WhileContext ctx);
	/**
	 * Exit a parse tree produced by {@link JavaParser#while}.
	 * @param ctx the parse tree
	 */
	void exitWhile(JavaParser.WhileContext ctx);
	/**
	 * Enter a parse tree produced by {@link JavaParser#do_while}.
	 * @param ctx the parse tree
	 */
	void enterDo_while(JavaParser.Do_whileContext ctx);
	/**
	 * Exit a parse tree produced by {@link JavaParser#do_while}.
	 * @param ctx the parse tree
	 */
	void exitDo_while(JavaParser.Do_whileContext ctx);
	/**
	 * Enter a parse tree produced by {@link JavaParser#if}.
	 * @param ctx the parse tree
	 */
	void enterIf(JavaParser.IfContext ctx);
	/**
	 * Exit a parse tree produced by {@link JavaParser#if}.
	 * @param ctx the parse tree
	 */
	void exitIf(JavaParser.IfContext ctx);
	/**
	 * Enter a parse tree produced by {@link JavaParser#print_call}.
	 * @param ctx the parse tree
	 */
	void enterPrint_call(JavaParser.Print_callContext ctx);
	/**
	 * Exit a parse tree produced by {@link JavaParser#print_call}.
	 * @param ctx the parse tree
	 */
	void exitPrint_call(JavaParser.Print_callContext ctx);
	/**
	 * Enter a parse tree produced by {@link JavaParser#public_or_static}.
	 * @param ctx the parse tree
	 */
	void enterPublic_or_static(JavaParser.Public_or_staticContext ctx);
	/**
	 * Exit a parse tree produced by {@link JavaParser#public_or_static}.
	 * @param ctx the parse tree
	 */
	void exitPublic_or_static(JavaParser.Public_or_staticContext ctx);
	/**
	 * Enter a parse tree produced by {@link JavaParser#op}.
	 * @param ctx the parse tree
	 */
	void enterOp(JavaParser.OpContext ctx);
	/**
	 * Exit a parse tree produced by {@link JavaParser#op}.
	 * @param ctx the parse tree
	 */
	void exitOp(JavaParser.OpContext ctx);
	/**
	 * Enter a parse tree produced by {@link JavaParser#lop}.
	 * @param ctx the parse tree
	 */
	void enterLop(JavaParser.LopContext ctx);
	/**
	 * Exit a parse tree produced by {@link JavaParser#lop}.
	 * @param ctx the parse tree
	 */
	void exitLop(JavaParser.LopContext ctx);
	/**
	 * Enter a parse tree produced by {@link JavaParser#var_type}.
	 * @param ctx the parse tree
	 */
	void enterVar_type(JavaParser.Var_typeContext ctx);
	/**
	 * Exit a parse tree produced by {@link JavaParser#var_type}.
	 * @param ctx the parse tree
	 */
	void exitVar_type(JavaParser.Var_typeContext ctx);
	/**
	 * Enter a parse tree produced by {@link JavaParser#boolean_val}.
	 * @param ctx the parse tree
	 */
	void enterBoolean_val(JavaParser.Boolean_valContext ctx);
	/**
	 * Exit a parse tree produced by {@link JavaParser#boolean_val}.
	 * @param ctx the parse tree
	 */
	void exitBoolean_val(JavaParser.Boolean_valContext ctx);
}