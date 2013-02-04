using System;
using System.Collections.Generic;

namespace RepositoryFactory
{
    public static class RepositoryFactory
    {
        private static readonly Dictionary<Type, Type> mapping = new Dictionary<Type,Type>();

        public static TIRep Make<TIRep>()
        {
            Type t = mapping[typeof (TIRep)];
            return (TIRep)Activator.CreateInstance(t);
        }

        public static void SetRepoType<TIRep,TImplRep>()
        {
            mapping.Add(typeof(TIRep), typeof(TImplRep));
        }
    }
}
