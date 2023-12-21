﻿// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Luna.Compilers.Simulators;

public abstract class AbstractSyntaxClassifier : ISyntaxClassifier
{
    public abstract void Classify(SyntaxClassifierExecutionContext context);
}
