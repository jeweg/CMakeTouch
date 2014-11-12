CMakeTouch
==========

A Visual Studio 2013 extension to *touch* files in projects, especially CMakeLists.txt.
Touching in this context means updating the last-modification time of files.

It will add an entry named "Touch" to the solution explorer context menu for files, and a "Touch CMakeLists.txt" entry
to the context menu of projects and project folders. The latter searches for files named "CMakeLists.txt" in the respective
project or folder and touches them.

Installation
------------

1. Optional: Build the solution.
2. Run **CMakeTouch.vsix** and restart Visual Studio 2013.

Uninstall as usual from within Visual Studio: Tools -> Extensions and Updates...

License
-------

The MIT License (MIT)

Copyright (c) 2014 Jens Weggemann

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.