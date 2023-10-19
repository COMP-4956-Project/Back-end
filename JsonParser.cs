using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC_Backend_Frontend.Models;

namespace MVC_Backend_Frontend
{
    public class JsonParser
    {
        public static string Parse(Block? block)
        {
            string code = "";
            if (block == null)
            {
                return code;
            }
            switch (block.type) // variable, function, control, logic, value
            {
                case "variable":
                    code += block.name;
                    break;
                case "function":
                    if (block.field == "operation")
                    {
                        string a = Parse(block.A);
                        string b = Parse(block.B);
                        switch (block.operation) // assign, assign_add, assign_subtract, assign_multiply, assign_divide
                        {
                            case "assign_variable":
                                code += a + " = " + b;
                                break;
                            case "assign_add":
                                code += a + " += " + b;
                                break;
                            case "assign_subtract":
                                code += a + " -= " + b;
                                break;
                            case "assign_multiply":
                                code += a + " *= " + b;
                                break;
                            case "assign_divide":
                                code += a + " /= " + b;
                                break;
                            default: break;
                        }
                    }
                    else
                    {
                        code += block.instruction + "(";
                        code += Parse(block.input);
                        code += ")";
                    }
                    break;
                case "value":
                    switch(block.field) // num, text, operation, var, assignment
                    {
                        case "num":
                            code += block.num;
                            break;
                        case "text":
                            code += "'" + block.text + "'";
                            break;
                        case "operation":
                            string a = Parse(block.A);
                            string b = Parse(block.B);
                            switch (block.operation) // add, subtract, multiply, divide
                            {
                                case "add":
                                    code += a + " + " + b;
                                    break;
                                case "subtract":
                                    code += a + " - " + b;
                                    break;
                                case "multiply":
                                    code += a + " * " + b;
                                    break;
                                case "divide":
                                    code += a + " / " + b;
                                    break;
                                case "add_chain":
                                    code += " + " + a;
                                    break;
                                case "subtract_chain":
                                    code += " - " + a;
                                    break;
                                case "multiply_chain":
                                    code += " * " + a;
                                    break;
                                case "divide_chain":
                                    code += " / " + a;
                                    break;
                                default: break;
                            }
                            if (block.input != null)
                            {
                                code += Parse(block.input);
                            }
                            break;
                        default: break;
                    }
                    break;
                case "control":
                    if (block.parent == null)
                    {
                        block.parent = 0;
                    }
                    code += block.instruction + " ";
                    if (block.instruction != "else") {
                        code += Parse(block.input);
                    }
                    code += ":";
                    foreach (var child in block.children)
                    {
                        code += "\n";
                        child.parent = block.parent + 1;
                        for (int i = 0; i < block.parent; i++)
                        {
                            code += "\t";
                        }
                        code += "\t" + Parse(child);
                    }
                    break;
                case "logic":
                    if (block.logic == "not")
                    {
                        code += block.logic + " ";
                        code += Parse(block.input);
                    } else
                    {
                        string a = Parse(block.A);
                        string b = Parse(block.B);
                        switch (block.logic) // not_equals equals greater_equals less_equals greater less not and or
                        {
                            case "not_equals":
                                code += a + " != " + b;
                                break;
                            case "equals":
                                code += a + " == " + b;
                                break;
                            case "greater_equals":
                                code += a + " >= " + b;
                                break;
                            case "less_equals":
                                code += a + " <= " + b;
                                break;
                            case "greater":
                                code += a + " > " + b;
                                break;
                            case "less":
                                code += a + " < " + b;
                                break;
                            case "and":
                                code += a + " and " + b;
                                break;
                            case "or":
                                code += a + " or " + b;
                                break;
                            default: break;
                        }
                    }
                    break;
                default: break;
            }
            return code;
        }
    }
}