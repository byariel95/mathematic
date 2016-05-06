using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathExpressionsCSharp.mathparser
{
    public class MathFunctions {

    private static Random random = new Random(DateTime.Now.Millisecond);
    
    private MathFunctions() {}
    
    private const double EPSILON = 1.0E-15;

    public static double math_sin(double x) {
        return (((x % Math.PI) - 0.0) < EPSILON) ? 0.0 : Math.Sin(x);
    }

    public static double factorial(double value) {

        double res;
        int v = (int) value;

        if (v < 0.0) {
            throw new ParsingException("Valor enterado esperado para la función factorial");
        }

        res = v;
        v--;
        while (v > 1) {
            res *= v;
            v--;
        }

        if (res == 0) {
            res = 1.0;        // 0! is per definition 1
        }
        
        if(Double.IsNaN(res) || Double.IsInfinity(res)) {
            throw new ParsingException("Número fuera de los límites de un double.");
        }
        return res;
    }

    public static double logarithm(double x) {
        double result = Math.Log(x);

        if (Double.IsInfinity(result) || Double.IsNaN(result)) {
            throw new ParsingException("Número fuera de dominio: " + result);
        }
        return result;
    }

    public static double math_acot(double x) {
        return ((Math.PI / 2.0) - Math.Abs(Math.Atan(x)));
    }

    public static double math_acosh(double x) {
        if (x >= 1.0) {
            double result = Math.Log(x + Math.Sqrt((x * x) - 1.0));
            if (!Double.IsInfinity(result) || !Double.IsNaN(result)) {
                return result;
            } else {
                throw new ParsingException("Fuera de dominio: " + x);
            }
        } else {
            throw new ParsingException("Fuera de dominio: " + x);
        }
    }

    public static double math_acos(double x) {
        if (x >= -1.0 && x <= 1.0) {
            double result = Math.Acos(x);
            if (!Double.IsInfinity(result) && !Double.IsNaN(result)) {
                return result;
            } else {
                throw new ParsingException("Fuera de dominio para la función 'acos': " + x);
            }
        } else {
            throw new ParsingException("Fuera de dominio para la función 'acos': " + x);
        }
    }

    public static double math_asech(double x) {

        if (x < 0.0) {
            throw new ParsingException("Fuera de rango: " + x);
        }
        if (x < 0.00000000000001) {
            throw new ParsingException("Fuera de rango: " + x);
        }
        return math_acosh(1.0 / x);
    }

    public static double math_atan(double x) {
        if (Math.Abs(x - 0.0) < EPSILON) {
            return 0.0;
        }
        return Math.Atan(x);
    }

    public static double math_asinh(double x) {
        if (Math.Abs(x - 0.0) < EPSILON) {
            return 0.0;
        }

        double result = Math.Log(x + Math.Sqrt((x * x) + 1.0));

        if (Double.IsInfinity(result) || Double.IsNaN(result)) {
            throw new ParsingException("Número fuera de dominio para la función asinh:" + x);
        }

        return result;
    }

    public static double math_atanh(double x) {
        if (Math.Abs(x - 0.0) < EPSILON) {
            return 0.0;
        }
        if (Math.Abs(x) >= 1.0) {
            throw new ParsingException("Fuera de dominio: " + x);
        }
        double y = (1.0 / 2.0) * Math.Log((1.0 + x) / (1.0 - x));
        if (!Double.IsNaN(y) || !Double.IsInfinity(y)) {
            return y;
        } else {
            throw new ParsingException("Fuera de dominio:" + x);
        }
    }

    public static double math_acoth(double x) {
        if (Math.Abs(x) < 1.0) {
            throw new ParsingException("Fuera de dominio para la función 'acoth': " + x);
        }
        double result = 0.5 * (Math.Log((x + 1.0) / x) - Math.Log((x - 1.0) / x));
        if (Double.IsNaN(result)) {
            throw new ParsingException("Fuera de dominio para la función 'acoth': " + x);
        }
        return result;
    }

    public static double math_asin(double x) {
        /// XXX Warning.
        if (Math.Abs(x - 0.0) < EPSILON) {
            return 0.0;
        }
        if (x < -1.0 || x > 1.0) {
            throw new ParsingException("Fuera de dominio para la función 'asinh': " + x);
        }

        double result = Math.Asin(x);
        if (!Double.IsNaN(result) || !Double.IsInfinity(result)) {
            return result;
        } else {
            throw new ParsingException("Número fuera de dominio: " + x);
        }
    }

    public static double math_acsc(double x) {
        if (x <= 1.0 && x >= 1.0) {
            throw new ParsingException("Fuera de dominio para la función 'acoth': " + x);
        }
        return math_asin(1.0 / x);
    }

    public static double math_acsch(double x) {

        if (Math.Abs(x - 0.0) < EPSILON) {
            throw new ParsingException("Fuera de dominio." + x);
        }
        double result = Math.Log(Math.Sqrt(1.0 + (1.0 / (x * x))) + (1.0 / x));
        if (Double.IsNaN(result)) {
            throw new ParsingException("Fuera de dominio." + result);
        }
        return result;
    }

    public static double math_asec(double x) {
		
        double y = Math.Acos(1.0 / x);
        if (!Double.IsInfinity(y) || !Double.IsNaN(y)) {
            return y;
        } else {
            throw new ParsingException("Fuera de dominio para la función asec: " + x);
        }
    }
    
    public static double math_cot(double x) {
		double result = 0.0;
        
        double b = (Math.Cos(2 * x) - 1.0);
        if(Math.Abs(b - 0.0) < EPSILON) {
            throw new ParsingException("Fuera de rango." + x);
        }
        result = -(Math.Sin(2 * x)/b);
        return result;
	}
    
    public static double math_coth(double x) {
		if(Math.Abs(x - 0.0) < EPSILON) {
            throw new ParsingException("Fuera de rango." + x);
		} else {
			return (1.0 / Math.Tanh(x));
		}
	}
    
    public static double math_csc(double x) {
        if (Math.Abs((Math.Abs(x) % Math.PI) - 0.0) < EPSILON)
        {
            throw new ParsingException("Fuera de rango." + x);
		} else {
            return (1.0 / Math.Sin(x));
		}
	}
    
    public static double math_csch(double x) {
        if ((Math.Abs(Math.Sinh(x) - 0.0) < EPSILON) ||
            (Math.Abs(x - 0.0) < EPSILON)) {
             throw new ParsingException("Fuera de rango." + x);
		 } else {
             return (1.0 / Math.Sinh(x));
		 }
	}
    
    public static double math_log(double x) {
		if(Math.Abs(x) < 0.0) {
            throw new ParsingException("Fuera de dominio." + x);
        }
		if(x > 0.0) {
            return Math.Log(x);
		} else {
            throw new ParsingException("Fuera de dominio." + x);
        }
	}

	public static double math_log10(double x) {
		if(x < 0.0) {
            throw new ParsingException("Fuera de rango." + x);
		} else if(x == 0.0 || Math.Abs(x) < 0.00000000000001) {
            throw new ParsingException("Fuera de rango." + x);
		} else {
			return Math.Log10(x);
        }
	}

	public static double math_log2(double x) {
		if(x < 0.0) {
            throw new ParsingException("Fuera de rango." + x);
		} else if(x == 0.0 || Math.Abs(x) < 0.00000000000001) {
            throw new ParsingException("Fuera de rango." + x);
		} else {
			return Math.Log(x) / Math.Log(2.0);
		}
	}

	public static double math_sec(double x) {
		if(Math.Abs(Math.Cos(x)) < 0.00000000000001) {
			throw new ParsingException("Fuera de rango." + x);
		} else {
            return 1.0 / Math.Cos(x);
        }
	}

	public static double math_sech(double x) {
		return ((2.0 * Math.Cosh(x)) / (Math.Cosh(2.0 * x) + 1.0));
	}

	public static double math_sqrt(double x) {
		if(Math.Abs(x) < EPSILON) {
			return 0.0;
		}
		if(x > 0.0) {
			return Math.Sqrt(x);
		} else {
            throw new ParsingException("Fuera de rango." + x);
		}
	}

	public static double math_tan(double x) {
		if((x % Math.PI) == (Math.PI / 2.0)) {
            throw new ParsingException("Fuera de rango." + x);
		} else if(Math.Abs((x % Math.PI)) < EPSILON) {
			return 0.0;
		} else {
            return Math.Tan(x);
        }
    }

    public static double rand_0_to_1() {
        return random.NextDouble();
	}

	public static int rand_int_between(int min, int max) {
        return random.Next(min, max);
	}
    }

}
