// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

namespace Luna.Compilers.Simulators;

public interface IMessageProvider
{
    LocalizableString GetParseOptionName(string name);

    LocalizableString GetParseOptionValue(string name, object value);
}
