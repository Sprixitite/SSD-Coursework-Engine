project('SprixEngine', 'cs')

testex = get_option('testex')

SPRIX_ENGINE_SRC = []
subdir('src')

add_global_arguments(['/r:System.Windows.Forms.dll', '/r:System.Drawing.dll'], language: ['cs'])

if testex
    executable(
        'SprixEngine',
        SPRIX_ENGINE_SRC
    )
else
    SPRIX_ENGINE_LIB = shared_library(
        'SprixEngine',
        SPRIX_ENGINE_SRC
    )
endif