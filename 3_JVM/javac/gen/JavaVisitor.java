// Generated from D:/disk/Projects/ifmo_translation_methods/3_JVM/javac\Java.g4 by ANTLR 4.7
import org.antlr.v4.runtime.tree.ParseTreeVisitor;

/**
 * This interface defines a complete generic visitor for a parse tree produced
 * by {@link JavaParser}.
 *
 * @param <T> The return type of the visit operation. Use {@link Void} for
 * operations with no return type.
 */
public interface JavaVisitor<T> extends ParseTreeVisitor<T> {
	/**
	 * Visit a parse tree produced by {@link JavaParser#compileUnit}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitCompileUnit(JavaParser.CompileUnitContext ctx);
	/**
	 * Visit a parse tree produced by {@link JavaParser#main_method}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitMain_method(JavaParser.Main_methodContext ctx);
	/**
	 * Visit a parse tree produced by {@link JavaParser#main_args}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitMain_args(JavaParser.Main_argsContext ctx);
	/**
	 * Visit a parse tree produced by {@link JavaParser#statement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitStatement(JavaParser.StatementContext ctx);
	/**
	 * Visit a parse tree produced by {@link JavaParser#var_decl}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitVar_decl(JavaParser.Var_declContext ctx);
	/**
	 * Visit a parse tree produced by {@link JavaParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitExpression(JavaParser.ExpressionContext ctx);
	/**
	 * Visit a parse tree produced by {@link JavaParser#expression_value}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitExpression_value(JavaParser.Expression_valueContext ctx);
	/**
	 * Visit a parse tree produced by {@link JavaParser#cast}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitCast(JavaParser.CastContext ctx);
	/**
	 * Visit a parse tree produced by {@link JavaParser#assignment}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitAssignment(JavaParser.AssignmentContext ctx);
	/**
	 * Visit a parse tree produced by {@link JavaParser#while}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitWhile(JavaParser.WhileContext ctx);
	/**
	 * Visit a parse tree produced by {@link JavaParser#do_while}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitDo_while(JavaParser.Do_whileContext ctx);
	/**
	 * Visit a parse tree produced by {@link JavaParser#if}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitIf(JavaParser.IfContext ctx);
	/**
	 * Visit a parse tree produced by {@link JavaParser#print_call}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitPrint_call(JavaParser.Print_callContext ctx);
	/**
	 * Visit a parse tree produced by {@link JavaParser#public_or_static}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitPublic_or_static(JavaParser.Public_or_staticContext ctx);
	/**
	 * Visit a parse tree produced by {@link JavaParser#op}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitOp(JavaParser.OpContext ctx);
	/**
	 * Visit a parse tree produced by {@link JavaParser#lop}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitLop(JavaParser.LopContext ctx);
	/**
	 * Visit a parse tree produced by {@link JavaParser#var_type}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitVar_type(JavaParser.Var_typeContext ctx);
	/**
	 * Visit a parse tree produced by {@link JavaParser#boolean_val}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitBoolean_val(JavaParser.Boolean_valContext ctx);
}