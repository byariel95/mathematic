using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathExpressionsCSharp.mathparser
{
    public class Parser
    {

        private char[] expression = null;

        private TOKEN_TYPE tipoTokenActual = TOKEN_TYPE.NADA;

        private int i = 0;
        private String token = "";
        private double respuestaNumerica = 0.0;
        private Variables userVar;

        public Parser()
        {
            tipoTokenActual = TOKEN_TYPE.NADA;
            userVar = new Variables();
        }

        public double RespuestaNumerica
        {
            get {
                return respuestaNumerica;
            }
        }

        public Variables UserVars
        {
            get {
                return userVar;
            }
        }

        private void init(string expr)
        {
            expr = expr.Replace(" ", "");
            expr = expr.Replace("\t", "");
            expr = expr.Replace("\r\n", "");
            expr = expr.Replace("\r\n", "");

            this.expression = (expr + '\u0000').ToCharArray();

            i = 0;
            respuestaNumerica = 0.0;
            tipoTokenActual = TOKEN_TYPE.NADA;

        }

        public void Parse(string expressionStr)
        {

            if (expressionStr.Length < 1)
            {
                throw new ParsingException("Expressión vacía", 0);
            }

            init(expressionStr);
            getToken();

            if (expression.Length < 1)
            {
                throw new ParsingException("Expresión vacía", 0);
            }

            respuestaNumerica = parseLevel1();

            if (tipoTokenActual != TOKEN_TYPE.DELIMITADOR)
            {
                throw new ParsingException("Parte no esperada: " + token, i);
            }

            userVar.AddVar("ans", respuestaNumerica);
        }

        private double parseLevel1()
        {

            if (tipoTokenActual == TOKEN_TYPE.VARIABLE)
            {
                TOKEN_TYPE tokenTemp = tipoTokenActual;
                String tokenNow = token;/*new String(token);*/

                int temp_index = i;

                getToken();
                if (token.Equals("="))
                {
                    if (isFunction(tokenNow))
                    {
                        throw new ParsingException("Identificador como palabra reservada (" + tokenNow + ")", i);
                    }
                    else if (userVar.isConstant(tokenNow))
                    {
                        throw new ParsingException("Asignación de constante: " + tokenNow, i);
                    }
                    else if (tokenNow.Equals("e"))
                    {
                        throw new ParsingException("Identificador como palabra reservada (" + tokenNow + ")", i);
                    }
                    else if (tokenNow.Equals("pi"))
                    {
                        throw new ParsingException("Identificador como palabra reservada (" + tokenNow + ")", i);
                    }
                    else if (tokenNow.Equals("g"))
                    {
                        throw new ParsingException("Identificador como palabra reservada (" + tokenNow + ")", i);
                    }
                    else if (tokenNow.Equals("random"))
                    {
                        throw new ParsingException("Identificador como palabra reservada (" + tokenNow + ")", i);
                    }
                    getToken();

                    double r_temp = parseLevel2();
                    if (userVar.AddVar(tokenNow, r_temp) == false)
                    {
                        throw new ParsingException("Definición de variable fallida", i);
                    }
                    else
                    {
                        return r_temp;
                    }
                }
                else
                {
                    // No era una asignación, hay que recuperar el índex:
                    i = temp_index;
                    token = tokenNow;
                    tipoTokenActual = tokenTemp;
                }
            }
            return parseLevel2();
        }

        private double parseLevel2()
        {

            double answer = parseLevel3();

            OPERADOR_ID op_id = getOperatorId(token);

            while ((op_id == OPERADOR_ID.AND) || (op_id == OPERADOR_ID.OR) ||
                        (op_id == OPERADOR_ID.BITSHIFTLEFT) || (op_id == OPERADOR_ID.BITSHIFTRIGHT))
            {
                getToken();
                answer = eval_operator(op_id, answer, parseLevel3());
                op_id = getOperatorId(token);
            }
            return answer;
        }

        private double parseLevel3()
        {

            double answer = parseLevel4();
            OPERADOR_ID op_id = getOperatorId(token);

            while ((op_id == OPERADOR_ID.EQUAL) || (op_id == OPERADOR_ID.UNEQUAL) ||
                    (op_id == OPERADOR_ID.SMALLER) ||
                      (op_id == OPERADOR_ID.LARGER) || (op_id == OPERADOR_ID.SMALLEREQ) || (op_id == OPERADOR_ID.LARGEREQ))
            {
                getToken();
                answer = eval_operator(op_id, answer, parseLevel4());
                op_id = getOperatorId(token);
            }
            return answer;
        }

        private double parseLevel4()
        {
            double answer = parseLevel5();
            OPERADOR_ID op_id = getOperatorId(token);

            while (op_id == OPERADOR_ID.PLUS || op_id == OPERADOR_ID.MINUS)
            {
                getToken();

                // XXX Eliminar si hay problemas!
                /*
                    El siguiente trozo de código, evita que haya expresiones del tipo:
                    1+-2
                    1--2
                */

                if (string.IsNullOrEmpty(token)/*token.isEmpty()*/)
                {
                    throw new ParsingException("Error de sintáxis: " + i);
                }

                if (token[0] == '-')
                {
                    throw new ParsingException("Parte no esperadas: " +
                            token, i);
                }

                answer = eval_operator(op_id, answer, parseLevel5());
                op_id = getOperatorId(token);
            }
            return answer;
        }

        private double parseLevel5()
        {
            double answer = parseLevel6();
            OPERADOR_ID op_id = getOperatorId(token);

            while ((op_id == OPERADOR_ID.MULTIPLY) || (op_id == OPERADOR_ID.DIVIDE) || (op_id == OPERADOR_ID.MODULUS) || (op_id == OPERADOR_ID.XOR))
            {
                getToken();
                answer = eval_operator(op_id, answer, parseLevel6());
                op_id = getOperatorId(token);
            }

            return answer;
        }

        private double parseLevel6()
        {
            double answer = parseLevel8();
            OPERADOR_ID op_id = getOperatorId(token);

            while (op_id == OPERADOR_ID.POW)
            {
                getToken();
                answer = eval_operator(op_id, answer, parseLevel8());
                op_id = getOperatorId(token);
            }

            return answer;
        }

        // private double parseLevel7() {return 0.0;}

        private double parseLevel8()
        {
            double answer;

            OPERADOR_ID op_id = getOperatorId(token);
            if (op_id == OPERADOR_ID.MINUS)
            {
                getToken();
                answer = parse_not();
                answer = -answer;
            } /*else if(op_id == NOT) {
				getToken();
				answer = parse_level2();
				answer = !answer;
			}*/ else
            {
                answer = parse_not();
            }

            return answer;
        }

        private double parse_not()
        {
            double answer = 0.0;
            OPERADOR_ID op_id = getOperatorId(token);

            if (op_id == OPERADOR_ID.NOT)
            {

                getToken();
                answer = parseLevel9();
                //answer = !(answer);
                // @todo Aguas!:
                answer = (answer != 0.0) ? 0.0 : 1.0;
            }
            else
            {
                answer = parseLevel9();
            }
            return answer;
        }

        private double parseLevel9()
        {

            double answer;

            if (tipoTokenActual == TOKEN_TYPE.FUNCION)
            {
                // Copiamos el nombre de la función:
                String fn_name = token.ToUpper();

                if (isFunction(fn_name) == false)
                {
                    throw new ParsingException("Función desconocida: " +
                            fn_name, i);
                }
                if (isFunctionDouble(fn_name))
                {
                    // Avanzar al siguiente token, avanzar el delimitador '(':
                    getToken();
                    // Avanzamos a la expresion:
                    getToken();

                    double expresion_1 = parseLevel2();
                    getToken();
                    double expresion_2 = parseLevel2();

                    answer = eval_function_double(fn_name, expresion_1,
                            expresion_2);

                    if (!token.Equals(")"))
                    {
                        throw new ParsingException("Se esperaba paréntesis de cierre", i);
                    }
                }
                else
                {
                    getToken();
                    double expresionFunction = parseLevel10();
                    answer = eval_function(fn_name, expresionFunction);
                }
            }
            else
            {
                answer = parseLevel10();
            }

            return answer;
        }

        private double parseLevel10()
        {
            if (tipoTokenActual == TOKEN_TYPE.DELIMITADOR)
            {

                if (string.IsNullOrEmpty(token)/*token.isEmpty()*/)
                {
                    throw new ParsingException("Final inesperado de expresión");
                }

                if (token[0] == '(')
                {
                    getToken();

                    double answer = parseLevel2();
                    if (string.IsNullOrEmpty(token)/*token.isEmpty()*/)
                    {
                        throw new ParsingException("Paréntesis faltante", i);
                    }

                    if (tipoTokenActual != TOKEN_TYPE.DELIMITADOR)
                    {
                        throw new ParsingException("Paréntesis faltante", i);
                    }
                    getToken();
                    return answer;
                }
            }
            return parseNumber();
        }

        private double parseNumber()
        {
            double answer = 0.0;
            switch (tipoTokenActual)
            {
                case TOKEN_TYPE.NUMERO:
                    answer = double.Parse(token);
                    getToken();
                    break;

                case TOKEN_TYPE.VARIABLE:
                    answer = eval_variable(token);
                    getToken();
                    break;

                default:
                    if (token[0] == '\u0000' || token.Length < 1)
                    {
                        throw new ParsingException("Fin inesperado de expresión", i);
                    }
                    else
                    {
                        throw new ParsingException("Valor esperado", i);
                    }
            }
            return answer;
        }

        private void getToken()
        {

            tipoTokenActual = TOKEN_TYPE.NADA;
            token = "";

            if (expression[i] == ' ' || expression[i] == '\t' ||
                    expression[i] == '\n')
            {
                i++;
            }

            if (expression[i] == '\u0000')
            {
                tipoTokenActual = TOKEN_TYPE.DELIMITADOR;
                return;
            }

            if (expression[i] == '-')
            {
                tipoTokenActual = TOKEN_TYPE.DELIMITADOR;
                token += '-';
                i++;
                return;
            }

            // Paréntesis:
            if (expression[i] == ')' || expression[i] == '(')
            {
                tipoTokenActual = TOKEN_TYPE.DELIMITADOR;
                token += expression[i];
                i++;
                return;
            }

            if (isDelimeter(expression[i]))
            {
                tipoTokenActual = TOKEN_TYPE.DELIMITADOR;
                while (isDelimeter(expression[i]))
                {
                    token += expression[i];
                    i++;
                }

                // == !1
                // x ==!0
                if (token.Length > 2)
                {
                    token = token.Substring(0, 2);
                    i--;
                }
                //token = token + '\u0000';
                return;
                // Cuidado con el NOT!
            }

            if (isDigitDot(expression[i]))
            {
                tipoTokenActual = TOKEN_TYPE.NUMERO;
                while (isDigit(expression[i]))
                {
                    token += expression[i];
                    i++;
                }
                if (expression[i] == '.')
                {
                    token += '.';
                    i++;
                }
                while (isDigit(expression[i]))
                {
                    token += expression[i];
                    i++;
                }
                if (Char.ToUpper(expression[i]) == 'E')
                {
                    token += 'E';
                    i++;

                    if (expression[i] == '+' || expression[i] == '-')
                    {
                        token += expression[i];
                        i++;
                    }

                    while (isDigit(expression[i]))
                    {
                        token += expression[i];
                        i++;
                    }

                }
                return;
            }
            if (isAlpha(expression[i]))
            {
                while (isAlpha(expression[i]) || isDigit(expression[i]))
                {
                    token += expression[i];
                    i++;
                }
                // Verificar si es función o variable:
                if (expression[i] == '(')
                {
                    tipoTokenActual = TOKEN_TYPE.FUNCION;
                }
                else
                {
                    //System.out.println("Var: " + token);
                    tipoTokenActual = TOKEN_TYPE.VARIABLE;
                }
                return;
            }

            tipoTokenActual = TOKEN_TYPE.NADA;

            int colError = i;

            // ERROR .... Ver qué hacer:
            while (expression[i] != '\u0000')
            {
                token += expression[i];
                i++;
            }
            throw new ParsingException("Error en parte: [" + token + ']', colError);
        }

        private OPERADOR_ID getOperatorId(string op)
        {
            switch (op)
            {
                case "&&":
                    return OPERADOR_ID.AND;
                case "|":
                    return OPERADOR_ID.OR;
                case "<<":
                    return OPERADOR_ID.BITSHIFTLEFT;
                case ">>":
                    return OPERADOR_ID.BITSHIFTRIGHT;
                case "==":
                    return OPERADOR_ID.EQUAL;

                case "!=":
                    return OPERADOR_ID.UNEQUAL;
                case "<":
                    return OPERADOR_ID.SMALLER;
                case ">":
                    return OPERADOR_ID.LARGER;
                case "<=":
                    return OPERADOR_ID.SMALLEREQ;
                case ">=":
                    return OPERADOR_ID.LARGEREQ;
                case "+":
                    return OPERADOR_ID.PLUS;
                case "-":
                    return OPERADOR_ID.MINUS;
                case "*":
                    return OPERADOR_ID.MULTIPLY;
                case "/":
                    return OPERADOR_ID.DIVIDE;
                case "%":
                    return OPERADOR_ID.MODULUS;
                case "||":
                    return OPERADOR_ID.MODULUS;
                case "^":
                    return OPERADOR_ID.POW;
                case "!":
                    return OPERADOR_ID.NOT;
            }
            return OPERADOR_ID.UNKNOWN;
        }

        private double eval_operator(OPERADOR_ID op_id, double lhs, double rhs)
        {

            switch (op_id)
            {
                case OPERADOR_ID.AND:
                    return boolToDouble(toBool((int)lhs) && toBool((int)rhs));
                case OPERADOR_ID.OR:
                    return boolToDouble(toBool((int)lhs) || toBool((int)rhs));
                case OPERADOR_ID.BITSHIFTLEFT:
                    return (int)lhs << (int)rhs;
                case OPERADOR_ID.BITSHIFTRIGHT:
                    return (int)lhs >> (int)rhs;
                case OPERADOR_ID.EQUAL:
                    return (lhs == rhs ? 1.0 : 0.0);
                case OPERADOR_ID.UNEQUAL:
                    return (lhs != rhs ? 1.0 : 0.0);
                case OPERADOR_ID.SMALLER:
                    return (lhs < rhs ? 1.0 : 0.0);
                case OPERADOR_ID.LARGER:
                    return (lhs > rhs ? 1.0 : 0.0);
                case OPERADOR_ID.SMALLEREQ:
                    return (lhs <= rhs ? 1.0 : 0.0);
                case OPERADOR_ID.LARGEREQ:
                    return (lhs >= rhs ? 1.0 : 0.0);
                case OPERADOR_ID.PLUS:
                    return lhs + rhs;
                case OPERADOR_ID.MINUS:
                    return lhs - rhs;
                case OPERADOR_ID.MULTIPLY:
                    return lhs * rhs;
                case OPERADOR_ID.DIVIDE:
                    return lhs / rhs;
                case OPERADOR_ID.MODULUS:
                    return lhs % rhs;
                case OPERADOR_ID.XOR:
                    return (int)lhs ^ (int)rhs;
                case OPERADOR_ID.POW:
                    return Math.Pow(lhs, rhs);
            }

            throw new ParsingException("Operador: " + op_id + " desconocido", i);
        }

        private double eval_function_double(string fn_name,
                double param_left, double param_right)
        {

            switch (fn_name)
            {
                case "POWER":
                    return Math.Pow(param_left, param_right);
                case "MAX":
                    return Math.Max(param_left, param_right);
                case "MIN":
                    return Math.Min(param_left, param_right);
                case "MOD":
                    return param_left % param_right;
                case "RAND":
                    return MathFunctions.rand_int_between((int)param_left,
                            (int)param_right);
            }
            throw new ParsingException("Función desconocida: " + fn_name, i);
        }

        private double eval_function(string fn_name, double value)
        {

            switch (fn_name.ToUpper())
            {

                case "ABS":
                    return Math.Abs(value);

                case "EXP":
                    return Math.Exp(value);

                case "SIGN":
                    return Math.Sign(value);

                case "SQRT":
                case "RAIZ":
                    double __temp = Math.Sqrt(value);
                    if (Double.IsNaN(__temp))
                    {
                        throw new ParsingException("Intentando evaluar raíz para número negativo: " + value, i);
                    }
                    return __temp;

                case "SIN":
                    return MathFunctions.math_sin(value);

                case "COS":
                    return Math.Cos(value);

                case "FACTORIAL":
                    return MathFunctions.factorial(value);

                case "LOG":
                case "LN":
                    return MathFunctions.math_log(value);

                case "LOG10":
                    return MathFunctions.math_log10(value);

                case "LOG2":
                    return MathFunctions.math_log2(value);

                case "TAN":
                    return MathFunctions.math_tan(value);

                case "ASIN":
                    return MathFunctions.math_asin(value);

                case "ACOS":
                    return MathFunctions.math_acos(value);

                case "ASEC":
                    return MathFunctions.math_asec(value);

                case "COT":
                    return MathFunctions.math_cot(value);

                case "SEC":
                    return MathFunctions.math_sec(value);

                case "CSC":
                    return MathFunctions.math_csc(value);

                case "ATAN":
                    return MathFunctions.math_atan(value);

                case "SINH":
                    return Math.Sinh(value);

                case "ASINH":
                    return MathFunctions.math_asinh(value);

                case "ACOSH":
                    return MathFunctions.math_acosh(value);

                case "ASECH":
                    return MathFunctions.math_asech(value);

                case "COSH":
                    return Math.Cosh(value);

                case "TANH":
                    return Math.Tanh(value);

                case "COTH":
                    return MathFunctions.math_coth(value);

                case "SECH":
                    return MathFunctions.math_sech(value);

                case "CSCH":
                    return MathFunctions.math_csch(value);

                case "ACSCH":
                    return MathFunctions.math_acsch(value);

                case "ATANH":
                    return MathFunctions.math_atanh(value);

                case "ACOT":
                    return MathFunctions.math_acot(value);

                case "ACOTH":
                    return MathFunctions.math_acoth(value);

                case "ACSC":
                    return MathFunctions.math_acsc(value);

                case "RAND":
                    return MathFunctions.rand_0_to_1();
            }
            throw new ParsingException("Función desconocida: " + fn_name, i);
        }

        private double eval_variable(string var_name)
        {

            switch (var_name.ToUpper())
            {
                case "E":
                    return Math.E;
                case "PI":
                    return Math.PI;
                case "G":
                    return 9.80665;
                case "RANDOM":
                    return new Random(DateTime.Now.Millisecond).NextDouble();
            }

            double ans;

            if (userVar.ExistVar(var_name))
            {
                ans = userVar.GetValueVar(var_name);
                return ans;
            }

            throw new ParsingException("Variable desconocida: [" + var_name +
                    "]", i);
        }

        private double boolToDouble(bool value)
        {
            return value == true ? 1.0 : 0.0;
        }

        private static bool toBool(int val)
        {
            return val != 0;
        }

        private enum TOKEN_TYPE
        {
            NADA, DELIMITADOR, NUMERO, VARIABLE, FUNCION, DESCONOCIDO, OPERADOR
        };

        // Tipos de operadores:
        private enum OPERADOR_ID
        {
            AND,				// nivel2
            OR,					// nivel2
            BITSHIFTLEFT,		// nivel2               , sin utilidad.
            BITSHIFTRIGHT,		// nivel2               , sin utilidad.
            EQUAL,				// nivel3
            UNEQUAL, 			// nivel3
            SMALLER, 			// nivel3
            LARGER, 			// nivel3
            SMALLEREQ, 			// nivel3
            LARGEREQ, 			// nivel3
            PLUS, 				// nivel4
            MINUS, 				// nivel4
            MULTIPLY, 			// nivel5
            DIVIDE, 			// nivel5
            MODULUS, 			// nivel5
            XOR, 				// nivel5
            POW, 				// nivel6
            FACTORIAL,			// nivel7
            NOT                 // nivel not  XXX NOT
            , UNKNOWN
        };

        private bool isDelimeter(char c)
        {
            return "&|<>=+/*%^!,".IndexOf(c) != -1;
        }

        private bool isDigitDot(char c)
        {
            return "0123456789.".IndexOf(c) != -1;
        }

        private bool isDigit(char c)
        {
            return "0123456789".IndexOf(c) != -1;
        }

        private bool isAlpha(char c)
        {
            return "ABCDEFGHIJKLMNOPQRSTUVWXYZ_".IndexOf(Char.ToUpper(c)) != -1;
        }

        private bool isFunctionDouble(string functionName)
        {

            bool result = false;

            switch (functionName.ToUpper())
            {
                case "POWER":
                case "SUMA":
                case "MIN":
                case "MAX":
                case "MOD":
                case "RAND":
                    result = true;
                    break;
                default:
                    break;
            }
            return result;
        }

        private bool isFunction(string functionName)
        {

            bool result = false;

            switch (functionName.ToUpper())
            {
                case "ABS":
                case "EXP":
                case "SIGN":
                case "SQRT":
                case "RAIZ":
                case "LOG":
                case "LN":
                case "LOG10":
                case "SIN":
                case "COS":
                case "TAN":
                case "ASIN":
                case "ACOS":
                case "ATAN":
                case "FACTORIAL":
                case "COT":
                case "SEC":
                case "CSC":
                case "SINH":
                case "COSH":
                case "TANH":
                case "COTH":
                case "SECH":
                case "CSCH":
                case "ACSC":
                case "ASEC":
                case "ASINH":
                case "ACOSH":
                case "ACSCH":
                case "ATANH":
                case "POWER":
                case "MAX":
                case "MIN":
                case "MOD":
                case "ACOT":
                case "ACOTH":
                case "LOG2":
                case "RAND":
                case "ASECH":
                    //return true;
                    result = true;
                    break;
            }
            return result;
        }

    }

}
