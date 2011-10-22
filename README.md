IpaziaPDFReader
=============


This is a simple PDF reader for iPhone/iPad showing the new PageCurl effect in MonoTouch and iOS 5.0.

Version
------------

0.1, still in alpha.


Requirements
--------------

* MonoDevelop 2.8
* MonoTouch 5.0

Installation
-----------

    Just open the solution (.sln) file with MonoDevelop 2.8 or higher.


Usage
-----

The PDF url is encoded at the moment, but you can change it with any online PDF.


Known Issues
------------

* Unexpected crashes when changing pages and disposing them. Probably a problem with the Garbage Collector. Temporally keeping all pages referenced to avoid garbage collection (but this limit the memory, crashing the App with large PDFs). 


Testing
-------

No Unit Tests at the moment.


Contributing
------------

1. Fork it.
2. Create a branch (`git checkout -b my_markup`)
3. Commit your changes (`git commit -am "Added Snarkdown"`)
4. Push to the branch (`git push origin my_markup`)
5. Create an [Issue][1] with a link to your branch
6. Enjoy a refreshing Diet Coke and wait

[1]: http://github.com/github/markup/issues
