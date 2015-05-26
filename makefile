COREPATH=src/LibFree.AspNet.Mvc.Bundle.Core/
COREPACKDNUPATH=artifacts/nuget/LibFree.AspNet.Mvc.Bundle.Core/
COREPACKPATH=artifacts/nuget/LibFree.AspNet.Mvc.Bundle.Core/Release/

HTMLAGILIITPACK_PATH=src/LibFree.AspNet.Mvc.Bundle.HtmlParsers.HtmlAgilityPack/
HTMLAGILIITPACK_DNUPATH=artifacts/nuget/LibFree.AspNet.Mvc.Bundle.HtmlParsers.HtmlAgilityPack/
HTMLAGILIITPACK_PACKPATH=artifacts/nuget/LibFree.AspNet.Mvc.Bundle.HtmlParsers.HtmlAgilityPack/Release/

YUICOMPRESSOR_PATH=src/LibFree.AspNet.Mvc.Bundle.Compressors.YUICompressor/
YUICOMPRESSOR_DNUPATH=artifacts/nuget/LibFree.AspNet.Mvc.Bundle.Compressors.YUICompressor/
YUICOMPRESSOR_PACKPATH=artifacts/nuget/LibFree.AspNet.Mvc.Bundle.Compressors.YUICompressor/Release/

nuget: nuget-core nuget-htmlparser nuget-yuicompressor

nuget-core:
	rm -rf $(COREPACKDNUPATH)
	mkdir -p artifacts/nuget
	dnu.cmd pack $(COREPATH) --configuration Release --out $(COREPACKDNUPATH)
	rm -f $(COREPACKPATH)*.nupkg
	mkdir $(COREPACKPATH)lib
	mv $(COREPACKPATH)dnx451 $(COREPACKPATH)lib/dnx451
	cp -p $(COREPATH).nuget/nuget.nuspec $(COREPACKPATH)
	nuget pack $(COREPACKPATH)nuget.nuspec -OutputDirectory $(COREPACKPATH)

nuget-htmlparser:
	rm -rf $(HTMLAGILIITPACK_DNUPATH)
	mkdir -p artifacts/nuget
	dnu.cmd pack $(HTMLAGILIITPACK_PATH) --configuration Release --out $(HTMLAGILIITPACK_DNUPATH)
	rm -f $(HTMLAGILIITPACK_PACKPATH)*.nupkg
	mkdir $(HTMLAGILIITPACK_PACKPATH)lib
	mv $(HTMLAGILIITPACK_PACKPATH)dnx451 $(HTMLAGILIITPACK_PACKPATH)lib/dnx451
	cp -p $(HTMLAGILIITPACK_PATH).nuget/nuget.nuspec $(HTMLAGILIITPACK_PACKPATH)
	nuget pack $(HTMLAGILIITPACK_PACKPATH)nuget.nuspec -OutputDirectory $(HTMLAGILIITPACK_PACKPATH)

nuget-yuicompressor:
	rm -rf $(YUICOMPRESSOR_DNUPATH)
	mkdir -p artifacts/nuget
	dnu.cmd pack $(YUICOMPRESSOR_PATH) --configuration Release --out $(YUICOMPRESSOR_DNUPATH)
	rm -f $(YUICOMPRESSOR_PACKPATH)*.nupkg
	mkdir $(YUICOMPRESSOR_PACKPATH)lib
	mv $(YUICOMPRESSOR_PACKPATH)dnx451 $(YUICOMPRESSOR_PACKPATH)lib/dnx451
	cp -p $(YUICOMPRESSOR_PATH).nuget/nuget.nuspec $(YUICOMPRESSOR_PACKPATH)
	nuget pack $(YUICOMPRESSOR_PACKPATH)nuget.nuspec -OutputDirectory $(YUICOMPRESSOR_PACKPATH)