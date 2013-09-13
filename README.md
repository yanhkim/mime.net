# mime.net

.NET port of node.js module [mime][1].

## Target framework

.NET Framework 3.0

> Useful when you can't use [MimeMapping][2] which is supported since .NET 4.5

## API - Queries

### Mime.Lookup(path)

Get the mime type associated with a file, if no mime type is found `application/octet-stream` is returned. Performs a case-insensitive lookup using the extension in `path` (the substring after the last '/' or '.').  E.g.

```c#
using mime.net;

Mime.Lookup("/path/to/file.txt");         // => "text/plain"
Mime.Lookup("file.txt");                  // => "text/plain"
Mime.Lookup(".TXT");                      // => "text/plain"
Mime.Lookup("htm");                       // => "text/html"
```

### Mime.DefaultType

Sets the mime type returned when `Mime.Lookup` fails to find the extension searched for. (Default is `application/octet-stream`.)

## API - Defining Custom Types

The following APIs allow you to add your own type mappings within your project.

### Mime.Define()

Add custom mime/extension mappings

```c#
using mime.net;

var map = new Dictionary<string, IEnumerable<string>>();
map["text/x-some-format"] = new string[] { "x-sf", "x-sft", "x-sfml" };
map["application/x-my-type"] = new string[] { "x-mt", "x-mtt" };
// etc ...

Mime.Define(map);

Mime.Lookup("x-sft");                 // => "text/x-some-format"
```

### Mime.Load(filepath)

Load mappings from an Apache ".types" format file

```c#
using mime.net;

Mime.Load(@"C:\Users\foobar\my_project.types");
```

[1]: https://github.com/broofa/node-mime
[2]: http://msdn.microsoft.com/en-us/library/system.web.mimemapping.aspx
