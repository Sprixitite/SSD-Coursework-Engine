using System;
using System.Text.RegularExpressions;

namespace Engine.IO {

    public static class ObjFile {

         private static readonly Regex OBJ_REGEX = new Regex("^( ?[!-\"\\$-~]+)+");

        private enum LineType {
            FACE_DEF,
            GROUP_DEF,
            MATERIAL_REF,
            MATERIAL_USE,
            OBJECT_DEF,
            SHADING_EDIT,
            VERTEX_DEF,
            VERTEX_NM_DEF,
            VERTEX_UV_DEF,
            UNKNOWN
        }

        private static LineType get_line_type(string line) {

            switch (line[0]) {

                case 'f': return LineType.FACE_DEF;
                case 'g': return LineType.GROUP_DEF;
                case 'm': return LineType.MATERIAL_REF;
                case 'u': return LineType.MATERIAL_USE;
                case 'o': return LineType.OBJECT_DEF;
                case 's': return LineType.SHADING_EDIT;

                case 'v': switch (line[1]) {
                    case 'n': return LineType.VERTEX_NM_DEF;
                    case 't': return LineType.VERTEX_UV_DEF;
                } return LineType.VERTEX_DEF;

            }

            return LineType.UNKNOWN;

        }

        public static void read_file(string path) {

            foreach (string line in System.IO.File.ReadLines(path)) {

                string real_line = OBJ_REGEX.Match(line).Value;

                LineType line_type = get_line_type(real_line);

                switch (line_type) {
                    default:
                    case LineType.UNKNOWN:
                        throw new Exception("Found an unexpected line prefix when attempting to parse .obj!");
                    case LineType.VERTEX_NM_DEF:
                    case LineType.SHADING_EDIT:
                        continue; // Ignore these, we don't support lighting other than flat lighting
                }

            }

        }

    }

}

