grammar Algebra;

//token definitions

//fragment is a kind of a function
fragment DIGIT: [0-9];
PLUS: '+';
MINUS: '-';
MULTIPLY: '*';
DIVIDE: '/';
POWER: '^';
 
//token definition for integer number. Recognizes number of any length
INTEGER: DIGIT+;

//token definition for floating point number 
FLOAT: DIGIT+ '.' DIGIT+;


//parsing rules definitions
//root expression to parse, this is the starting point of the parser
root: expression EOF;

//recusive rule to parse types of expressions we support
expression:
	(INTEGER | FLOAT)												#NumberExpression			|
	'-' num = expression											#NegativeNumberExpression	|
	num = expression POWER pow = expression							#PowerExpression			|
	'(' expression ')'												#ParenthesisExpression		|

	//notice that we put handling of multiplication and division first and then addition and substraction
	//this is needed to establish operator precedence 
	//In this way, for expression '2 + 3 * 5', first 3*5 will be evaluated and then 'result' + 2
	left = expression op = (MULTIPLY | DIVIDE) right = expression	#MultOrDivideExpression		|
	left = expression op = (PLUS | MINUS) right = expression		#PlusOrMinusExpression		
	;

