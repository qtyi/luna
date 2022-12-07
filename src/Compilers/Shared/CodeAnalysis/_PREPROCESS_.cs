// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if LANG_LUA
// Lua
#elif LANG_MOONSCRIPT
// MoonScript
#elif LANG_TERRA
// Terra
#error 未支持Terra语言
#elif LANG_IDLE
// Idle
#error 未支持Idle语言
#elif LANG_LUAU
// Luau
#error 未支持Luau语言
#else
#error 不支持的语言
#endif
