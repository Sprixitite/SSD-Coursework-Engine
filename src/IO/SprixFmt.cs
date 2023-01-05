using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace Engine.IO {

    public static class Sprixane {

        public static SprixaneFile load(string path) {
            SprixaneFile file_contents = new SprixaneFile(parse_file(path));
            return file_contents;
        }

        private static List<string> get_file_content(string path, uint scope_to_add = 0) {

            List<string> file = new List<string>(System.IO.File.ReadAllLines(path));

            // First pass, removes comments and empty lines
            for (int i=0; i<file.Count; i++) {
                string new_line = CONTENT_REGEX.Match(file[i]).Value.TrimEnd();
                switch (new_line) {
                    case "":
                        file.RemoveAt(i);
                        break;
                    default:
                        new_line = new string(' ', (int)scope_to_add*4) + new_line;
                        file[i] = new_line;
                        break;
                }
            }

            return file;

        }

        private static Dictionary<string, DynamicClass> parse_file(string path) {

            List<string> content = get_file_content(path);

            foreach (string s in content) {
                Console.WriteLine(s);
            }

            Dictionary<string, DynamicClass> parsed = new Dictionary<string, DynamicClass>();

            for (uint i=0; i<content.Count; ) {
                var result = real_parse_recurse(path, 0, ref i, ref content);
                parsed.Add(result.Item1, result.Item2);
                i++;
            }

            return parsed;

        }

        private static (string, DynamicClass) real_parse_recurse(string path, uint end_scope, ref uint pos, ref List<string> file) {

            // "Pos" is the header for the object we're parsing

            string returning_name = "\0";
            DynamicClass returning = new DynamicClass();

            returning_name = parse_line(file[(int)pos], pos).content;

            // Move from the header for the object we're parsing to the next line
            pos++;

            for ( ; pos< file.Count; ) {

                LineInfo info1 = parse_line(file[(int)pos], pos);
                LineInfo info2 = parse_line(file[(int)pos+1], pos);

                //if (Math.Abs(info1.scope-info2.scope) > 1) throw new Exception("Scope mismatch encountered between lines " + info1.line_number + " and " + info2.line_number + "!");

                if (info1.scope == end_scope) {
                    return (returning_name, returning);
                }

                // Info1 should always be a header
                if (info1.type != LineType.HEADER) throw new Exception("Missing header encountered on line " + info1.line_number + "!");

                string field_name = info1.content;

                // Valid situations:
                //  Header -> value on the same tab
                //  Header -> header on the next tab
                //  Header -> link on the next tab
                //  All other cases are invalid

                switch (info2.type) {

                    case LineType.HEADER:
                        if (info2.scope != info1.scope+1) throw new Exception("Header followed by header of incorrect indentation on line " + info2.line_number + "!\nCorrect indentation amount should be " + info1.scope+1 + ".");
                        returning.members.Add(field_name, real_parse_recurse(path, info1.scope, ref pos, ref file).Item2);
                        break;

                    case LineType.LINK:
                        if (info2.scope != info1.scope+1) throw new Exception("Header followed by link of incorrect indentation on line " + info2.line_number + "!\nCorrect indentation amount should be " + info1.scope+1 + ".");
                        file.RemoveAt((int)pos+1);
                        file.InsertRange((int)pos+1, get_file_content((string)get_line_value(info2, path), info2.scope));
                        returning.members.Add(field_name, real_parse_recurse(path, info1.scope, ref pos, ref file).Item2);
                        break;

                    case LineType.VALUE:
                        if (info2.scope != info1.scope) throw new Exception("Header followed by value of incorrect indentation on line " + info2.line_number + "!\nCorrect indentation amount should be " + info1.scope + ".");
                        returning.members.Add(field_name, get_line_value(info2));
                        pos+=2;
                        break;

                    case LineType.UNKNOWN:
                        throw new Exception("Line of unknown formatting found on line " + info2.line_number + "!");

                }

            }

            return (returning_name, returning);

        }

        private enum LineType {
            HEADER,
            LINK,
            VALUE,
            UNKNOWN
        }

        private static LineInfo parse_line(string line, uint line_number) {

            LineInfo info = new LineInfo();

            if (HEADER_REGEX.IsMatch(line)) {
                info.type = LineType.HEADER;
            } else if (LINK_REGEX.IsMatch(line)) {
                info.type = LineType.LINK;
            } else if (VALUE_REGEX.IsMatch(line)) {
                info.type = LineType.VALUE;
            } else {
                throw new Exception("IDFK!!!");
            }

            info.scope = (uint)SCOPE_REGEX.Matches(line).Count;
            info.content = line.Trim();
            info.line_number = line_number;

            if (info.type == LineType.HEADER) {
                info.content = info.content.Replace("[", "").Replace("]", "");
            }

            return info;

        }

        private static object get_line_value(LineInfo line_info, string path = "") {

            switch (line_info.type) {
                case LineType.VALUE:
                case LineType.LINK:
                    break;
                default:
                    throw new Exception("Can't get value of line of type \"" + line_info.type + "\".");
            }

            string[] substrings = line_info.content.Split(new[] {' '}, 2);

            if (substrings.Length != 2) throw new Exception("???");

            switch (line_info.type) {
                case LineType.LINK:
                    return Path.Combine(substrings[1].Replace("\"", ""), new FileInfo(Path.GetFullPath(path)).Directory.FullName);
                case LineType.VALUE:switch (substrings[0]) {
                    case "string":
                        return substrings[1];
                    default:
                        Type value_type = Assembly.GetAssembly(typeof(Int32)).GetType("System." + get_primitive_asm_name(substrings[0]), true, true);
                        if (!value_type.IsPrimitive) throw new Exception("Line of type \"value\" found using non-primitive value type at line " + line_info.line_number + "!");
                        return value_type.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public, null, new Type[]{typeof(string)}, new ParameterModifier[]{}).Invoke(null, new object[]{substrings[1]});
                }
                default: throw new Exception("???2");
            }

        }

        // Maps the user facing primitive names (E.g: int) to their assembly names
        // Example: "int" -> "int32"
        private static string get_primitive_asm_name(string alias) {

            switch (alias) {
                case "int": return "int32";
                case "short": return "int16";
                case "long": return "int64";
                case "uint": return "uint32";
                case "ushort": return "uint16";
                case "ulong": return "uint64";
                case "float": return "single";
                default: return alias; // We don't error here because the file could contain the asm names
            }

        }

        private static readonly Regex CONTENT_REGEX = new Regex("^([^\\S\r\n])*([!-\\.0-~]+ |\\[.+\\])([^\"\\/\n ]+|\".*?\"|)");
        private static readonly Regex HEADER_REGEX = new Regex(".*\\[.+\\]");
        private static readonly Regex LINK_REGEX = new Regex(".*link \\\".+\\\"");
        private static readonly Regex VALUE_REGEX = new Regex(".*\\S+ \\S+");
        private static readonly Regex SCOPE_REGEX = new Regex("((\t)|(    ))(?=.*\\S+)");

        public class SprixaneFile {

            public SprixaneFile(Dictionary<string, DynamicClass> loaded) {
                top_level_objects = loaded;
            }

            private Dictionary<string, DynamicClass> top_level_objects;
            
            public DynamicClass get_dynamic_class(string name) {
                return top_level_objects[name];
            }

            public T get_object<T>(string name) {
                return top_level_objects[name].convert_to<T>();
            }

        }

        private struct LineInfo {

            public string content;
            public uint scope;
            public uint line_number;
            public LineType type;

        }

    }

    public class DynamicClass {

        public DynamicClass() {
            members = new Dictionary<string, object>();
        }

        public Dictionary<string, object> members;

        // Publically exposed function for better syntax
        public T convert_to<T>() {

            return (T)convert_to(typeof(T));

        }

        // Private function, we need to pass "Type t" for recursive calls
        private object convert_to(Type t, string name = "") {

            if (t.IsArray) return convert_to_arr(t, name);

            // Create a new object of the desired type
            object converted = Activator.CreateInstance(t);

            foreach (string field_name in members.Keys){

                // Get the field object referred to
                FieldInfo relevant_field = t.GetField(field_name);

                // Get the value relevant to the field we're operating on
                object relevant_object = members[field_name];

                // If the object found is another dynamicclass, we need to convert it too
                if (relevant_object.GetType() == typeof(DynamicClass)) {
                    // This is the reason we need the private version of this function
                    // Without the private version, we're stuck with the restrictive generic type system
                    // The generic type system doesn't allow us to pass a type at runtime (without constructing an all new function)
                    relevant_object = (relevant_object as DynamicClass).convert_to(relevant_field.FieldType);
                }

                // Change the underlying value of our new object
                relevant_field.SetValue(converted, relevant_object);

            }
            return converted;

        }

        private bool is_array() => members.ContainsKey("0");

        private int[] infer_array_dimensions() {

            List<int> dimensions = new List<int>();
            DynamicClass working_on = (DynamicClass)members["0"];
            bool done = false;
            while (!done) {
                // We assume child objects of the working object which contain the key "0" are arrays
                if (working_on.is_array()) {
                    dimensions.Add(working_on.members.Keys.Count);
                    working_on = (DynamicClass)working_on.members["0"];
                } else {
                    done = true;
                }
            }

            return dimensions.ToArray();

        }

        private object get_value(int[] indexes, int depth=0) {
            switch (depth == indexes.Length-1) {
                default:
                    return this.members[indexes[depth].ToString()];
                case false:
                    return ((DynamicClass)members[indexes[depth].ToString()]).get_value(indexes, depth+1);
            }
        }

        private void recursive_assign_arr(int[] dimensions, ref Array arr) {
            int[] index = new int[dimensions.Length];
            for (int i=0; i<dimensions.Length; i++) {
                index[i] = 0;
            }
            recursive_assign_arr(dimensions, ref index, ref arr, 0);
        }

        private void recursive_assign_arr(int[] dimensions, ref int[] index, ref Array arr, int depth=0) {

            for (int i=0; i<dimensions[depth]; i++) {
                if (depth != dimensions.Length-1) recursive_assign_arr(dimensions, ref index, ref arr, depth+1);
                else {
                    arr.SetValue(get_value(index), index);
                }
            }

            if (depth != 0) index[depth-1]++;

        }

        private Array convert_to_arr(Type t, string name) {

            bool single_dim = true;
            bool jagged = false;

            if (t.GetElementType().IsArray) { single_dim = false; jagged = true; }
            else {
                if (members["0"].GetType() != typeof(DynamicClass)) single_dim = true;
                else if (!((DynamicClass)members["0"]).is_array()) single_dim = true;
            }

            Array deserialized;

            switch (single_dim) {

                case true:
                    deserialized = Array.CreateInstance(t.GetElementType(), members.Keys.Count);
                    foreach (string index in members.Keys) {
                        deserialized.SetValue(members[index], int.Parse(index));
                    }
                    return deserialized;

                case false: switch (jagged) {
                    case false:
                        int[] dimensions = infer_array_dimensions();
                        deserialized = Array.CreateInstance(t.GetElementType(), dimensions);
                        recursive_assign_arr(dimensions, ref deserialized);
                        return deserialized;
                    case true:
                        deserialized = Array.CreateInstance(t.GetElementType(), members.Keys.Count);
                        foreach (string index in members.Keys) {
                            deserialized.SetValue(((DynamicClass)members[index]).convert_to(t.GetElementType()), int.Parse(index));
                        }
                        return deserialized;
                    default:
                        throw new Exception("NEW BOOLEAN!!!");
                }

                default: throw new Exception("NEW BOOLEAN!!!");

            }

        }

    }
    

    public class SprixaneSerializer : IFormatter {

        public ISurrogateSelector SurrogateSelector { get; set; }
        public StreamingContext Context { get; set; }
        public SerializationBinder Binder { get; set; }

        public void Serialize(Stream _s, object graph) {

            System.Console.WriteLine(graph.GetType().Name);

        }

        public object Deserialize(Stream _s) { return false; }

    }

}