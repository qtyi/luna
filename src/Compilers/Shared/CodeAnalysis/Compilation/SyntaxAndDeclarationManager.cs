using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

internal sealed partial class SyntaxAndDeclarationManager : CommonSyntaxAndDeclarationManager
{

}
