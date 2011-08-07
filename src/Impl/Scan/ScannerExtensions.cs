﻿// /* 
// Copyright 2008-2011 Alex Robson
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// */
using System;

namespace typefoundry.Impl.Scan
{
    public static class ScannerExtensions
    {
        public static bool Closes( this Type type, Type openType )
        {
            if ( !openType.IsGenericType && openType.IsGenericTypeDefinition )
                return false;

            bool closes = false;

            if ( openType.IsInterface )
            {
                closes = type.ImplementsInterfaceTemplate( openType ) 
                    && !type.IsGenericTypeDefinition;
            }
            else
            {
                if ( type.BaseType != null )
                    if ( type.BaseType.IsGenericType )
                        closes = type.BaseType.GetGenericTypeDefinition() == openType;
                    else
                        closes = type.BaseType == openType;
            }
            if ( closes ) return true;
            return type.BaseType == null || type.BaseType == typeof( object ) ? false : type.BaseType.Closes( openType );
        }

        public static bool IsOpenGeneric( this Type type )
        {
            return type.IsGenericTypeDefinition || type.ContainsGenericParameters;
        }

        public static int GetGenericCardinality( this Type type )
        {
            return !type.ContainsGenericParameters
                       ? 0
                       : type.GetGenericArguments().Length;
        }

        public static bool IsConcreteAndAssignableTo( this Type pluggedType, Type pluginType )
        {
            return
                pluggedType.IsConcrete() &&
                pluginType.IsAssignableFrom( pluggedType ) &&
                pluginType.GetGenericCardinality() >= pluggedType.GetGenericCardinality();
        }

        public static bool ImplementsInterfaceTemplate( this Type pluggedType, Type templateType )
        {
            if ( !pluggedType.IsConcrete() ) return false;

            foreach( Type interfaceType in pluggedType.GetInterfaces() )
            {
                if ( interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == templateType )
                    return true;
            }

            return false;
        }

        public static bool IsConcrete( this Type type )
        {
            return !type.IsAbstract && !type.IsInterface;
        }
    }
}