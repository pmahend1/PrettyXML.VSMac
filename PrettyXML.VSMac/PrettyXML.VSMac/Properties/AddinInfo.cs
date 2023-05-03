//
// AddinInfo.cs
//
// Author:
//       Mikayla Hutchinson <m.j.hutchinson@gmail.com>
//
// Copyright (c) 2017 Microsoft Corp.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using Mono.Addins;
using Mono.Addins.Description;

[assembly: Addin(
    "PrettyXML.VSMac2022",
    Namespace = "PrettyXML.VSMac2022",
    Version = "1.5.1",
    Category = "Code formatters",
    Flags = AddinFlags.None,
    EnabledByDefault = true,
    Url = "https://github.com/datnvh/PrettyXML.VSMac2022"
)]

[assembly: AddinName("PrettyXML VS Mac 2022")]
[assembly: AddinCategory("Code formatters")]
[assembly: AddinDescription("PrettyXML for Visual Studio for Mac 2022.\nFormats XML just like Windows counterpart.\nShortcut Command+Control+I")]
[assembly: AddinAuthor("DatNVH")]
[assembly: AddinProperty("Icon32", "logo.png")]

[assembly: AddinDependency("::MonoDevelop.Core", "17.5")]
[assembly: AddinDependency("::MonoDevelop.Ide", "17.5")]