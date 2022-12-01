project('SprixEngine', 'cs')

SPRIX_ENGINE_SRC = []
subdir('src')

add_global_arguments(['/r:System.Windows.Forms.dll', '/r:System.Drawing.dll'], language: ['cs'])

shared_library(
    target_name: 'SprixEngine',
    source: SPRIX_ENGINE_SRC
)