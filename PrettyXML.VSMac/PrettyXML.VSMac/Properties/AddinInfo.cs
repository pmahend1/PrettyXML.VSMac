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
    "PrettyXML.VSMac",
    Namespace = "PrettyXML.VSMac",
    Version = "1.5.0",
    Category = "Code formatters",
    Flags = AddinFlags.None,
    EnabledByDefault = true,
    Url = "https://github.com/pmahend1/PrettyXML.VSMac"
)]

[assembly: AddinName("PrettyXML VS Mac")]
[assembly: AddinCategory("Code formatters")]
[assembly: AddinDescription("PrettyXML for Visual Studio for Mac.\nFormats XML just like Windows counterpart.\nShortcut CMD+K J or CMD+K CMD+J")]
[assembly: AddinAuthor("Prateek Mahendrakar")]
[assembly: AddinProperty("Icon32", "logo.png")]