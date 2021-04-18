﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;

namespace System.CommandLine.Binding
{
    internal class FailedArgumentTypeConversionResult : FailedArgumentConversionResult
    {
        internal FailedArgumentTypeConversionResult(
            IArgument argument,
            Type expectedType,
            string value) :
            base(argument, FormatErrorMessage(argument, expectedType, value))
        {
        }

        private static string FormatErrorMessage(
            IArgument argument,
            Type expectedType,
            string value)
        {
            if (argument is Argument a &&
                a.Parents.Count == 1)
            {
                var firstParent = (IIdentifierSymbol) a.Parents[0];
                var alias = firstParent.Aliases.First();

                switch(firstParent)
                {
                    case ICommand _:
                        return Resources.Instance.ArgumentConversionCannotParseForCommand(value, alias, expectedType);
                    case IOption _:
                        return Resources.Instance.ArgumentConversionCannotParseForOption(value, alias, expectedType);
                }
            }

            return Resources.Instance.ArgumentConversionCannotParse(value, expectedType);
        }
    }
}
