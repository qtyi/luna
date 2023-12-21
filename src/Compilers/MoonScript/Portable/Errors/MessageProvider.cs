// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.MoonScript;

partial class MessageProvider
{
    internal const string ErrorCodePrefix = "MOON";

    private partial bool GetIsEnabledByDefault(ErrorCode code) => true;
}
