Rough spec for a custom .ini/.obj like format I'll prolly use for this
<br/>
Still don't know what I'll use for an extension though, ".srx"? ".spx"? ".sprx"?
<br/>
I feel like I should do "sprixate" to keep in line with the theme of metals ("sprixium", "sprixitite"), but I don't know what extension I should give that... there's also many more metal suffixes and their associated acidic forms...

# Headers:

All headers are enclosed within square braces like so:

```cs
[MY-HEADER]
```

and end with a newline.

# Fields:

Fields are as simple as so:

```cs
[FieldName]
FieldType Value

[MyField]
string "My string"
```

# Comments:

Comments are the standard "//", like so:

```cs
[FieldName] // Some extra info about our field could be put here
FieldType Value // Some extra info about our value could be put here
```

# Composition:

Example of our basic composition:

```cs
[Object1]
    [Field1]
    int 69
    [Field2]
        [SubField1]
        string "EXAMPLE TEXT"
        [SubField2]
        float 1.00000000
[Object2]
    [Field1]
    bool true
```

If we had to write these all into one file it'd be agony, so how do we split things up into multiple?
Well we implement another keyword "link", used as so:

```cs
// File 1
[Object1]
    link "./Object1.sprixfmt"
[Object2]
    [Field1]
    bool true

// "./Object1.sprixfmt"
[Field1]
int 69
[Field2]
    [SubField1]
    string "EXAMPLE TEXT"
    [SubField2]
    float 1.00000000
```

This works well for three reasons:
*   It fits perfectly with our current parsing method
*   It can be easily distinguished from an in-file definition, as an in-file definition would start with a header!
*   We can easily just replace the "link" statement with the linked file's contents when parsing

