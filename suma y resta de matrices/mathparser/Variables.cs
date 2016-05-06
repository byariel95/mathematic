using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathExpressionsCSharp.mathparser
{
    public class Variables {

    private List < Variable > varList;

    public Variables() {
        varList = new List<Variable>();
    }

    public List<Variable> GetVarList() {
        return varList;
    }

    public int GetIdVar(String name) {
        for (int i = 0; i < varList.Count; i++) {
            if (name.Equals(varList[i].VarName, StringComparison.InvariantCultureIgnoreCase))
            {
                return i;
            }
        }
        return -1;
    }

    public bool ExistVar(string varName) {
        return (GetIdVar(varName) != -1);
    }

    public double GetValueVar(string name) {
        int id = GetIdVar(name);
        if (id != -1) {
            return varList[id].Value;
        }
        return 0.0;
    }

    public bool DelVar(string varName) {
        int id = GetIdVar(varName);
        if (id != -1) {
            varList.RemoveAt(id);
            return true;
        }
        return false;
    }

    public bool AddVar(string varName, double value) {
        Variable newVar = new Variable(varName, value);

        int index = GetIdVar(varName);
        if (index == -1) {
            varList.Add(newVar);
        } else {
            varList[index] = newVar;
        }
        return true;
    }

    public bool SetConstant(string varName, bool value) {
        int index = GetIdVar(varName);
        if (index != -1) {
            varList[index].IsConstant = value;
            return true;
        }
        return false;
    }

    public bool isConstant(string varName) {
        int index = GetIdVar(varName);
        if (index != -1) {
            return varList[index].IsConstant;
        }
        return false;
    }

    public bool setDescripcion(string varName, string descripcion) {
        int index = GetIdVar(varName);
        if (index != -1) {
            varList[index].Descripcion = descripcion;
            return true;
        }
        return false;
    }

    public class Variable
    {

        private string var_name;
        private bool Constant;
        private string descripcion;
        private double valueNumeric;

        public Variable(String var_name, bool Constant, string descripcion, double value)
        {
            this.var_name = var_name;
            this.Constant = Constant;
            this.descripcion = descripcion;
            this.valueNumeric = value;
        }

        public Variable(string var_name, double value)
        {
            this.var_name = var_name;
            this.valueNumeric = value;
        }

        public string VarName
        {
            get
            {
                return var_name;
            }
        }

        public bool IsConstant
        {
            get
            {
                return Constant;
            }
            set
            {
                Constant = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return descripcion;
            }
            set
            {
                descripcion = value;
            }
        }

        public double Value
        {
            get
            {
                return valueNumeric;
            }
            set
            {
                valueNumeric = value;
            }
        }

        public override string ToString()
        {
            return "Variable{" + "var_name=" + var_name + ", value=" + valueNumeric +
                    '}';
        }
    };
};

};