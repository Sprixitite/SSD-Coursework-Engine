project('SprixEngine', 'cs')

testex = get_option('testex')

SPRIX_ENGINE_SRC = []
subdir('src')

CSHARP_ARGS = ['/r:System.Windows.Forms.dll', '/r:System.Drawing.dll']

WinForMono_subproject = subproject('WinForMono')
WinForMono_dep = WinForMono_subproject.get_variable('WinForMono_dep')

if testex
    executable(
        'SprixEngine',
        SPRIX_ENGINE_SRC,
        cs_args: CSHARP_ARGS,
        dependencies: WinForMono_dep,
    )
else
    SPRIX_ENGINE_LIB = shared_library(
        'SprixEngine',
        SPRIX_ENGINE_SRC,
        cs_args: CSHARP_ARGS,
        dependencies: WinForMono_dep
    )

    SPRIX_ENGINE_DEP = declare_dependency(
        link_with: SPRIX_ENGINE_LIB
    )
endif
