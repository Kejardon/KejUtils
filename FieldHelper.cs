using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KejUtils
{
    public static class FieldHelper
    {

        public static Action<U, T> GenerateSetter<U, T>(Type type, string fieldName)
        {
            FieldInfo field = type.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance);
            if (field == null) throw new ArgumentException("fieldName not found on the type.");
            return GenerateSetter<U, T>(field);
        }
        public static Action<U, T> GenerateSetter<U, T>(FieldInfo field)
        {
            Type fieldType = field.FieldType;
            if (typeof(T) != fieldType) throw new ArgumentException("field does not match the expected type.");
            Type baseType = field.DeclaringType;
            if (typeof(U).IsAssignableFrom(baseType)) throw new ArgumentException("Declaring type does not match the expected type.");

            ParameterExpression paramOne = Expression.Parameter(typeof(U), "argOne");
            ParameterExpression paramTwo = Expression.Parameter(fieldType, "argTwo");
            Action<U, T> del = Expression.Lambda<Action<U, T>>(
                Expression.Assign(Expression.MakeMemberAccess(Expression.Convert(paramOne, baseType), field), paramTwo),
                new ParameterExpression[] { paramOne, paramTwo }).Compile();
            return del;
        }
        public static Func<U, T> GenerateGetter<U, T>(Type type, string fieldName)
        {
            FieldInfo field = type.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance);
            if (field == null) throw new ArgumentException("fieldName not found on the type.");
            return GenerateGetter<U, T>(field);
        }
        public static Func<U, T> GenerateGetter<U, T>(FieldInfo field)
        {
            Type fieldType = field.FieldType;
            if (typeof(T) != fieldType) throw new ArgumentException("field does not match the expected type.");
            Type baseType = field.DeclaringType;
            if (typeof(U).IsAssignableFrom(baseType)) throw new ArgumentException("Declaring type does not match the expected type.");

            ParameterExpression paramOne = Expression.Parameter(typeof(U), "argOne");
            ParameterExpression paramTwo = Expression.Parameter(fieldType, "result");
            Func<U, T> del = Expression.Lambda<Func<U, T>>(
                Expression.MakeMemberAccess(Expression.Convert(paramOne, baseType), field),
                //Expression.Assign(Expression.MakeMemberAccess(paramOne, field), paramTwo),
                new ParameterExpression[] { paramOne }).Compile();
            return del;
        }
    }
}
